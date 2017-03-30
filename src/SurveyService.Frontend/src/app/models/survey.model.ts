import { Question } from "./question.model";

export class Survey { 
    public id: any;
    public uniqueId: any;
    public name: string;
    public questions: Array<Question> = [];
    public logoUrl: string;
}
