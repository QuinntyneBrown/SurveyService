import { SurveyResultAdd, SurveyResultDelete, SurveyResultEdit, surveyResultActions } from "./survey-result.actions";
import { SurveyResult } from "./survey-result.model";

const template = require("./survey-result-master-detail-embed.component.html");
const styles = require("./survey-result-master-detail-embed.component.scss");

export class SurveyResultMasterDetailEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSurveyResultAdd = this.onSurveyResultAdd.bind(this);
        this.onSurveyResultView = this.onSurveyResultView.bind(this);
        this.onSurveyResultDelete = this.onSurveyResultDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "survey-results"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.surveyResultListElement.setAttribute("survey-results", JSON.stringify(this.surveyResults));
    }

    private _setEventListeners() {
        this.addEventListener(surveyResultActions.ADD, this.onSurveyResultAdd);
        this.addEventListener(surveyResultActions.VIEW, this.onSurveyResultView);
        this.addEventListener(surveyResultActions.DELETE, this.onSurveyResultDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(surveyResultActions.ADD, this.onSurveyResultAdd);
        this.removeEventListener(surveyResultActions.VIEW, this.onSurveyResultView);
        this.removeEventListener(surveyResultActions.DELETE, this.onSurveyResultDelete);
    }

    public onSurveyResultAdd(e) {
        const index = this.surveyResults.findIndex(o => o.id == e.detail.surveyResult.id);
        const indexBaseOnUniqueIdentifier = this.surveyResults.findIndex(o => o.name == e.detail.surveyResult.name);

        if (index > -1 && e.detail.surveyResult.id != null) {
            this.surveyResults[index] = e.detail.surveyResult;
        } else if (indexBaseOnUniqueIdentifier > -1) {
            for (var i = 0; i < this.surveyResults.length; ++i) {
                if (this.surveyResults[i].name == e.detail.surveyResult.name)
                    this.surveyResults[i] = e.detail.surveyResult;
            }
        } else {
            this.surveyResults.push(e.detail.surveyResult);
        }
        
        this.surveyResultListElement.setAttribute("survey-results", JSON.stringify(this.surveyResults));
        this.surveyResultViewElement.setAttribute("survey-result", JSON.stringify(new SurveyResult()));
    }

    public onSurveyResultView(e) {
        this.surveyResultViewElement.setAttribute("survey-result", JSON.stringify(e.detail.surveyResult));
    }

    public onSurveyResultDelete(e) {
        if (e.detail.surveyResult.Id != null && e.detail.surveyResult.Id != undefined) {
            this.surveyResults.splice(this.surveyResults.findIndex(o => o.id == e.detail.optionId), 1);
        } else {
            for (var i = 0; i < this.surveyResults.length; ++i) {
                if (this.surveyResults[i].name == e.detail.surveyResult.name)
                    this.surveyResults.splice(i, 1);
            }
        }

        this.surveyResultListElement.setAttribute("survey-results", JSON.stringify(this.surveyResults));
        this.surveyResultViewElement.setAttribute("survey-result", JSON.stringify(new SurveyResult()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "survey-results":
                this.surveyResults = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<SurveyResult> { return this.surveyResults; }

    private surveyResults: Array<SurveyResult> = [];
    public surveyResult: SurveyResult = <SurveyResult>{};
    public get surveyResultViewElement(): HTMLElement { return this.querySelector("ce-survey-result-view-embed") as HTMLElement; }
    public get surveyResultListElement(): HTMLElement { return this.querySelector("ce-survey-result-list-embed") as HTMLElement; }
}

customElements.define(`ce-survey-result-master-detail-embed`,SurveyResultMasterDetailEmbedComponent);
