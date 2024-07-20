import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IQuizCreate } from '../../interfaces/quiz-create';
import { IQuizQuestionResponse } from '../../interfaces/quiz-question';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  baseUrl: string = environment.apiBaseUrl + 'Gemini';

  allQss$: BehaviorSubject<IQuizQuestionResponse[]> = new BehaviorSubject<IQuizQuestionResponse[]>([]);

  constructor(private httpClient: HttpClient) {

  }

  createQuiz(reqBody: IQuizCreate): Observable<IQuizQuestionResponse[]> {
    return this.httpClient.post<IQuizQuestionResponse[]>(this.baseUrl + '/generate-quiz', reqBody, { withCredentials: true });
  }
}
