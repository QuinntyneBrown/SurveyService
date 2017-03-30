import { SurveyResult } from "./survey-result.model";

const template = require("./survey-result-list-embed.component.html");
const styles = require("./survey-result-list-embed.component.scss");

export class SurveyResultListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "survey-results"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.surveyResults.length; i++) {
            let el = this._document.createElement(`ce-survey-result-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.surveyResults[i]));
            this.appendChild(el);
        }    
    }

    surveyResults:Array<SurveyResult> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "survey-results":
                this.surveyResults = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-survey-result-list-embed", SurveyResultListEmbedComponent);
