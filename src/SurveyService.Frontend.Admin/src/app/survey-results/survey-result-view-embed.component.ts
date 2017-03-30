import { SurveyResult } from "./survey-result.model";
import { createElement } from "../utilities";

const template = require("./survey-result-view-embed.component.html");
const styles = require("./survey-result-view-embed.component.scss");

export class SurveyResultViewEmbedComponent extends HTMLElement {
    constructor() {
        super();
    }

    static get observedAttributes () {
        return [
            "survey-result"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {

    }

    private _setEventListeners() {

    }

    disconnectedCallback() {

    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "survey-result":                
                this.surveyResult = JSON.parse(newValue);
                if (this.parentNode) {
                    this.surveyResultResponsesElement.innerHTML = "";

                    for (let i = 0; i < this.surveyResult.responses.length; i++) {
                        let responseViewElement = createElement("<ce-response-view-item></ce-response-view-item>");
                        responseViewElement.setAttribute("entity", JSON.stringify(this.surveyResult.responses[i]));
                        this.surveyResultResponsesElement.appendChild(responseViewElement);                        
                    }
                }
                break;
        }
    }

    public surveyResult: SurveyResult;
    public get surveyResultResponsesElement(): HTMLElement { return this.querySelector(".survey-result-responses") as HTMLElement; }
}

customElements.define(`ce-survey-result-view-embed`,SurveyResultViewEmbedComponent);
