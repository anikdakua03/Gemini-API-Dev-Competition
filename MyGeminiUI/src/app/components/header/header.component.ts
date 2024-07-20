import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FAIcons } from '../../shared/constants/font-awesome-icons';
import { NgClass } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [FontAwesomeModule, NgClass, RouterLink],
  templateUrl: './header.component.html',
  styles: ``
})
export class HeaderComponent {

  checkCurrUser: boolean = false;
  currUserName: string = "";
  isSidebarShowing: boolean = false;
  isLoading: boolean = false;
  dots = FAIcons.ELLIPSES;
  close = FAIcons.CLOSE;
  hamburger = FAIcons.HAMBURGER;
  home = FAIcons.HOME;
  quizzer = FAIcons.QUIZZ;
  payment = FAIcons.CREDIT_CARD;
  ai = FAIcons.MAGIC_SPARKLES;
  c = FAIcons.C;
  card = FAIcons.CREDIT_CARD;
  o = FAIcons.O;

  constructor() { }

  openSidebar() {
    this.isSidebarShowing = true;
  }

  closeSidebar() {
    this.isSidebarShowing = false;
  }

}
