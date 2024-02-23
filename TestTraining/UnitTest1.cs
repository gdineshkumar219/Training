// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// UnitTest1.cs
// --------------------------------------------------------------------------------------------
using Training;
namespace TestTraining {
   [TestClass]
   public class UnitTest1 {
      [TestMethod]
      public void TestCorrectGuess () {
         Wordle wordle = new ("ADEPT", new List<string> { "ABOUT", "DEPTH", "PAINT", "APART", "AGAIN", "ADEPT" });
         wordle.Run ();
         Assert.IsTrue (CheckTextFilesEqual ("../Training/data/Test_File.txt", "data/CorrectGuess.txt"));
      }

      [TestMethod]
      public void TestFailedGuess () {
         Wordle wordle = new ("ADEPT", new List<string> { "ABOUT", "DEPTH", "PAINT", "APART", "AGAIN", "ALONE" });
         wordle.Run ();
         Assert.IsTrue (CheckTextFilesEqual ("../Training/data/Test_File.txt", "data/failedGuess.txt"));
      }

      [TestMethod]
      public void TestInvalid () {
         Wordle wordle = new ("ADEPT", new List<string> { "ABOUP" });
         wordle.Run ();
         Assert.IsTrue (CheckTextFilesEqual ("../Training/data/Test_File.txt", "data/invalid.txt"));
      }

      [TestMethod]
      public void TestInComplete () {
         Wordle wordle = new ("ADEPT", new List<string> { "ABOUT", "DEPTH", "PAINT", "APA" });
         wordle.Run ();
         Assert.IsTrue (CheckTextFilesEqual ("../Training/data/Test_File.txt", "data/incomplete.txt"));
      }

      [TestMethod]
      public void TestToHandleBackSpace () {
         Wordle wordle = new ("ADEPT", new List<string> { "ABOUT" }, ConsoleKey.Backspace);
         wordle.Run ();
         Assert.IsTrue (CheckTextFilesEqual ("../Training/data/Test_File.txt", "data/handleBackSpaceKey.txt"));
      }

      [TestMethod]
      public void TestToHandleLeftArrow () {
         Wordle wordle = new ("ADEPT", new List<string> { "ADEPT" }, ConsoleKey.LeftArrow);
         wordle.Run ();
         Assert.IsTrue (CheckTextFilesEqual ("../Training/data/Test_File.txt", "data/handleLeftArrowKey.txt"));
      }

      bool CheckTextFilesEqual (string f1, string f2) {
         var file1 = File.ReadAllText (f1);
         var file2 = File.ReadAllText (f2);
         if (file1.Equals (file2)) return true;
         var p = System.Diagnostics.Process.Start ("C:/Program Files/WinMerge/WinMergeU.exe", $"\"{f1}\" \"{f2}\"");
         p.WaitForExit ();
         return false;
      }
   }
}