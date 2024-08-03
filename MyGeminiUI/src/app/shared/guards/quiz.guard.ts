import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { ToasterService } from '../services/toaster.service';
import { GeminiService } from '../services/gemini.service';

export const quizGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const geminiService = inject(GeminiService);
  const router = inject(Router);
  const toasterService = inject(ToasterService);

  geminiService.allQss$.subscribe(res => {
    if (res.length < 5) {
      toasterService.showError("Error Playing Quiz", "Not having enough questions to play quiz.");
      router.navigateByUrl("/");
      return false;
    }

    toasterService.showSuccess("Quiz is Ready to Play", "Quiz will be starting soon.");
    return true;
  });

  return true;
};
