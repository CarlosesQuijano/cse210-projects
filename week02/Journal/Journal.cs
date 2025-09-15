using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Journal
{
    private readonly List<Entry> _entries = new();

    public void AddEntry(Entry newEntry) => _entries.Add(newEntry);

    public void DisplayAll()
    {
        DisplayEntries(_entries);
    }

    public void DisplayByTag(string tag)
    {
        var filteredEntries = _entries.Where(e => 
            e._tags.Any(t => t.Equals(tag, StringComparison.OrdinalIgnoreCase))).ToList();
        
        if (filteredEntries.Count == 0)
        {
            Console.WriteLine($"No entries found with tag '{tag}'.");
            return;
        }

        Console.WriteLine($"Entries tagged with '{tag}':");
        Console.WriteLine(new string('-', 40));
        DisplayEntries(filteredEntries);
    }

    public void DisplayByMood(int mood)
    {
        var filteredEntries = _entries.Where(e => e._mood == mood).ToList();
        
        if (filteredEntries.Count == 0)
        {
            Console.WriteLine($"No entries found with mood {mood}/5.");
            return;
        }

        Console.WriteLine($"Entries with mood {mood}/5:");
        Console.WriteLine(new string('-', 40));
        DisplayEntries(filteredEntries);
    }

    private void DisplayEntries(List<Entry> entries)
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries yet. Write your first one!");
            return;
        }

        foreach (var e in entries)
        {
            e.Display();
        }
    }

    public void SaveToFile(string file)
    {
        using var output = new StreamWriter(file);
        
        // Write CSV header
        output.WriteLine("Date,Prompt,Entry,Mood,Weather,Tags");
        
        foreach (var e in _entries)
            output.WriteLine(e.ToStorageString());

        Console.WriteLine($"Saved {_entries.Count} entr{(_entries.Count == 1 ? "y" : "ies")} to '{file}'.");
    }

    public void LoadFromFile(string file)
    {
        if (!File.Exists(file))
        {
            Console.WriteLine("File not found.");
            return;
        }

        var lines = File.ReadAllLines(file);
        _entries.Clear();

        // Skip header if present
        int startIndex = 0;
        if (lines.Length > 0 && lines[0].StartsWith("Date,Prompt,Entry"))
        {
            startIndex = 1;
        }

        for (int i = startIndex; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            _entries.Add(Entry.FromStorageString(line));
        }

        Console.WriteLine($"Loaded {_entries.Count} entr{(_entries.Count == 1 ? "y" : "ies")} from '{file}'.");
    }

    public void ShowAllTags()
    {
        var allTags = _entries.SelectMany(e => e._tags).Distinct().OrderBy(t => t).ToList();
        
        if (allTags.Count == 0)
        {
            Console.WriteLine("No tags found in entries.");
            return;
        }

        Console.WriteLine("Available tags:");
        foreach (var tag in allTags)
        {
            var count = _entries.Count(e => e._tags.Contains(tag));
            Console.WriteLine($"  {tag} ({count} entr{(count == 1 ? "y" : "ies")})");
        }
    }
}
