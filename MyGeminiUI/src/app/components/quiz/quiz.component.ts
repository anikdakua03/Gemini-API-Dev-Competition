import { NgClass } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { KatexOptions, MarkdownModule } from 'ngx-markdown';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { timer } from 'rxjs';
import { IDropdown } from '../../interfaces/dropdown';
import { IQuizQuestionResponse } from '../../interfaces/quiz-question';
import { IQuizResult } from '../../interfaces/quiz-result';
import { FAIcons } from '../../shared/constants/font-awesome-icons';
import { QuestionTypes } from '../../shared/constants/question.const';
import { CryptingService } from '../../shared/services/crypting.service';
import { LoaderService } from '../../shared/services/loader.service';
import { QuizService } from '../../shared/services/quiz.service';

@Component({
  selector: 'app-quiz',
  standalone: true,
  imports: [FontAwesomeModule, NgClass, CheckboxModule, MarkdownModule, ReactiveFormsModule, RadioButtonModule],
  templateUrl: './quiz.component.html',
  styles: ``
})
export class QuizComponent implements OnInit {
  public options: KatexOptions = {
    displayMode: true,
    throwOnError: false,
    errorColor: 'red',
  };

  q = FAIcons.Q;
  stopwatch = FAIcons.STW;
  leftArr = FAIcons.LONG_LEFT_ARROW;
  rightArr = FAIcons.LONG_RIGHT_ARROW;
  close = FAIcons.CLOSE;
  tick = FAIcons.TICK;
  dots = FAIcons.ELLIPSES;

  isQuizStarted: boolean = false;
  openQuizScore: boolean = false;
  isLoading: boolean = false;
  optionSelected: boolean = false;

  questionList: IQuizQuestionResponse[] = [];

  currentQuestion: number = 0;

  userQuizScore: number = 0;

  totalTimeTaken: number = 0;

  quizScore: IQuizResult = {
    totalQs: 0,
    totalCorrect: 0,
    totalScore: 0,
    totalTime: "",
    percentage: 0,
    hasPassed: false
  };

  seconds: number = 0;
  minutes: number = 0;
  hours: number = 0;
  timer$: any;

  questionTypes: IDropdown[] = QuestionTypes;

  userAnswers = new Map<number, { answer: Set<string> }>();

  onlyOneAnsForm !: FormGroup;
  multipleAnsForm !: FormGroup;

  constructor(private fb: FormBuilder, private cryptingService: CryptingService, private quizService: QuizService, private router: Router, private loaderService: LoaderService) {

    this.onlyOneAnsForm = this.fb.group({
      answer: new FormControl("")
    });

    this.multipleAnsForm = this.fb.group({
      answers: new FormControl([])
    });
  }

  ngOnInit(): void {
    this.isQuizStarted = true;
    this.startTimer();

    // get those quiz question from service
    this.quizService.allQss$.subscribe(res => {
      this.questionList = res;
    });

    if (this.questionList.length < 5) {
      // return to create quiz with more qs or other category
      this.router.navigateByUrl("/");
      // this.toaster.info("Please try other category or changing some options.", "Insufficient no. of questions.");
    }
  }


  ngOnDestroy() {
    this.stopTimer();
  }

  startTimer() {
    if (this.isQuizStarted) {
      // will wait for 2 seconds then each 1 sec after will do operation
      this.timer$ = timer(1000, 1000).subscribe((res) => {
        // now need to break this count into hour minutes seconds
        this.seconds = Math.floor(res % 60);
        this.minutes = Math.floor(res / 60);
        this.hours = Math.floor(res / 3600);
      });

    }
  }

  stopTimer() {
    this.timer$.unsubscribe();
  }

  resetTimer() {
    this.stopTimer();
    this.seconds = 0;
    this.minutes = 0;
    this.hours = 0;
  }

  formatTime(value: number) {
    return value.toString().padStart(2, '0'); // Add leading zero for single digits
  }

  formatTotalTime(hr: number, min: number, seconds: number): string {
    if (hr <= 0) {
      return `${this.formatTime(min)} minutes ${this.formatTime(seconds)} seconds`;
    }
    return `${this.formatTime(hr)} hours ${this.formatTime(min)} minutes ${this.formatTime(seconds)} seconds`;
  }


  getQsTypeValue(typeId: string): string {
    const matchingType = this.questionTypes.find(type => type.id === typeId.toLowerCase());
    return matchingType ? matchingType.value : 'Any';
  }

  onQuizSubmit() {
    this.isQuizStarted = false;
    this.stopTimer();
    this.loaderService.showLoader();

    for (const qs of this.questionList) {
      const userAns = this.userAnswers.get(qs.questionId);
      if (userAns === undefined || userAns.answer === null) {
        // skipped that qs
        continue;
      }
      else if (userAns) {
        if (userAns.answer.size > 1) {
          let userAnsCount: number = 0;
          qs.correctAnswers.forEach(ans => {
            if (userAns.answer.has(this.cryptingService.decryptText(ans))) {
              userAnsCount++;
            }
          });

          if (userAnsCount === qs.correctAnswers.length) {
            this.userQuizScore++;
          }
        }
        else if (userAns.answer.size == 1 && userAns.answer.has(this.cryptingService.decryptText(qs.correctAnswers[0]))) {
          this.userQuizScore++;
        }
      }
    }

    this.isLoading = false;
    this.quizScore.totalQs = this.questionList.length;
    this.quizScore.totalCorrect = this.userQuizScore;
    this.quizScore.totalScore = this.userQuizScore;
    this.quizScore.totalTime = this.formatTotalTime(this.hours, this.minutes, this.seconds);
    this.quizScore.percentage = (this.quizScore.totalCorrect * 100) / this.quizScore.totalQs;
    this.quizScore.hasPassed = this.quizScore.percentage >= 80 ? true : false;
    this.openQuizScore = true;
    // now also reset the timer
    this.loaderService.hideLoader();
    this.resetTimer();
  }


  showAnswers() {
    // not implemented, TODO :
  }

  closeModal() {
    this.openQuizScore = false;
    this.router.navigate(['']);
  }

  nextQuestionAndSubmit(currQs: IQuizQuestionResponse) {
    const a = this.userAnswers.get(currQs.questionId);
    const option = this.onlyOneAnsForm.value.answer;
    const options = this.multipleAnsForm.value;

    if (option !== undefined && option) {
      if (a === undefined) {
        const an = new Set<string>().add(option);
        this.userAnswers.set(currQs.questionId, { answer: an });
      }
      else {
        a.answer.add(option);
      }
    }
    else if (option !== undefined && options) {
      if (a === undefined) {
        const an = new Set<string>().add(option);
        this.userAnswers.set(currQs.questionId, { answer: an });
      }
      else {
        a.answer.add(option);
      }
    }

    if (options.answers !== null && options.answers.length > 0) {
      let answers = new Set<string>();
      options.answers.forEach((ans: string) => answers.add(ans));
      this.userAnswers.set(currQs.questionId, { answer: answers });
    }

    this.onlyOneAnsForm.reset();
    this.multipleAnsForm.reset();

    // if it is last quest submitted then check for answers
    if (this.userAnswers.size === this.questionList.length) {
      this.onQuizSubmit();
      return;
    }

    // next question
    this.currentQuestion++;
  }
}
