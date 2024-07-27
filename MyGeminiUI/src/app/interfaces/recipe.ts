export interface IRecipe {
    ingredients: string[],
    isVeg: boolean,
    dishRegion: string
}

export interface IRecipeItem {
    title: string,
    shortDescription: string,
    dishRegion: string
    isVeg: boolean,
    ingredients: string[],
    instructions: string,
    summary: string
}