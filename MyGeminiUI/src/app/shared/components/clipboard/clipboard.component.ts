import { Component } from '@angular/core';
import { FAIcons } from '../../constants/font-awesome-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-clipboard',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './clipboard.component.html',
  styles: ``
})
export class ClipboardComponent {
  clicked: boolean = false;

  copy = FAIcons.COPY;
  tick = FAIcons.TICK;

  onClickCopy() {
    this.clicked = true;
    // after 5 seconds make that changed to Copy
    setTimeout(() => {
      this.clicked = false;
    }, 5000);
  }
}
