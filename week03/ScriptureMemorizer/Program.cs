using System;

/*
 * Scripture Memorizer Program
 * 
 * CORE REQUIREMENTS EXCEEDED:
 * 1. Scripture Library: Instead of a single scripture, this program manages multiple scriptures
 *    through the ScriptureLibrary class, allowing random selection.
 * 
 * 2. File Loading: The program can load scriptures from external files using the 
 *    LoadScriptures(filePath) method, making it extensible.
 * 
 * 3. Smart Word Hiding: Only visible words are selected for hiding (stretch challenge),
 *    preventing already hidden words from being selected again.
 * 
 * 4. Enhanced User Experience: Clear screen functionality and congratulations message
 *    provide better interaction.
 * 
 * 5. Error Handling: Try-catch blocks and proper validation ensure robust operation.
 */

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Initialize scripture library with multiple scriptures
            ScriptureLibrary library = new ScriptureLibrary();
            library.LoadDefaultScriptures(); 

            // Randomly select a scripture from the library
            Scripture selectedScripture = library.GetRandomScripture();
            
            // Continue until all words are hidden or user quits
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
                
                // Hide only visible words (stretch challenge implementation)
                selectedScripture.HideRandomWord();
            }
            
            // Congratulate user when memorization is complete
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