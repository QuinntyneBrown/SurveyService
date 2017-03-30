import { ApiService } from "../shared";
import { Router } from "../router";

const template = require("./splash.component.html");
const styles = require("./splash.component.scss");

export class SplashComponent extends HTMLElement {
    constructor(
        private _apiService: ApiService = ApiService.Instance,
        private _router: Router = Router.Instance
    ) {
        super();
        this.onEnterClick = this.onEnterClick.bind(this);
    }

    static get observedAttributes () {
        return [];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.survey = await this._apiService.getSurvey();
        this.logoElement.src = this.survey.logoUrl;        
        this.descriptionElement.innerHTML = this.survey.description;
    }

    public onEnterClick() {
        this._router.navigate(["landing"]);
    }

    private _setEventListeners() {
        this.enterElement.addEventListener("click", this.onEnterClick);
    }

    disconnectedCallback() {
        this.enterElement.removeEventListener("click", this.onEnterClick);
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            default:
                break;
        }
    }

    public survey: any;
    public get enterElement(): HTMLElement { return this.querySelector("ce-button") as HTMLElement; }
    public get logoElement(): HTMLImageElement { return this.querySelector("img") as HTMLImageElement; }
    public get descriptionElement(): HTMLElement { return this.querySelector(".description") as HTMLElement; }
}

customElements.define(`ce-splash`,SplashComponent);
