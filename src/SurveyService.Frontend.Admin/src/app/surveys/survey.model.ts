import { Question } from "../questions";
import { SurveyResult } from "../survey-results";

export class Survey { 
    id: any;
    uniqueId: any;
    name: string;
    logoUrl: string;
    description: string;
    questions: Array<Question> = [];
    surveyResults: Array<SurveyResult> = [];
}
