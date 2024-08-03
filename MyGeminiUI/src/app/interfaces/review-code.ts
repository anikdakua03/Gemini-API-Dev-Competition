export interface IReviewCode {
    code: string
}

export interface IReviewCodeResponse {
    code: string
    possibleBugs: string[]
    summary: string
    language: string
    performance: IPerformance
    readability: string
    scalability: string
    security: string
    errorHandling: string
    conclusion: string
    additionalComment: string
}

interface IPerformance {
    timeComplexity: string
    spaceComplexity: string
}