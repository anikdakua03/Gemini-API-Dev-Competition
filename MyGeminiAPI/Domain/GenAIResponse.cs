﻿namespace MyGeminiAPI.Domain;

public class GenAIResponse
{
    public string? ResponseMessage { get; set; }
    public string? ErrorMessage { get; set; }
    public bool? Succeed { get; set; }
}
