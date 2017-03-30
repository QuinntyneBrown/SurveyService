export class SurveyResult { 
    public id:any;
    public name: string;
    public createdOn: string;
    public responses: Array<any>;

    public fromJSON(data: { name:string, createdOn:string, responses:Array<any> }): SurveyResult {
        let surveyResult = new SurveyResult();
        surveyResult.name = data.name;
        return surveyResult;
    }
}