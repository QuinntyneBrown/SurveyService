const template = require("./response-view-item.component.html");
const styles = require("./response-view-item.component.scss");

export class ResponseViewItemComponent extends HTMLElement {
    constructor() {
        super();        
    }

    static get observedAttributes() {
        return ["entity"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    disconnectedCallback() { }

    private _bind() {
        this.questionBodyElement.innerHTML = this.entity.question.body;
        this.responseBodyElement.innerHTML = this.entity.value;
    }

    private _setEventListeners() { }
    
    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "entity":
                this.entity = JSON.parse(newValue);                
                break;
        }        
    }
    
    public entity: any;
    public get questionBodyElement(): HTMLElement { return this.querySelector(".question-body") as HTMLElement; }
    public get responseBodyElement(): HTMLElement { return this.querySelector(".response-body") as HTMLElement; }
}

customElements.define(`ce-response-view-item`,ResponseViewItemComponent);
