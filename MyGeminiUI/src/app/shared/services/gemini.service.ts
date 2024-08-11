import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IQuizCreate } from '../../interfaces/quiz-create';
import { IQuizQuestionResponse } from '../../interfaces/quiz-question';
import { IRecipe, IRecipeItem } from '../../interfaces/recipe';
import { IReviewCode, IReviewCodeResponse } from '../../interfaces/review-code';
import { GenAIResponse } from '../../interfaces/gen-ai-response';

@Injectable({
  providedIn: 'root'
})
export class GeminiService {

  private baseUrl: string = environment.apiBaseUrl + 'Gemini';

  allQss$: BehaviorSubject<IQuizQuestionResponse[]> = new BehaviorSubject<IQuizQuestionResponse[]>([]);

  constructor(private httpClient: HttpClient) {

  }

  createQuiz(reqBody: IQuizCreate): Observable<IQuizQuestionResponse[]> {
    return this.httpClient.post<IQuizQuestionResponse[]>(this.baseUrl + '/generate-quiz', reqBody, { withCredentials: true });
  }

  generateRecipes(reqBody: IRecipe): Observable<GenAIResponse> {
    return this.httpClient.post<GenAIResponse>(this.baseUrl + '/generate-recipe', reqBody, { withCredentials: true });
  }

  generateReview(reqBody: IReviewCode): Observable<GenAIResponse> {
    return this.httpClient.post<GenAIResponse>(this.baseUrl + '/review-code', reqBody, { withCredentials: true });
  }

  conversationAndAnswer(askQuery: object): Observable<GenAIResponse> {
    return this.httpClient.post<GenAIResponse>(this.baseUrl + '/conversation', askQuery, { withCredentials: true });
  }
}
