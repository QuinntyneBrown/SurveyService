import { SurveyResultService } from "./survey-result.service";
import { SurveyResult } from "./survey-result.model";
import { createElement } from "../utilities";

const template = require("./survey-result-view.component.html");
const styles = require("./survey-result-view.component.scss");

export class SurveyResultViewComponent extends HTMLElement {
    constructor(
        private _surveyResultService: SurveyResultService = SurveyResultService.Instance
    ) {
        super();
    }

    static get observedAttributes () {
        return [
            "survey-result-id"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.surveyResult = await this._surveyResultService.getById(this.surveyResultId);
        
        this.titleElement.innerText = `Survey Results: ${moment(`${this.surveyResult.createdOn}`).tz("America/ New_York").format('MMMM Do YYYY h:mm a')}`;

        for (let i = 0; i < this.surveyResult.responses.length; i++) {
            let responseViewElement = createElement("<ce-response-view-item></ce-response-view-item>");
            responseViewElement.setAttribute("entity", JSON.stringify(this.surveyResult.responses[i]));
            this.surveyResultResponsesElement.appendChild(responseViewElement);
        }
    }

    private _setEventListeners() {

    }

    disconnectedCallback() {

    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "survey-result-id":
                this.surveyResultId = JSON.parse(newValue);

                if (this.parentNode) {

                }

                break;
        }
    }

    public surveyResultId: any;
    public surveyResult: SurveyResult;
    public get titleElement(): HTMLElement { return this.querySelector(".survey-result-title") as HTMLElement; }
    public get surveyResultResponsesElement(): HTMLElement { return this.querySelector(".survey-result-responses") as HTMLElement; }
}

customElements.define(`ce-survey-result-view`,SurveyResultViewComponent);
