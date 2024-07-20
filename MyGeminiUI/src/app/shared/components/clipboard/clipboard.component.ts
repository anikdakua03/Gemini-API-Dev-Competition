import { Component } from '@angular/core';

@Component({
  selector: 'app-clipboard',
  standalone: true,
  imports: [],
  templateUrl: './clipboard.component.html',
  styles: ``
})
export class ClipboardComponent {
  clicked: boolean = false;

  onClickCopy() {
    this.clicked = true;
    // after 5 seconds make that changed to Copy
    setTimeout(() => {
      this.clicked = false;
    }, 5000);
  }
}
