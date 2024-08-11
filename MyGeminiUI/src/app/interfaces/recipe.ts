export interface IRecipe {
    ingredients: string[],
    isVeg: boolean,
    dishRegion: string
}

export interface IRecipeItem {
    Title: string,
    ShortDescription: string,
    DishRegion: string
    IsVeg: boolean,
    Ingredients: string[],
    Instruction: string,
    Summary: string
}