import { DatePipe } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { KatexOptions, MarkdownModule } from 'ngx-markdown';
import { Content, Conversation } from '../../interfaces/conversation';
import { ClipboardComponent } from '../../shared/components/clipboard/clipboard.component';
import { FAIcons } from '../../shared/constants/font-awesome-icons';
import { GeminiService } from '../../shared/services/gemini.service';
import { LoaderService } from '../../shared/services/loader.service';
import { ToasterService } from '../../shared/services/toaster.service';
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

@Component({
  selector: 'app-conversation',
  standalone: true,
  imports: [ReactiveFormsModule, FontAwesomeModule, MarkdownModule, DatePipe],
  templateUrl: './conversation.component.html',
  styles: ``
})
export class ConversationComponent {
  currDate: Date = new Date();
  @ViewChild("scrollMe") scrollContainer!: ElementRef;
  @ViewChild('myTextArea') myTextArea!: ElementRef;

  public options: KatexOptions = {
    displayMode: true,
    throwOnError: false,
    errorColor: 'red',
  };

  readonly clipBoardButton = ClipboardComponent;

  response: string = "";
  allChats: Content[] = [];
  sendPaperPlane = FAIcons.PAPER_PLANE;

  sendChat: Conversation = {
    contents: this.allChats
  };

  isLoading: boolean = false;

  inputForm: FormGroup = new FormGroup({
    inputText: new FormControl("", [Validators.required, Validators.minLength(4)]),
  });


  constructor(public geminiService: GeminiService, private loaderService: LoaderService, private toasterService: ToasterService) {
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  // this will keep chat the end by scrolling if any things occurred on the view
  scrollToBottom() {
    const el: HTMLDivElement = this.scrollContainer.nativeElement;
    el.scrollTop = el.scrollHeight;
  }


  // for auto adjusting textarea heights
  onInput(event: Event) {
    const textArea = event.target as HTMLTextAreaElement;
    const borderBoxHeight = (textArea.offsetHeight - textArea.clientHeight) + 'px';
    textArea.style.height = 'auto';
    textArea.style.height = (textArea.scrollHeight + parseInt(borderBoxHeight)) + 'px';
  }

  getAnswer() {
    if (this.inputForm.valid) {
      const data = { role: "user", parts: [{ text: this.inputForm.value.inputText }] } as Content;
      this.allChats.push(data);
      this.isLoading = true;
      this.clearAll();
      this.geminiService.conversationAndAnswer(this.sendChat)
        .subscribe(
          {
            next: res => {
              this.response = res.responseMessage;
              // fill the chat
              const mod = { role: "model", parts: [{ text: res.responseMessage }] } as Content;
              this.allChats.push(mod);
              this.isLoading = false;
              this.toasterService.showSuccess("Got Response", "Successfully got some response.")
            },
            error: err => {
              this.toasterService.showError("Error Getting Response", "Please try again in some minutes.")
              this.isLoading = false;
            }
          }
        );
    }
  }

  clearAll() {
    this.inputForm.reset();
  }
}
