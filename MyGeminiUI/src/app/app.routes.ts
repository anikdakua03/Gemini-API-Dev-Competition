import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { RecipesComponent } from './components/recipes/recipes.component';
import { quizGuard } from './shared/guards/quiz.guard';

export const routes: Routes = [
    {
        path: '', title: 'Home', component: HomeComponent
    },
    {
        path: 'quiz', title: 'Play Quiz', component: QuizComponent, canActivate: [quizGuard]
    },
    {
        path: 'recipe-generator', title: 'Generate Recipes', component: RecipesComponent
    },
    {
        path: '**', title: 'Page Not Found', component: PageNotFoundComponent
    }
];
