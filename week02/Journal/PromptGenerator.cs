using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private readonly List<string> _prompts = new()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "What challenged me today, and how did I respond?",
        "What am I grateful for right now?",
        "What is something small I noticed that felt meaningful?",
        "If I could redo one moment today, what would it be and why?",
        "Where did I show kindness (to self or others)?"
    };

    private readonly Random _rng = new();

    public string GetRandomPrompt()
    {
        int i = _rng.Next(_prompts.Count);
        return _prompts[i];
    }
}
