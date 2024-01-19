// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to execute WORDLE in Console
// --------------------------------------------------------------------------------------------
using System.Text;

namespace Training;

/// <summary>Represents the possible states of a letter in the Wordle game</summary>
public enum GameStates {
   NONE,
   ABSENT,
   PRESENT,
   CORRECT
}

/// <summary>
/// Represents a Wordle game with the functionality to make guesses and display the game board
/// </summary>
public class Wordle {
   private GameWord gameWord = new (WordList.RandomWord ());
   public List<Guess> Guesses { get; } = new List<Guess> ();
   int guessesLeft = 6;
   string currentInput = "";
   static Dictionary<char, ConsoleColor> alpColor;

   /// <summary> Makes a guess in the Wordle game</summary>
   /// <param name="word">The word guessed by the player</param>
   /// <returns>The <see cref="Guess"/> object representing the result of the guess </returns>
   Guess MakeGuess (string word) {
      GameStates[] guessState = new GameStates[word.Length];
      Dictionary<char, int> tempLetterCount = new (gameWord.UniqueCharCnt);
      for (int i = 0; i < word.Length; i++) {
         if (word[i] == gameWord.Word[i]) {
            guessState[i] = GameStates.CORRECT;
            if (tempLetterCount[word[i]] > 0) tempLetterCount[word[i]]--;
            else {
               for (int j = i - 1; j >= 0; j--) {
                  if ((word[i] == word[j]) && (guessState[j] == GameStates.PRESENT)) {
                     guessState[j] = GameStates.ABSENT;
                     break;
                  }
               }
            }
         } else if (tempLetterCount.TryGetValue (word[i], out int value) && value > 0) {
            guessState[i] = GameStates.PRESENT;
            tempLetterCount[word[i]] = --value;
         } else guessState[i] = GameStates.ABSENT;
      }
      Guess guess = new (word, guessState);
      Guesses.Add (guess);
      return guess;
   }

   /// <summary> Displays the Wordle game board on the console </summary>
   static void DisplayBoard () {
      int boardWidth = 29;
      int boardHeight = 17;
      int startX = (Console.WindowWidth - boardWidth) / 2;
      int startY = (Console.WindowHeight - boardHeight) / 2;
      for (int row = 0; row < 6; row++) {
         Console.SetCursorPosition (startX, startY);
         if (row == 0) {
            Console.Write ("┌");
            for (int col = 0; col < 4; col++) Console.Write ("───┬");
            Console.WriteLine ("───┐");
            startY++;
         }
         Console.SetCursorPosition (startX, startY);
         Console.Write ("│");
         for (int col = 0; col < 4; col++) Console.Write (" · │");
         Console.WriteLine (" · │");
         if (row < 5) {
            Console.SetCursorPosition (startX, ++startY);
            Console.Write ("├");
            for (int col = 0; col < 4; col++) {
               Console.Write ("───┼");
            }
            Console.WriteLine ("───┤");
         }
         ++startY;
      }
      Console.SetCursorPosition (startX, startY);
      Console.Write ("└");
      for (int col = 0; col < 4; col++) Console.Write ("───┴");
      Console.WriteLine ("───┘");
      Console.SetCursorPosition (startX - 2, startY + 1);
      Console.WriteLine ("──────────────────────────");
      int i = 0;
      for (char c = 'A'; c <= 'Z'; c++) {
         Console.SetCursorPosition (startX + (i % 7) * 4 - 3, startY + 2);
         Console.Write ($" {c}");
         i++;
         if (i % 7 == 0) startY += 1;
      }
      Console.SetCursorPosition (startX - 2, startY + 3);
      Console.Write ("──────────────────────────");
   }

   /// <summary>Checks if a given guess is correct</summary>
   /// <param name="guess">The <see cref="Guess"/> to be checked</param>
   /// <returns>True if the guess is correct; otherwise, false</returns>
   static bool IsGuessCorrect (Guess guess) {
      foreach (GameStates state in guess.State)
         if (state != GameStates.CORRECT) return false;
      return true;
   }

   /// <summary> Updates the color of letters on the game board based on the guess </summary>
   /// <param name="row">The row on the game board to be updated</param>
   /// <param name="guess">The guess containing information about correct and incorrect letters</param>
   static void UpdateColor (int row, Guess guess) {
      int startingCol = 36;
      for (int col = 0; col < guess.State.Length; col++) {
         Console.SetCursorPosition (startingCol + col * 4, row + 2);
         ConsoleColor color = WordleHelpers.StateColor (guess.State[col]);
         if (guess.State[col] != GameStates.NONE) {
            char currentChar = guess.Word[col];
            if (alpColor.TryGetValue (guess.Word[col], out ConsoleColor existingColor)) {
               if (WordleHelpers.State (color) > WordleHelpers.State (existingColor)) alpColor[guess.Word[col]] = color;
               Console.ForegroundColor = color;
               Console.Write ($" {currentChar} ");
            }
         }
      }
      PrintColoredAlphabets (alpColor);
   }

   /// <summary>Gets a dictionary mapping letters to their corresponding colors</summary>
   /// <returns>A dictionary mapping letters to colors.</returns>
   static Dictionary<char, ConsoleColor> GetAlpColorDictionary () {
      var colorAlp = new Dictionary<char, ConsoleColor> ();
      for (char c = 'A'; c <= 'Z'; c++) colorAlp[c] = WordleHelpers.StateColor (GameStates.NONE);
      return colorAlp;
   }

   /// <summary>Prints the colored alphabet on the console based on the given dictionary</summary>
   /// <param name="charColorDictionary">The dictionary mapping letters to colors</param>
   static void PrintColoredAlphabets (Dictionary<char, ConsoleColor> charColorDictionary) {
      int startingCol = 32;
      int chRow = 19, i = 0;
      foreach (var entry in charColorDictionary) {
         Console.SetCursorPosition (startingCol + 1, chRow);
         Console.ForegroundColor = entry.Value;
         Console.Write (entry.Key + "  ");
         Console.ResetColor ();
         i++;
         if (i % 7 == 0) {
            Console.WriteLine ("\n");
            startingCol = 32;
            chRow++;
         } else startingCol += 4;
      }
   }

   /// <summary>Updates the game state based on the player's input</summary>
   /// <param name="keyInfo">The key information representing the player's input</param>
   /// <param name="currentRow">The current row on the game board</param>
   /// <param name="currentCol">The current column on the game board</param>
   void UpdateGameState (ConsoleKeyInfo keyInfo, ref int currentRow, ref int currentCol) {
      if (char.IsLetter (keyInfo.KeyChar) && currentInput.Length < 5) {
         currentInput += keyInfo.KeyChar;
         currentCol = currentInput.Length * 4;
         Console.SetCursorPosition (currentCol + 32, currentRow + 3);
         Console.Write ($" {keyInfo.KeyChar.ToString ().ToUpper ()}");
      } else if (keyInfo.Key == ConsoleKey.Enter && currentInput.Length == 5) {
         Guess guess = MakeGuess (currentInput.ToUpper ());
         if (WordList.IsWord (currentInput)) {
            UpdateColor (currentRow + 1, guess);
            if (IsGuessCorrect (guess)) {
               currentRow = 0;
               Console.WriteLine ("\n");
            }
            currentInput = "";
            currentRow += 2;
            guessesLeft--;
         } else {
            Console.SetCursorPosition (32, 25);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ($"{currentInput} is invalid");
            Console.ResetColor ();
         }
      } else if (keyInfo.Key == ConsoleKey.Backspace && currentInput.Length > 0) {
         Console.SetCursorPosition (currentCol + 32, currentRow + 3);
         if (currentCol <= 22) Console.Write ($" · ");
         currentInput = currentInput[..^1];
         currentCol = currentInput.Length * 4 + 1;
         Console.SetCursorPosition (32, 25);
         Console.WriteLine ("                         ");
      }
   }
   /// <summary> Prints the result of the Wordle game, indicating whether the player won or lost</summary>
   void PrintResult () {
      Console.SetCursorPosition (33, 25);
      if (GameOver () && Guesses.Any (IsGuessCorrect)) {
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine ($"You found the word in {6 - guessesLeft} tries");
         Console.ResetColor ();
      } else if (GameOver () && !Guesses.Any (IsGuessCorrect)) {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ($"Sorry, you lost.The word was:{gameWord.Word} ");
         Console.ResetColor ();
      } else Console.WriteLine ();
   }

   /// <summary>Checks if the game is over based on the number of remaining guesses and correct guesses</summary>
   bool GameOver () => guessesLeft <= 0 || Guesses.Any (IsGuessCorrect);


   /// <summary>Runs the Wordle game, allowing the player to make guesses and play the game</summary>
   public void Run () {
      Console.OutputEncoding = Encoding.UTF8;
      do {
         Console.CursorVisible = false;
         Console.Clear ();
         Console.WriteLine ("\n\t\t\t\t\t  WORDLE");
         Console.WriteLine ();
         int currentRow = 3;
         DisplayBoard ();
         alpColor = GetAlpColorDictionary ();
         while (!GameOver ()) {
            int currentCol = (currentInput.Length * 4) + 4;
            Console.SetCursorPosition (currentCol + 32, currentRow + 3);
            if (currentCol <= 22) Console.Write ($" ◌ ");
            ConsoleKeyInfo keyInfo = Console.ReadKey (true);
            UpdateGameState (keyInfo, ref currentRow, ref currentCol);
            Console.SetCursorPosition (currentCol, currentRow + 3);
         }
         PrintResult ();
      } while (PlayAgain ());
   }

   /// <summary>Asks the player if they want to play the game again</summary>
   /// <returns>True if the player wants to play again, otherwise false</returns>
   bool PlayAgain () {
      Console.SetCursorPosition (33, 28);
      Console.WriteLine ("Do you want to continue? (Y/N)");
      ConsoleKeyInfo key = Console.ReadKey (true);
      if (key.Key == ConsoleKey.Y) {
         Guesses.Clear ();
         guessesLeft = 6;
         currentInput = "";
         return true;
      } else return false;
   }
}
class Program {
   static void Main () {
      do {
         Console.Clear ();
         Wordle game = new ();
         game.Run ();
         Console.WriteLine ("Do you want to play again? (Y/N)");
         ConsoleKeyInfo playAgainKey = Console.ReadKey (true);
         if (playAgainKey.Key != ConsoleKey.Y) break;
      } while (true);
   }
}