using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        _random = new Random();
        
        string[] wordArray = text.Split(' ');
        foreach (string word in wordArray)
        {
            _words.Add(new Word(word));
        }
    }

    public string Display()
    {
        string scriptureText = string.Join(" ", _words.Select(word => word.Display()));
        return $"{_reference.Display()} {scriptureText}";
    }

    public void HideRandomWord()
    {
        List<Word> visibleWords = _words.Where(word => !word.IsHidden()).ToList();
        
        if (visibleWords.Count > 0)
        {
            int randomIndex = _random.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
        }
    }

    public bool AllWordsHidden()
    {
        return _words.All(word => word.IsHidden());
    }
}