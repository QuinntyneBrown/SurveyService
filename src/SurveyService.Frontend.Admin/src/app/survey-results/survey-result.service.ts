import { fetch } from "../utilities";
import { SurveyResult } from "./survey-result.model";

export class SurveyResultService {
    constructor(private _fetch = fetch) { }

    private static _instance: SurveyResultService;

    public static get Instance() {
        this._instance = this._instance || new SurveyResultService();
        return this._instance;
    }

    public get(): Promise<Array<SurveyResult>> {
        return this._fetch({ url: "/api/surveyresult/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { surveyResults: Array<SurveyResult> }).surveyResults;
        });
    }

    public getById(id): Promise<SurveyResult> {
        return this._fetch({ url: `/api/surveyresult/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { surveyResult: SurveyResult }).surveyResult;
        });
    }

    public add(surveyResult) {
        return this._fetch({ url: `/api/surveyresult/add`, method: "POST", data: { surveyResult }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/surveyresult/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
