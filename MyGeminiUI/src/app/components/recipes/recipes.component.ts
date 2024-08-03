import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';
import { AccordionModule } from 'primeng/accordion';
import { CardModule } from 'primeng/card';
import { ChipsModule } from 'primeng/chips';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { BehaviorSubject } from 'rxjs';
import { IDropdown } from '../../interfaces/dropdown';
import { IRecipe, IRecipeItem } from '../../interfaces/recipe';
import { DISH_REGIONS, DISH_TYPES } from '../../shared/constants/recipe.constant';
import { LoaderService } from '../../shared/services/loader.service';
import { ToasterService } from '../../shared/services/toaster.service';
import { SkeletonModule } from 'primeng/skeleton';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FAIcons } from '../../shared/constants/font-awesome-icons';
import { GeminiService } from '../../shared/services/gemini.service';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [ReactiveFormsModule, ChipsModule, RadioButtonModule, CardModule, AccordionModule, DropdownModule, SkeletonModule, MarkdownModule, FontAwesomeModule, RouterLink],
  templateUrl: './recipes.component.html',
  styles: ``,
})
export class RecipesComponent implements OnInit {

  close = FAIcons.CLOSE;
  tick = FAIcons.TICK;

  recipeForm !: FormGroup;

  allRecipes$: BehaviorSubject<IRecipeItem[]> = new BehaviorSubject<IRecipeItem[]>([]);

  dishRegions: IDropdown[] = DISH_REGIONS;
  dishTypes: IDropdown[] = DISH_TYPES;

  generatedRecipes: IRecipeItem[] = [];

  constructor(private fb: FormBuilder, public loaderService: LoaderService, private geminiService: GeminiService, private toasterService: ToasterService) {
  }

  ngOnInit(): void {
    // initialize form
    this.recipeForm = this.fb.group({
      ingredients: new FormControl([], [Validators.required]),
      isVeg: new FormControl("", [Validators.required]),
      dishRegion: new FormControl("", [Validators.required])
    });
  }

  generateRecipe() {
    this.loaderService.showLoader();
    this.generatedRecipes = []; // clear existing
    const recipeReq: IRecipe = this.recipeForm.value;
    recipeReq.isVeg = this.recipeForm.value.isVeg.id === 'true' ? true : false;

    this.geminiService.generateRecipes(recipeReq).subscribe({
      next: res => {
        this.allRecipes$.next(res);
        this.generatedRecipes = res;
        this.loaderService.hideLoader();
        this.toasterService.showSuccess('Successfully Generated', "Recipes generated successfully.");
      },
      error: err => {
        this.loaderService.hideLoader();
        this.toasterService.showError('Error Generating', "Recipes generation failed.");
      }
    });
    // once form is submitted , reset it
    this.recipeForm.reset();
  }
}
