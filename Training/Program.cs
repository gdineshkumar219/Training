// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------

using System.Reflection;
using static System.Console;
using static System.ConsoleColor;

namespace Training {
   public class Wordle {
      public Wordle () {
         mSw = new StreamWriter (mFilePath, false);
         mWinWidth = WindowWidth / 2;
      }

      public Wordle (string secretWord, List<string> guess, ConsoleKey key = ConsoleKey.Enter, string filePath = $"../Training/data/Test_File.txt") {
         mWord = secretWord;
         mList = guess;
         if (File.Exists (filePath)) File.Delete (filePath);
         mFilePath = filePath;
         mSw = new StreamWriter (mFilePath, false);
         mDict = LoadStrings ("dict-5.txt");
         mWinWidth = 100;
         mKey = key;
      }

      void UserInputMode () {
         Clear ();
         ClearScreen ();
         SelectWord ();
         DisplayBoard ();
         while (!GameOver) {
            ConsoleKey key = ReadKey (true).Key;
            UpdateGameState (key);
            ResetFile ();
            DisplayBoard ();
         }
         PrintResult ();
      }

      void CustomInputMode () {
         foreach (string word in mList) {
            foreach (char ch in word) {
               ClearScreen ();
               UpdateGameState ((ConsoleKey)ch);
            }
            UpdateGameState (mKey);
            ResetFile ();
            DisplayBoard ();
         }
         if (GameOver) PrintResult ();
         mSw.Close ();
      }

      public void Run () {
         if (mList is null) UserInputMode ();
         else CustomInputMode ();
      }

      // Implementation ----------------------------
      // Set up suitable colors and clear the screen
      void ClearScreen () {
         BackgroundColor = Black;
         GRIDX = mWinWidth - 7;
         KBDX = mWinWidth - 16;
         if (mList is null) CursorVisible = false;
         OutputEncoding = System.Text.Encoding.Unicode;
      }

      // Select a word at random 
      void SelectWord () {
         mDict = LoadStrings ("dict-5.txt");
         var puzzle = LoadStrings ("puzzle-5.txt");
         mWord = puzzle[new Random ().Next (puzzle.Length)];
      }

      // Display the current state of the board
      void DisplayBoard () {
         // First, display the game state
         for (int y = 0; y < 6; y++)
            for (int x = 0; x < 5; x++) {
               char ch = mGuess[y][x];
               ConsoleColor color = Gray;
               if (y < mY) {
                  color = DarkGray;
                  if (mWord[x] == ch) color = Green;
                  else if (mWord.Contains (ch)) color = Blue;
               }
               if (ch == ' ') ch = '\u00b7';
               if (x == mX && y == mY) ch = '\u25cc';
               Put (x * 3 + GRIDX, y * 2 + GRIDY, color, ch);
               mSw.Flush ();
            }

         // Then, add the 'keyboard hint display' - this shows the keys 
         // that we've already used along with some color codes
         Put (KBDX, KBDY - 2, DarkGray, new string ('_', 31));
         string recent = mY == 0 ? "     " : mGuess[mY - 1];
         for (int i = 0; i < 26; i++) {
            int x = i % 7, y = i / 7;
            char ch = (char)('A' + i);
            ConsoleColor color = White;                                 // Not yet used
            if (mGuess.Take (mY).Any (a => a.Contains (ch))) color = DarkGray;    // Already used
            if (recent.Contains (ch) && mWord.Contains (ch)) {
               color = Blue;     // Used in an incorrect position in the recent guess
               int a = recent.IndexOf (ch), b = mWord.IndexOf (ch);
               if (a == b) color = Green;
            }
            Put (x * 5 + KBDX, y * 1 + KBDY, color, ch);
            mSw.Flush ();
         }

         // If the user has recently typed in a word that is not in the
         // dictionary, display that
         string error = (mBadWord != null) ? $"{mBadWord} is not a word" : new string (' ', 20);
         Put (mWinWidth - 10, RESULTY + 1, Yellow, error);
         mSw.Flush ();
      }

      void ResetFile () {
         mSw.Close ();
         FileStream file = new (mFilePath, FileMode.OpenOrCreate);
         mSw = new (file);
      }

      int GRIDX = 3, GRIDY = 1;
      int KBDX = 3, KBDY = 14;

      // Check if the game is over
      bool GameOver => mSucceeded || mFailed;
      bool mSucceeded;     // User succeeded in guessing the word
      bool mFailed;        // User failed to guess the word
      string Color2Char (char ch, ConsoleColor color) =>
         color switch {
            Green => new string ($"{{{ch}}}"),
            Blue => new string ($"[{ch}]"),
            DarkGray => new string ($"({ch})"),
            White => new string ($"{ch}"),
            _ => new string ($"{ch}")
         };

      // Update the game-state based on the key the user pressed
      void UpdateGameState (ConsoleKey info) {
         mBadWord = null;
         if (info is ConsoleKey.LeftArrow or ConsoleKey.Backspace && mX > 0) {
            // First, handle left / backspace to erase the last character
            Set (--mX, mY, ' ');
            return;
         }
         if (info is ConsoleKey.Enter && mX == 5) {
            // Handle the Enter key to submit a new word
            // First, if the current word is not in the dictionar, don't 
            // accept it
            if (!mDict.Contains (mGuess[mY])) {
               mBadWord = mGuess[mY];
               return;
            }
            mX = 0;
            if (mGuess[mY++] == mWord) { mSucceeded = true; } else if (mY == 6) mFailed = true;
            return;
         }
         char ch = char.ToUpper ((char)info);
         if (ch is >= 'A' and <= 'Z' && mX < 5) {
            // Handle letter keys to input a new character
            Set (mX++, mY, ch);
            return;
         }

         // Set a particular character of a particular guess string
         void Set (int x, int y, char ch) {
            var array = mGuess[y].ToCharArray ();
            array[x] = ch;
            mGuess[y] = new string (array);
         }
      }

      // Print the final result
      void PrintResult () {
         Put (KBDX, RESULTY, DarkGray, new string ('_', 31));
         if (mSucceeded)
            Put (mWinWidth - 15, RESULTY + 2, Green, $"You found the word in {mY} tries");
         else
            Put (mWinWidth - 13, RESULTY + 2, Yellow, $"Sorry - the word was {mWord}");
         Put (mWinWidth - 11, RESULTY + 4, White, "Press any key to quit");
         if (mList is null) ReadKey ();
         WriteLine ();
      }
      int RESULTY = 18;

      void Put (int x, int y, ConsoleColor color, object data) {
         if (mList != null) {
            PutToFile (x, y, color, data);
            return;
         }
         CursorLeft = x; CursorTop = y; ForegroundColor = color;
         Write (data);
         PutToFile (x, y, color, data);
         mSw.Flush ();
         ResetColor ();
      }

      void PutToFile (int x, int y, ConsoleColor color, object data) {
         if (y != mTemp && y != 19) {
            if (y == 18) mSw.WriteLine ();
            else mSw.WriteLine ("\n");
            mTemp = y;
            if (y % 6 == 0) x++;
            mSw.Write ($"{new string (' ', x)}");
         }
         if (y > 18) {
            mTemp = y;
            if (color == Yellow) {
               mSw.WriteLine ();
               mSw.Write ($"{new string (' ', x)}");
            }
            mSw.WriteLine (data.ToString ());
         } else if (y < 19) {
            foreach (char c in data.ToString ()) {
               if (color is ConsoleColor.White or ConsoleColor.Gray && c != '_') {
                  if (char.IsLetter (c) && y > 12) mSw.Write ($" {c}   "); // Keyboard before key-press
                  else mSw.Write ($" {c} ");
               } else {
                  string annotatedChar = Color2Char (c, color);
                  if (char.IsLetterOrDigit (c)) {
                     if (y > 13 && y <= 17) mSw.Write ($"{annotatedChar}  "); // Indication of keyboard after each state
                     else if (y < 13) mSw.Write (annotatedChar);
                  } else if (c == '_') mSw.Write ($"{c}"); // DASH HAS NO SPACES
               }
            }
         }
         mSw.Flush ();
         ResetColor ();
      }

      // Helper routine to load strings from a assembly-manifest resource file
      string[] LoadStrings (string file) {
         using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ($"Training.data.{file}");
         using var reader = new StreamReader (stream);
         return reader.ReadToEnd ().Split ('\n');
      }

      // State -------------------------------------
      string[] mDict;   // The dictionary of all possible words
      string mWord;     // The word the computer has selected
      List<string> mList;
      string[] mGuess = { "     ", "     ", "     ", "     ", "     ", "     " };   // The 6 guesses of the user
      int mY = 0;       // The word we're typing into now
      int mX = 0;       // The letter within that word we're typing in
      string mBadWord;  // This is set if the user types in a word not in the dictionary
      StreamWriter mSw;
      int mTemp = -1, mWinWidth;
      static string mFilePath;
      ConsoleKey mKey;
   }

   internal class Program {
      static void Main () => new Wordle ().Run ();
   }
}