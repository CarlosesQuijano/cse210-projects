# ScriptureMemorizer Project

## Overview
The Scripture Memorizer project is a console application designed to help users memorize scriptures by displaying them and allowing users to hide random words. The program encourages engagement with the text and aids in memorization through interactive features.

## Installation Instructions
1. Clone the repository to your local machine using:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd ScriptureMemorizer
   ```
3. Restore the project dependencies:
   ```
   dotnet restore
   ```

## Usage Guidelines
1. Run the application using the following command:
   ```
   dotnet run --project src/ScriptureMemorizer.csproj
   ```
2. Follow the on-screen instructions to view scriptures and hide words.
3. To quit the program, follow the prompt provided in the console.

## Project Structure
- **src/Program.cs**: Entry point of the application.
- **src/Scripture.cs**: Contains the Scripture class for managing scripture text and references.
- **src/Reference.cs**: Defines the Reference class for scripture references.
- **src/Word.cs**: Represents individual words and methods to hide them.
- **src/ScriptureLibrary.cs**: Manages a collection of scriptures and random selection.
- **ScriptureMemorizer.csproj**: Project file containing configuration settings.