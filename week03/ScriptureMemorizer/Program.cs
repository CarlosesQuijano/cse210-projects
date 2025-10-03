using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            ScriptureLibrary library = new ScriptureLibrary();
            library.LoadDefaultScriptures(); 

            Scripture selectedScripture = library.GetRandomScripture();
            
            while (!selectedScripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(selectedScripture.Display());
                Console.WriteLine("\nPress Enter to hide a word, or type 'quit' to exit:");
                
                string input = Console.ReadLine() ?? "";
                
                if (input.ToLower() == "quit")
                {
                    break;
                }
                
                selectedScripture.HideRandomWord();
            }
            
            if (selectedScripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(selectedScripture.Display());
                Console.WriteLine("\nCongratulations! You've memorized the scripture!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}