import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { KatexOptions, MarkdownModule } from 'ngx-markdown';
import { CardModule } from 'primeng/card';
import { InputTextareaModule } from 'primeng/inputtextarea';
import 'prismjs';
import "prismjs/components/prism-bash.min.js";
import "prismjs/components/prism-c.min.js";
import "prismjs/components/prism-cobol.min.js";
import "prismjs/components/prism-cpp.min.js";
import "prismjs/components/prism-csharp.min.js";
import "prismjs/components/prism-css.min.js";
import "prismjs/components/prism-dart.min.js";
import "prismjs/components/prism-docker.min.js";
import "prismjs/components/prism-fsharp.min.js";
import "prismjs/components/prism-go.min.js";
import "prismjs/components/prism-http.min.js";
import "prismjs/components/prism-java.min.js";
import "prismjs/components/prism-javascript.min.js";
import "prismjs/components/prism-jsx.min.js";
import "prismjs/components/prism-kotlin.min.js";
import "prismjs/components/prism-pascal.min.js";
import "prismjs/components/prism-perl.min.js";
import "prismjs/components/prism-powershell.min.js";
import "prismjs/components/prism-python.min.js";
import "prismjs/components/prism-r.min.js";
import "prismjs/components/prism-ruby.min.js";
import "prismjs/components/prism-rust.min.js";
import "prismjs/components/prism-sass.min.js";
import "prismjs/components/prism-scala.min.js";
import "prismjs/components/prism-scss.min.js";
import "prismjs/components/prism-sql.min.js";
import "prismjs/components/prism-swift.min.js";
import "prismjs/components/prism-typescript.min.js";
import "prismjs/components/prism-v.min.js";
import "prismjs/components/prism-yaml.min.js";
import { IReviewCodeResponse } from '../../interfaces/review-code';
import { ClipboardComponent } from '../../shared/components/clipboard/clipboard.component';
import { FAIcons } from '../../shared/constants/font-awesome-icons';
import { GeminiService } from '../../shared/services/gemini.service';
import { LoaderService } from '../../shared/services/loader.service';
import { ToasterService } from '../../shared/services/toaster.service';
import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-code-reviewer',
  standalone: true,
  imports: [InputTextareaModule, SkeletonModule, CardModule, MarkdownModule, ReactiveFormsModule, FontAwesomeModule, RouterLink],
  templateUrl: './code-reviewer.component.html',
  styles: ``
})
export class CodeReviewerComponent {

  public options: KatexOptions = {
    displayMode: true,
    throwOnError: false,
    errorColor: 'red'
  };

  clipboard = ClipboardComponent;

  close = FAIcons.CLOSE;
  tick = FAIcons.TICK;

  reviewForm !: FormGroup;

  reviewResult: IReviewCodeResponse | null = null;


  constructor(private fb: FormBuilder, public loaderService: LoaderService, private geminiService: GeminiService, private toasterService: ToasterService) {

  }

  ngOnInit(): void {
    // initialize form
    this.reviewForm = this.fb.group({
      code: new FormControl("", [Validators.required])
    });
  }

  generateReview() {
    this.reviewResult = null; // remove any previous one

    this.loaderService.showLoader();

    this.geminiService.generateReview(this.reviewForm.value).subscribe({
      next: res => {
        const parsed = JSON.parse(res.responseMessage) as IReviewCodeResponse;
        this.loaderService.hideLoader();
        this.toasterService.showSuccess("Code Reviewed", "Code reviewed successfully.");
        this.reviewResult = parsed;
      },
      error: err => {
        this.loaderService.hideLoader();
        this.toasterService.showError("Code Review Failed", "Code review failed, please try again after few minutes.");
      }
    });

    this.reviewForm.reset();
  }
}
