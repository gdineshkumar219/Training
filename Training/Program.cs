// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------

using System.Text;

namespace Training;
public class Wordle{
   private GameWord gameWord ;
   public List<Guess> Guesses { get; } = new List<Guess>();
   int guessesLeft = 6;
   string currentInput = "";
   static Dictionary<char, ConsoleColor> alpColor;
   public Wordle (string newWord) {
      gameWord = new GameWord (newWord);
   }

   public Guess MakeGuess (string word) {
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
         for (int col = 0; col < 4; col++) Console.Write (" . │");
         Console.WriteLine (" . │");
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
      Console.SetCursorPosition (startX - 2, startY + 4);
      Console.Write ("──────────────────────────");
   }

   static bool IsGuessCorrect (Guess guess) {
      foreach (GameStates state in guess.State)
         if (state != GameStates.CORRECT) return false;
      return true;
   }

   static void UpdateColor (int row, Guess guess) {
      int startingCol = 36;
      for (int col = 0; col < guess.State.Length; col++) {
         Console.SetCursorPosition (startingCol + col * 4, row + 2);
         ConsoleColor color = WordleHelpers.StateColour (guess.State[col]);
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

   static Dictionary<char, ConsoleColor> GetAlpColorDictionary () {
      var colorAlp=new Dictionary<char, ConsoleColor>();
      for (char c = 'A'; c <= 'Z'; c++) colorAlp[c] = WordleHelpers.StateColour (GameStates.NONE);
      return colorAlp;
   }

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
         if (currentCol <= 22) Console.Write ($" . ");
         currentInput = currentInput[..^1];
         currentCol = currentInput.Length * 4 + 1;
         Console.SetCursorPosition (32, 25);
         Console.WriteLine ("                         ");
      }
   }
   void PrintResult () {
      Console.SetCursorPosition (34, 25);
      if (GameOver () && Guesses.Any (IsGuessCorrect)) {
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine ($"You found the word in {6 - guessesLeft} tries");
         Console.ResetColor ();
      } else if (GameOver () && !Guesses.Any (IsGuessCorrect)) {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ($"Sorry, you lost\n\t\t\t\t The word was:{gameWord} ");
         Console.ResetColor ();
      } else Console.WriteLine ();
   }
   bool GameOver () => guessesLeft <= 0 || Guesses.Any (IsGuessCorrect);
   public void Run () {
      Console.OutputEncoding = Encoding.UTF8;
      do {
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

   bool PlayAgain () {
      Console.SetCursorPosition (34, 28);
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
         Wordle game = new  Wordle(WordList.RandomWord ());
         game.Run ();
         Console.WriteLine ("Do you want to play again? (Y/N)");
         ConsoleKeyInfo playAgainKey = Console.ReadKey (true);
         if (playAgainKey.Key != ConsoleKey.Y) break;
      } while (true);
   }
}
