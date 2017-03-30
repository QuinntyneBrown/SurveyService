import { SurveyResult } from "./survey-result.model";
import {  SurveyResultDelete, SurveyResultEdit, SurveyResultAdd, SurveyResultView } from "./survey-result.actions";
import { getBaseUrl } from "../utilities";
	
const template = require("./survey-result-item-embed.component.html");
const styles = require("./survey-result-item-embed.component.scss");

export class SurveyResultItemEmbedComponent extends HTMLElement {
    constructor() {
        super();

        this._onDeleteClick = this._onDeleteClick.bind(this);
        this._onEditClick = this._onEditClick.bind(this);
        this._onViewClick = this._onViewClick.bind(this);
        this._onFullClick = this._onFullClick.bind(this);
    }

    static get observedAttributes() {
        return ["entity"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    disconnectedCallback() {
        this._deleteLinkElement.removeEventListener("click", this._onDeleteClick);
        this._viewLinkElement.removeEventListener("click", this._onViewClick);
        this._fullElement.removeEventListener("click", this._onFullClick);
    }

    private _bind() {
        this._nameElement.textContent = moment(moment.tz(this.entity.createdOn, "America/New_York")).format('MMMM Do YYYY h:mm a');
    }

    private _setEventListeners() {
        this._deleteLinkElement.addEventListener("click", this._onDeleteClick);        
        this._viewLinkElement.addEventListener("click", this._onViewClick);
        this._fullElement.addEventListener("click", this._onFullClick);
    }
    private _onFullClick(e) {
        window.open(`${getBaseUrl(window)}/survey-result/view/${this.entity.id}`);
    }

    private async _onDeleteClick(e:Event) {
        this.dispatchEvent(new SurveyResultDelete(this.entity)); 
    }

    private _onEditClick() {
        this.dispatchEvent(new SurveyResultEdit(this.entity));
    }

    private _onViewClick() {
        this.dispatchEvent(new SurveyResultView(this.entity));
    }
    
    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "entity":
                this.entity = JSON.parse(newValue);
                break;
        }        
    }

    private get _nameElement() { return this.querySelector("p") as HTMLElement; }
    private get _deleteLinkElement() { return this.querySelector(".entity-item-delete") as HTMLElement; }
    private get _editLinkElement() { return this.querySelector(".entity-item-edit") as HTMLElement; }
    private get _viewLinkElement() { return this.querySelector(".entity-item-view") as HTMLElement; }
    private get _fullElement() { return this.querySelector(".entity-item-full") as HTMLElement; }

    public entity: SurveyResult;
}

customElements.define(`ce-survey-result-item-embed`,SurveyResultItemEmbedComponent);
