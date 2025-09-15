using System;
using System.Collections.Generic;
using System.Linq;

public class Entry
{
    public string _date;
    public string _promptText;
    public string _entryText;
    public int _mood; 
    public string _weather;
    public List<string> _tags;

    public Entry(string date, string promptText, string entryText, int mood = 3, string weather = "", List<string> tags = null)
    {
        _date = date;
        _promptText = promptText;
        _entryText = entryText;
        _mood = mood;
        _weather = weather;
        _tags = tags ?? new List<string>();
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_promptText}");
        Console.WriteLine($"Entry: {_entryText}");
        Console.WriteLine($"Mood: {GetMoodEmoji(_mood)} ({_mood}/5)");
        if (!string.IsNullOrEmpty(_weather))
            Console.WriteLine($"Weather: {_weather}");
        if (_tags.Count > 0)
            Console.WriteLine($"Tags: {string.Join(", ", _tags)}");
        Console.WriteLine();
    }

    private string GetMoodEmoji(int mood)
    {
        return mood switch
        {
            1 => "😢",
            2 => "😕",
            3 => "😐",
            4 => "😊",
            5 => "😁",
            _ => "😐"
        };
    }

    public string ToStorageString()
    {
        string csvDate = EscapeCsv(_date);
        string csvPrompt = EscapeCsv(_promptText);
        string csvEntry = EscapeCsv(_entryText);
        string csvWeather = EscapeCsv(_weather);
        string csvTags = EscapeCsv(string.Join(";", _tags));
        
        return $"{csvDate},{csvPrompt},{csvEntry},{_mood},{csvWeather},{csvTags}";
    }

    public static Entry FromStorageString(string line)
    {
        var parts = ParseCsvLine(line);
        if (parts.Count < 6) throw new ArgumentException("Invalid entry format");

        var tags = string.IsNullOrEmpty(parts[5]) 
            ? new List<string>() 
            : parts[5].Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

        return new Entry(parts[0], parts[1], parts[2], int.Parse(parts[3]), parts[4], tags);
    }

    private string EscapeCsv(string value)
    {
        if (string.IsNullOrEmpty(value)) return "\"\"";
        return $"\"{value.Replace("\"", "\"\"")}\"";
    }

    private static List<string> ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        bool inQuotes = false;
        string currentField = "";
        
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    currentField += '"';
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(currentField);
                currentField = "";
            }
            else
            {
                currentField += c;
            }
        }
        
        fields.Add(currentField);
        return fields;
    }
}
