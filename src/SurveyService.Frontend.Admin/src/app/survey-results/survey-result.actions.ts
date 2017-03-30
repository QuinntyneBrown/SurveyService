import { SurveyResult } from "./survey-result.model";

export const surveyResultActions = {
    ADD: "[SurveyResult] Add",
    EDIT: "[SurveyResult] Edit",
    DELETE: "[SurveyResult] Delete",
    VIEW: "[SurveyResult] View",
    SURVEY_RESULTS_CHANGED: "[SurveyResult] SurveyResults Changed"
};

export class SurveyResultEvent extends CustomEvent {
    constructor(eventName: string, surveyResult: SurveyResult) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { surveyResult }
        });
    }
}

export class SurveyResultView extends SurveyResultEvent {
    constructor(surveyResult: SurveyResult) {
        super(surveyResultActions.VIEW, surveyResult);
    }
}

export class SurveyResultAdd extends SurveyResultEvent {
    constructor(surveyResult: SurveyResult) {
        super(surveyResultActions.ADD, surveyResult);
    }
}

export class SurveyResultEdit extends SurveyResultEvent {
    constructor(surveyResult: SurveyResult) {
        super(surveyResultActions.EDIT, surveyResult);
    }
}

export class SurveyResultDelete extends SurveyResultEvent {
    constructor(surveyResult: SurveyResult) {
        super(surveyResultActions.DELETE, surveyResult);
    }
}

export class SurveyResultsChanged extends CustomEvent {
    constructor(surveyResults: Array<SurveyResult>) {
        super(surveyResultActions.SURVEY_RESULTS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { surveyResults }
        });
    }
}
