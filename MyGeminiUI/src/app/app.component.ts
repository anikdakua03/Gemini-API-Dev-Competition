import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { HeaderComponent } from './components/header/header.component';
import { LoaderComponent } from "./shared/components/loader/loader.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, LoaderComponent, ToastModule],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  title = 'MyGeminiUI';
}
