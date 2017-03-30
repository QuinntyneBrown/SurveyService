import { Survey } from "./survey.model";
import { SurveyService } from "./survey.service";

const template = require("./survey-list.component.html");
const styles = require("./survey-list.component.scss");

export class SurveyListComponent extends HTMLElement {
    constructor(
		private _document: Document = document,
		private _surveyService: SurveyService = SurveyService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {
		const surveys: Array<Survey> = await this._surveyService.get();
        for (var i = 0; i < surveys.length; i++) {
			let el = this._document.createElement(`ce-survey-item`);
			el.setAttribute("entity", JSON.stringify(surveys[i]));
			this.appendChild(el);
        }	
	}
}

customElements.define("ce-survey-list", SurveyListComponent);
