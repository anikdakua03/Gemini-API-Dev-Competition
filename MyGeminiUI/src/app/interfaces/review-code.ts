export interface IReviewCode {
    code: string
}

export interface IReviewCodeResponse {
    Code: string
    PossibleBugs: string[]
    Summary: string
    Language: string
    Performance: IPerformance
    Readability: string
    Scalability: string
    Security: string
    ErrorHandling: string
    Conclusion: string
    AdditionalComment: string
}

interface IPerformance {
    TimeComplexity: string
    SpaceComplexity: string
}