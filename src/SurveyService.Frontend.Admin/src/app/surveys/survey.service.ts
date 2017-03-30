import { fetch } from "../utilities";
import { Survey } from "./survey.model";

export class SurveyService {
    constructor(private _fetch = fetch) { }

    private static _instance: SurveyService;

    public static get Instance() {
        this._instance = this._instance || new SurveyService();
        return this._instance;
    }

    public get(): Promise<Array<Survey>> {
        return this._fetch({ url: "/api/survey/get?tenantId=1", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { surveys: Array<Survey> }).surveys;
        });
    }

    public getById(id): Promise<Survey> {
        return this._fetch({ url: `/api/survey/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { survey: Survey }).survey;
        });
    }

    public add(survey) {
        return this._fetch({ url: `/api/survey/add`, method: "POST", data: { survey }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/survey/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
