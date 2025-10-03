using System;
using System.Collections.Generic;
using System.IO;

public class ScriptureLibrary
{
    private List<Scripture> _scriptures;
    private Random _random;

    public ScriptureLibrary()
    {
        _scriptures = new List<Scripture>();
        _random = new Random();
    }

    public void LoadDefaultScriptures()
    {
        _scriptures.Add(new Scripture(
            new Reference("John", 3, 16),
            "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."
        ));

        _scriptures.Add(new Scripture(
            new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."
        ));

        _scriptures.Add(new Scripture(
            new Reference("1 Nephi", 3, 7),
            "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."
        ));
    }

    public void LoadScriptures(string filePath)
    {
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                {
                    var referenceParts = parts[0].Split(' ');
                    var book = referenceParts[0];
                    var chapter = int.Parse(referenceParts[1].Split(':')[0]);
                    var verse = int.Parse(referenceParts[1].Split(':')[1]);
                    var reference = new Reference(book, chapter, verse);
                    var text = parts[1];
                    _scriptures.Add(new Scripture(reference, text));
                }
            }
        }
        else
        {
            Console.WriteLine("File not found: " + filePath);
        }
    }

    public Scripture GetRandomScripture()
    {
        if (_scriptures.Count == 0)
        {
            throw new InvalidOperationException("No scriptures loaded in the library.");
        }

        int randomIndex = _random.Next(_scriptures.Count);
        return _scriptures[randomIndex];
    }
}