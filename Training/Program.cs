// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to execute WORDLE in Console
// --------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

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
   private GameWord gameWord = new ("ALGAE"/*WordList.RandomWord ()*/);
   public List<Guess> Guesses { get; } = new List<Guess> ();
   int guessesLeft = 6;
   string currentInput = "";
   static Dictionary<char, ConsoleColor> alpColor;
   static readonly int boardWidth = 26;
   static readonly int boardHeight = 19;
   static int startX = WindowWidth / 2 - boardWidth;

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
      int boardWidth = 26;
      int boardHeight = 19;
      int startX = WindowWidth / 2 - boardWidth;
      int startY = Math.Abs ((WindowHeight / 2) - boardHeight);
      for (int row = 0; row < 6; row++) {
         SetCursorPosition (startX, startY);
         if (row == 0) {
            Write ("┌");
            for (int col = 0; col < 4; col++) Write ("───┬");
            WriteLine ("───┐");
            startY++;
         }
         SetCursorPosition (startX, startY);
         Write ("│");
         for (int col = 0; col < 4; col++) Write (" · │");
         WriteLine (" · │");
         if (row < 5) {
            SetCursorPosition (startX, ++startY);
            Write ("├");
            for (int col = 0; col < 4; col++) {
               Write ("───┼");
            }
            WriteLine ("───┤");
         }
         ++startY;
      }
      SetCursorPosition (startX, startY);
      Write ("└");
      for (int col = 0; col < 4; col++) Write ("───┴");
      WriteLine ("───┘");
      SetCursorPosition (startX - 2, startY + 1);
      WriteLine ("──────────────────────────");
      int i = 0;
      for (char c = 'A'; c <= 'Z'; c++) {
         SetCursorPosition (startX + (i % 7) * 4 - 3, startY + 2);
         Write ($" {c}");
         i++;
         if (i % 7 == 0) startY ++;
      }
      SetCursorPosition (startX - 2, startY + 3);
      Write ("──────────────────────────");
   }

   /// <summary>Checks if a given guess is correct</summary>
   /// <param name="guess">The <see cref="Guess"/> to be checked</param>
   /// <returns>True if the guess is correct; otherwise, false</returns>
   static bool IsGuessCorrect (Guess guess) => guess.State.All (state => state == GameStates.CORRECT);

   /// <summary> Updates the color of letters on the game board based on the guess </summary>
   /// <param name="row">The row on the game board to be updated</param>
   /// <param name="guess">The guess containing information about correct and incorrect letters</param>
   static void UpdateColor (int row, Guess guess) {
      int startingCol = startX + 1;
      for (int col = 0; col < guess.State.Length; col++) {
         SetCursorPosition (startingCol + col * 4, row + 2);
         ConsoleColor color = WordleHelpers.StateColor (guess.State[col]);
         if (guess.State[col] != GameStates.NONE) {
            char currentChar = guess.Word[col];
            if (alpColor.TryGetValue (guess.Word[col], out ConsoleColor existingColor)) {
               if (WordleHelpers.State (color) > WordleHelpers.State (existingColor)) alpColor[guess.Word[col]] = color;
               ForegroundColor = color;
               Write ($" {currentChar} ");
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
      int startingCol = startX - 3;
      int chRow = 19, i = 0;
      foreach (var entry in charColorDictionary) {
         SetCursorPosition (startingCol + 1, chRow);
         ForegroundColor = entry.Value;
         if (ForegroundColor == ConsoleColor.DarkGray) ForegroundColor = BackgroundColor;
         Write (entry.Key + "  ");
         ResetColor ();
         i++;
         if (i % 7 == 0) {
            WriteLine ("\n");
            startingCol = startX - 3;
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
         SetCursorPosition (currentCol + startX - 3, currentRow + 3);
         Write ($" {keyInfo.KeyChar.ToString ().ToUpper ()}");
      } else if (keyInfo.Key == ConsoleKey.Enter && currentInput.Length == 5) {
         Guess guess = MakeGuess (currentInput.ToUpper ());
         if (WordList.IsWord (currentInput)) {
            UpdateColor (currentRow + 1, guess);
            if (IsGuessCorrect (guess)) {
               currentRow = 0;
               WriteLine ("\n");
            }
            currentInput = "";
            currentRow += 2;
            guessesLeft--;
         } else {
            SetCursorPosition (startX + 3, 25);
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine ($"{currentInput.ToUpper ()} is invalid");
            ResetColor ();
         }
      } else if (keyInfo.Key == ConsoleKey.Backspace && currentInput.Length > 0) {
         SetCursorPosition (currentCol, currentRow + 3);
         if (currentCol <= startX + boardWidth - 8) Write ($" · ");
         currentInput = currentInput[..^1];
         currentCol = currentInput.Length * 4 + 1;
         SetCursorPosition (startX, boardHeight + 6);
         WriteLine ("                         ");
      }
   }

   /// <summary> Prints the result of the Wordle game, indicating whether the player won or lost</summary>
   void PrintResult () {
      SetCursorPosition (startX - 4, boardHeight + 5);
      if (GameOver () && Guesses.Any (IsGuessCorrect)) {
         ForegroundColor = ConsoleColor.Green;
         WriteLine ($"You found the word in {6 - guessesLeft} tries");
         ResetColor ();
      } else if (GameOver () && !Guesses.Any (IsGuessCorrect)) {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ($"Sorry, you lost.The word was:{gameWord.Word} ");
         ResetColor ();
      } else WriteLine ();
   }

   /// <summary>Checks if the game is over based on the number of remaining guesses and correct guesses</summary>
   bool GameOver () => guessesLeft <= 0 || Guesses.Any (IsGuessCorrect);

   /// <summary>Runs the Wordle game, allowing the player to make guesses and play the game</summary>
   public void Run () {
      OutputEncoding = Encoding.UTF8;
      do {
         Clear ();
         CursorVisible = false;
         SetCursorPosition (startX + 6, boardWidth - 25);
         WriteLine ("WORDLE\n");
         int currentRow = 3;
         DisplayBoard ();
         alpColor = GetAlpColorDictionary ();
         while (!GameOver ()) {
            int currentCol = startX + (currentInput.Length * 4) + 1;
            SetCursorPosition (currentCol, currentRow + 3);
            if (currentCol <= startX + boardWidth - 8) Write ($" ◌ ");
            ConsoleKeyInfo keyInfo = ReadKey (true);
            UpdateGameState (keyInfo, ref currentRow, ref currentCol);
            SetCursorPosition (currentCol + 4, currentRow + 3);
         }
         PrintResult ();
      } while (PlayAgain ());
   }

   /// <summary>Asks the player if they want to play the game again</summary>
   /// <returns>True if the player wants to play again, otherwise false</returns>
   bool PlayAgain () {
      SetCursorPosition (startX - 4, boardHeight + 8);
      WriteLine ("Do you want to continue? (Y/N)");
      ConsoleKeyInfo key = ReadKey (true);
      if (key.Key == ConsoleKey.Y) {
         Guesses.Clear ();
         guessesLeft = 6;
         currentInput = "";
         gameWord = new GameWord (WordList.RandomWord ());
         return true;
      } else return false;
   }
}
class Program {
   static void Main () {
      do {
         Clear ();
         CursorVisible = false;
         Wordle game = new ();
         game.Run ();
         ConsoleKeyInfo playAgainKey = ReadKey (true);
         if (playAgainKey.Key != ConsoleKey.Y) break;
      } while (true);
   }
}