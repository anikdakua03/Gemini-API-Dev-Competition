import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { QuizComponent } from './components/quiz/quiz.component';

export const routes: Routes = [
    {
        path: '', title: 'Home', component: HomeComponent
    },
    {
        path: 'quiz', title: 'Play Quiz', component: QuizComponent
    },
    {
        path: '**', title: 'Page Not Found', component: PageNotFoundComponent
    }
];
