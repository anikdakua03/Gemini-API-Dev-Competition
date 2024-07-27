import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IRecipe, IRecipeItem } from '../../interfaces/recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private baseUrl: string = environment.apiBaseUrl + 'Gemini';

  constructor(private httpClient: HttpClient) {

  }

  generateRecipes(reqBody: IRecipe): Observable<IRecipeItem[]> {
    return this.httpClient.post<IRecipeItem[]>(this.baseUrl + '/generate-recipe', reqBody, { withCredentials: true });
  }
}
