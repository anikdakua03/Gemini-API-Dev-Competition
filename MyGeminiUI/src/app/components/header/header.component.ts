import { NgClass } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ALL_ROUTES } from '../../shared/constants/all-route.constant';
import { FAIcons } from '../../shared/constants/font-awesome-icons';

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
  c = FAIcons.C;
  o = FAIcons.O;

  constructor(private router: Router) { }

  openSidebar() {
    this.isSidebarShowing = true;
  }

  closeSidebar() {
    this.isSidebarShowing = false;
  }

  navigateToRandom() {
    let randomRoute: string = '';

    const index: number = Math.floor(Math.random() * ALL_ROUTES.length);

    randomRoute = ALL_ROUTES[index];

    this.router.navigateByUrl(randomRoute);
  }
}
