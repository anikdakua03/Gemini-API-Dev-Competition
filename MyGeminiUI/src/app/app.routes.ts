import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { QuizComponent } from './components/quiz/quiz.component';
import { RecipesComponent } from './components/recipes/recipes.component';
import { quizGuard } from './shared/guards/quiz.guard';
import { CodeReviewerComponent } from './components/code-reviewer/code-reviewer.component';
import { ConversationComponent } from './components/conversation/conversation.component';

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
        path: 'code-review', title: 'Code Reviewer', component: CodeReviewerComponent
    },
    {
        path: 'chat', title: 'Conversation with Gemini', component: ConversationComponent
    },
    {
        path: '**', title: 'Page Not Found', component: PageNotFoundComponent
    }
];
