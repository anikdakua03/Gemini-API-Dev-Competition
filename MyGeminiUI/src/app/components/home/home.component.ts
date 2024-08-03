import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ToastModule } from 'primeng/toast';
import { FAIcons } from '../../shared/constants/font-awesome-icons';
import { QuizCreateComponent } from './quiz-create/quiz-create.component';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CardModule, FontAwesomeModule, QuizCreateComponent, RouterLink, ToastModule],
  templateUrl: './home.component.html',
  styles: ``
})
export class HomeComponent {
  longRightArrow = FAIcons.LONG_RIGHT_ARROW;
  q = FAIcons.QUIZZ;
  utensil = FAIcons.UTENSIL;
  chat = FAIcons.CHAT;
  code = FAIcons.CODE;

  openQuizModal: boolean = false;

  constructor() {

  }

  openCreateQuizModal() {
    this.openQuizModal = true;
  }

  closeCreateQuizModal(isClosed: boolean) {
    this.openQuizModal = !isClosed;
  }
}
