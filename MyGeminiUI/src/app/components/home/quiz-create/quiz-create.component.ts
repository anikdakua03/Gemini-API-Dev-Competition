import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { ToastModule } from 'primeng/toast';
import { IDropdown } from '../../../interfaces/dropdown';
import { IQuizCreate } from '../../../interfaces/quiz-create';
import { LoaderService } from '../../../shared/services/loader.service';
import { ToasterService } from '../../../shared/services/toaster.service';
import { CATEGORIES, QUESTION_LEVELS, QUESTION_TYPES } from '../../../shared/constants/question.const';
import { GeminiService } from '../../../shared/services/gemini.service';

@Component({
  selector: 'app-quiz-create',
  standalone: true,
  imports: [ButtonModule, DropdownModule, DialogModule, InputNumberModule, ToastModule, ReactiveFormsModule],
  templateUrl: './quiz-create.component.html',
  styles: ``
})
export class QuizCreateComponent implements OnInit {
  @Input() visible: boolean = false;

  @Output() closeModal: EventEmitter<boolean> = new EventEmitter<boolean>();

  categories: IDropdown[] = CATEGORIES;

  questionTypes: IDropdown[] = QUESTION_TYPES;

  questionLevels: IDropdown[] = QUESTION_LEVELS;

  quizCreationForm!: FormGroup;

  constructor(private fb: FormBuilder, private geminiService: GeminiService, private loaderService: LoaderService,
    private toasterService: ToasterService,
    private router: Router) {

    // initialize form
    this.quizCreationForm = fb.group({
      questionCount: new FormControl(5, [Validators.required, Validators.min(5), Validators.max(25)]),
      questionType: new FormControl("", [Validators.required]),
      categoryName: new FormControl("", [Validators.required]),
      questionLevel: new FormControl("", [Validators.required])
    });
  }

  ngOnInit(): void {
  }

  closeDialog() {
    this.closeModal.emit(true);
  }

  onQuizCreate() {
    this.visible = false;
    this.loaderService.showLoader();

    const quizCreateBody: IQuizCreate = {
      questionCount: this.quizCreationForm.value.questionCount,
      questionType: this.quizCreationForm.value.questionType.id ?? "Any",
      categoryName: this.quizCreationForm.value.categoryName.id ?? "only_one",
      questionLevel: this.quizCreationForm.value.questionLevel.id ?? "any"
    };

    this.geminiService.createQuiz(quizCreateBody).subscribe({
      next: res => {
        this.loaderService.hideLoader();

        this.geminiService.allQss$.next(this.shuffle(res));
        this.toasterService.showSuccess('Success', 'Quiz created successfully.');
        this.router.navigateByUrl("quiz");
        this.loaderService.hideLoader();
      },
      error: err => {
        this.toasterService.showError('Error', 'Unable to create quiz, please try again.');
        this.loaderService.hideLoader();
      }
    });

    this.quizCreationForm.reset();
  }

  // shuffle array elements
  shuffle(array: any[]) {
    for (let i = array.length - 1; i >= 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
  }

}
