export interface IQuizQuestionResponse {
    questionId: number;
    questionText: string;
    questionType: string;
    questionLevel?: string;
    categoryName: string;
    allOptions: string[]
    correctAnswers: string[];
}