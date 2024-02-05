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

#region Enums ----------------------------------------------------------------------------------
/// <summary>Represents the possible states of a letter in the Wordle game</summary>
public enum GameStates {
   NONE,
   ABSENT,
   PRESENT,
   CORRECT
}
#endregion

#region Wordle ---------------------------------------------------------------------------------
/// <summary>
/// Represents a Wordle game with the functionality to make guesses and display the game board
/// </summary>
public class Wordle {
   #region Methods -----------------------------------------------------------------------------
   /// <summary> Makes a guess in the Wordle game</summary>
   /// <param name="word">The word guessed by the player</param>
   /// <returns>The <see cref="Guess"/> object representing the result of the guess </returns>
   Guess MakeGuess (string word) {
      GameStates[] guessState = new GameStates[word.Length];
      Dictionary<char, int> tempLetterCount = new (mGameWord.UniqueCharCnt);
      for (int i = 0; i < word.Length; i++) {
         if (word[i] == mGameWord.Word[i]) {
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
      mGuesses.Add (guess);
      return guess;
   }

   /// <summary> Displays the Wordle game board on the console </summary>
   void DisplayBoard () {
      int currentRow = mCurrentRow;
      for (int row = 0; row < mGuessesLeft; row++) {
         SetCursorPosition (startX, mCurrentRow);
         if (row == 0) {
            Write ("┌");
            for (int col = 0; col < 4; col++) Write ("───┬");
            WriteLine ("───┐");
            mCurrentRow++;
         }
         SetCursorPosition (startX, mCurrentRow);
         Write ("│");
         for (int col = 0; col < mWordLen; col++) Write (" · │");
         if (row < 5) {
            SetCursorPosition (startX, ++mCurrentRow);
            Write ("├");
            for (int col = 0; col < 4; col++) Write ("───┼");
            WriteLine ("───┤");
         }
         ++mCurrentRow;
      }
      SetCursorPosition (startX, mCurrentRow);
      Write ("└");
      for (int col = 0; col < 4; col++) Write ("───┴");
      WriteLine ("───┘");
      SetCursorPosition (startX - 2, mCurrentRow + 1);
      WriteLine ("──────────────────────────");
      int i = 0;
      for (char c = 'A'; c <= 'Z'; c++) {
         SetCursorPosition (startX + (i % 7) * mCellWidth - 3, mCurrentRow + 2);
         Write ($" {c}");
         i++;
         if (i % 7 == 0) mCurrentRow++;
      }
      SetCursorPosition (startX - 2, mCurrentRow + 3);
      Write ("──────────────────────────");
      mCurrentRow = currentRow + 1; // goes to next row from initial row
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
         SetCursorPosition (startingCol + col * mCellWidth, row);
         ConsoleColor color = WordleHelpers.StateColor (guess.State[col]);
         if (guess.State[col] != GameStates.NONE) {
            char currentChar = guess.Word[col];
            if (alpColor.TryGetValue (guess.Word[col], out ConsoleColor existingColor)) {
               if (WordleHelpers.State (color) > WordleHelpers.State (existingColor)) alpColor[guess.Word[col]] = color;
               BackgroundColor = color;
               ForegroundColor = ConsoleColor.Black;
               Write ($" {currentChar} ");
               ResetColor ();
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
      int startingCol = startX - 2;
      int chRow = mBoardHeight + 2, i = 0;
      foreach (var entry in charColorDictionary) {
         SetCursorPosition (startingCol, chRow);
         ForegroundColor = entry.Value;
         if (ForegroundColor == ConsoleColor.DarkGray) ForegroundColor = BackgroundColor;
         Write (entry.Key + "  ");
         ResetColor ();
         i++;
         if (i % 7 == 0) {
            WriteLine ("\n");
            startingCol = startX - 2;
            chRow++;
         } else startingCol += mCellWidth;
      }
   }

   /// <summary>Updates the game state based on the player's input</summary>
   /// <param name="keyInfo">The key information representing the player's input</param>
   /// <param name="mCurrentRow">The current row on the game board</param>
   /// <param name="mCurrentCol">The current column on the game board</param>
   void UpdateGameState (ConsoleKeyInfo keyInfo) {
      switch (keyInfo.Key) {
         case ConsoleKey.Enter when mCurrentInput.Length == mWordLen:
            Guess guess = MakeGuess (mCurrentInput.ToUpper ());
            if (WordList.IsWord (mCurrentInput)) {
               UpdateColor (mCurrentRow, guess);
               mCurrentInput = "";
               mCurrentRow += 2;
               mGuessesLeft--;
            } else {
               SetCursorPosition (startX + 3, mMsgLine); // prints error message in the center of 25th row
               ForegroundColor = ConsoleColor.Yellow;
               WriteLine ($"{mCurrentInput.ToUpper ()} is invalid");
               ResetColor ();
            }
            break;
         case >= ConsoleKey.A and <= ConsoleKey.Z when mCurrentInput.Length < mWordLen:
            mCurrentInput += keyInfo.KeyChar;
            mCurrentCol = mCurrentInput.Length * mCellWidth + startX - 3;
            SetCursorPosition (mCurrentCol, mCurrentRow);
            Write ($" {keyInfo.KeyChar.ToString ().ToUpper ()}");
            break;
         case ConsoleKey.Backspace when mCurrentInput.Length > 0:
            SetCursorPosition (mCurrentCol, mCurrentRow);
            if (mCurrentCol < startX + mBoardWidth) Write ($" · ");
            mCurrentInput = mCurrentInput[..^1];
            mCurrentCol = mCurrentInput.Length * mCellWidth + 1;
            SetCursorPosition (startX + 3, mMsgLine); // removes error message in row 25 after pressing key
            WriteLine ("                         ");
            break;
         default: break;
      }
   }

   /// <summary> Prints the result of the Wordle game, indicating whether the player won or lost</summary>
   void PrintResult () {
      SetCursorPosition (startX - 3, mMsgLine - 1);
      if (!GameOver ()) return;
      if (mGuesses.Any (IsGuessCorrect)) {
         ForegroundColor = ConsoleColor.Green;
         WriteLine ($"You found the word in {6 - mGuessesLeft} tries");
         ResetColor ();
      } else {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ($"Sorry, you lost.The word was:{mGameWord.Word} ");
         ResetColor ();
      }
   }

   /// <summary>Checks if the game is over based on the number of remaining guesses and correct guesses</summary>
   bool GameOver () => mGuessesLeft == 0 || mGuesses.Any (IsGuessCorrect);

   /// <summary>Runs the Wordle game, allowing the player to make guesses and play the game</summary>
   public void Run () {
      do {
         Clear ();
         SetCursorPosition (startX + mCellWidth, 1);
         WriteLine ("WORDLE\n");
         mCurrentRow = 3;
         DisplayBoard ();
         alpColor = GetAlpColorDictionary ();
         while (!GameOver ()) {
            mCurrentCol = startX + (mCurrentInput.Length * mCellWidth) + 1;
            SetCursorPosition (mCurrentCol, mCurrentRow);
            if (mCurrentCol < startX + mBoardWidth) Write ($" ◌ ");
            ConsoleKeyInfo keyInfo = ReadKey (true);
            UpdateGameState (keyInfo);
            SetCursorPosition (mCurrentCol + mCellWidth, mCurrentRow);
         }
         PrintResult ();
      } while (PlayAgain ());
   }

   /// <summary>Asks the player if they want to play the game again</summary>
   /// <returns>True if the player wants to play again, otherwise false</returns>
   bool PlayAgain () {
      SetCursorPosition (startX - 3, mMsgLine); // Align the message to center
      WriteLine ("Do you want to continue? (Y/N)");
      ConsoleKeyInfo key = ReadKey (true);
      if (key.Key == ConsoleKey.Y) {
         mGuesses.Clear ();
         mGuessesLeft = 6;
         mCurrentInput = "";
         mGameWord = new GameWord (WordList.RandomWord ());
         return true;
      } else return false;
   }
   #endregion

   #region Fields ------------------------------------------------------------------------------
   GameWord mGameWord = new (WordList.RandomWord ());
   public List<Guess> mGuesses { get; } = new List<Guess> ();
   int mGuessesLeft = 6;
   string mCurrentInput = "";
   static Dictionary<char, ConsoleColor> alpColor;
   static readonly int mBoardWidth = 21;
   static readonly int mBoardHeight = 13 + 2;
   static readonly int startX = WindowWidth / 2 - mBoardWidth - 5;
   static readonly int mCellWidth = 4, mMsgLine = 23, mWordLen = 5;
   int mCurrentCol, mCurrentRow;
   #endregion
}
#endregion

#region Program --------------------------------------------------------------------------------
class Program {
   static void Main () {
      CursorVisible = false;
      OutputEncoding = Encoding.UTF8;
      new Wordle ().Run ();
   }
}
#endregion