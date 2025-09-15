using System;

class Program
{
    static void Main()
    {
        var journal = new Journal();
        var prompts = new PromptGenerator();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1) Write a new entry");
            Console.WriteLine("2) Display all entries");
            Console.WriteLine("3) Filter by tag");
            Console.WriteLine("4) Filter by mood");
            Console.WriteLine("5) Show all tags");
            Console.WriteLine("6) Save to file");
            Console.WriteLine("7) Load from file");
            Console.WriteLine("8) Quit");
            Console.Write("Choose an option (1-8): ");

            var choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteEntry(journal, prompts);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    Console.Write("Enter tag to filter by: ");
                    journal.DisplayByTag(Console.ReadLine() ?? "");
                    break;
                case "4":
                    Console.Write("Enter mood to filter by (1-5): ");
                    if (int.TryParse(Console.ReadLine(), out int mood) && mood >= 1 && mood <= 5)
                        journal.DisplayByMood(mood);
                    else
                        Console.WriteLine("Please enter a number between 1 and 5.");
                    break;
                case "5":
                    journal.ShowAllTags();
                    break;
                case "6":
                    Console.Write("Filename to save (e.g., journal.csv): ");
                    journal.SaveToFile(Console.ReadLine() ?? "journal.csv");
                    break;
                case "7":
                    Console.Write("Filename to load (e.g., journal.csv): ");
                    journal.LoadFromFile(Console.ReadLine() ?? "journal.csv");
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Please choose 1–8.");
                    break;
            }
        }
    }

    private static void WriteEntry(Journal journal, PromptGenerator prompts)
    {
        string prompt = prompts.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your entry: ");
        string text = Console.ReadLine() ?? string.Empty;

        Console.Write("How are you feeling? (1=😢 2=😕 3=😐 4=😊 5=😁): ");
        int mood = 3;
        if (int.TryParse(Console.ReadLine(), out int moodInput) && moodInput >= 1 && moodInput <= 5)
            mood = moodInput;

        Console.Write("Weather (optional): ");
        string weather = Console.ReadLine() ?? "";

        Console.Write("Tags (comma-separated, optional): ");
        var tagInput = Console.ReadLine() ?? "";
        var tags = tagInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(t => t.Trim())
                          .Where(t => !string.IsNullOrEmpty(t))
                          .ToList();

        string date = DateTime.Now.ToShortDateString();
        journal.AddEntry(new Entry(date, prompt, text, mood, weather, tags));

        Console.WriteLine("Entry recorded with metadata!");
    }
}
