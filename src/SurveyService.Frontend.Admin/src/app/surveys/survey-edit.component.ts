import { Survey } from "./survey.model";
import { SurveyService } from "./survey.service";
import { EditorComponent, tabsEvents } from "../shared";
import { Router } from "../router";
import { questionActions, Question } from "../questions";

const template = require("./survey-edit.component.html");
const styles = require("./survey-edit.component.scss");

export class SurveyEditComponent extends HTMLElement {
    constructor(
		private _surveyService: SurveyService = SurveyService.Instance,
		private _router: Router = Router.Instance
		) {
        super();
		this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
        this.onTabSelectedIndexChanged = this.onTabSelectedIndexChanged.bind(this);
        this.onQuestionsChanged = this.onQuestionsChanged.bind(this);
    }

    static get observedAttributes() {
        return [
            "survey-id",
            "tab-index"
        ];
    }

    public onTabSelectedIndexChanged(e) {
        this._router.navigate(["survey", "edit", this.surveyId, "tab", e.detail.selectedIndex]);
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
		this._bind();
		this._setEventListeners();
    }
    
    private async _bind() {
        this._tabsElement.setAttribute("custom-tab-index", `${this.customTabIndex}`);

        this._titleElement.textContent = this.surveyId ? "Edit Survey": "Create Survey";
        this.descriptionEditor = new EditorComponent(this.descriptionElement);

        if (this.surveyId) {
            const survey: Survey = await this._surveyService.getById(this.surveyId);                
            this._nameInputElement.value = survey.name;  
            this._logoUrlInputElement.value = survey.logoUrl;
            this.descriptionEditor.setHTML(survey.description);
            this._titleElement.textContent = `Edit Survey: ${survey.name}`;
            this.questionMasterDetailElement.setAttribute("questions", JSON.stringify(survey.questions));
            this.surveyResultMasterDetailElement.setAttribute("survey-results", JSON.stringify(survey.surveyResults));
            this._uniqueIdElement.innerText = survey.uniqueId;
            this.questions = survey.questions;
            this.uniqueId = survey.uniqueId;
        } else {
            this._deleteButtonElement.style.display = "none";
        } 	
	}

	private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
		this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
        this.addEventListener(tabsEvents.SELECTED_INDEX_CHANGED, this.onTabSelectedIndexChanged);
        this.questionMasterDetailElement.addEventListener(questionActions.QUESTIONS_CHANGED, this.onQuestionsChanged);
    }

    public async onQuestionsChanged(e) {
        this.questions = e.detail.questions;
        await this.Save();
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
		this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
        this.removeEventListener(tabsEvents.SELECTED_INDEX_CHANGED, this.onTabSelectedIndexChanged);
        this.questionMasterDetailElement.removeEventListener(questionActions.QUESTIONS_CHANGED, this.onQuestionsChanged);
    }
    public async Save() {
        const survey = {
            id: this.surveyId,
            uniqueId: this.uniqueId,
            name: this._nameInputElement.value,
            logoUrl: this._logoUrlInputElement.value,
            description: this.descriptionEditor.text,
            questions: this.questions
        } as Survey;

        await this._surveyService.add(survey);
    }
    public async onSave() {
        await this.Save();
		this._router.navigate(["survey","list"]);
    }

    public async onDelete() {        
        await this._surveyService.remove({ id: this.surveyId });
		this._router.navigate(["survey","list"]);
    }

	public onTitleClick() {
        this._router.navigate(["survey", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "survey-id":
                this.surveyId = newValue;
                break;
            case "tab-index":
                this.customTabIndex = newValue;
                break;
        }        
    }

    public questions: Array<Question> = [];
    public uniqueId: any;
    public customTabIndex: any;
    public surveyId: number;
    public descriptionEditor: EditorComponent;
    public get descriptionElement(): HTMLElement { return this.querySelector(".survey-description") as HTMLElement; }
    public get questionMasterDetailElement(): HTMLElement { return this.querySelector("ce-question-master-detail-embed") as HTMLElement; }
    public get surveyResultMasterDetailElement(): HTMLElement { return this.querySelector("ce-survey-result-master-detail-embed") as HTMLElement; }

    private get _uniqueIdElement(): HTMLElement { return this.querySelector(".survey-unique-id") as HTMLElement; }
    private get _tabsElement(): HTMLElement { return this.querySelector("ce-tabs") as HTMLElement; }
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".survey-name") as HTMLInputElement; }
    private get _logoUrlInputElement(): HTMLInputElement { return this.querySelector(".survey-logo-url") as HTMLInputElement; }
}

customElements.define(`ce-survey-edit`,SurveyEditComponent);
