// -----------------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// UnitTest1.cs
//Program to test file name parser
// ------------------------------------------------------------------------------------------------
using ClassLibrary;

namespace TestTraining {
   #region TestClass ------------------------------------------------
   /// <summary>Test class for UnitTest1</summary>
   [TestClass]
   public class UnitTest1 {
      #region Methods -----------------------------------------------
      /// <summary>Test method for the FileNameParser class</summary>
      [TestMethod]
      public void TestFileNameParser () {
         var sTests = new Dictionary<string, bool>{ 
            { @"Cz:\abc\def\r.txt", false }, { @"C:\abc\def\r.txt", true }, { @"C:\Readme.txt", false }, 
            { @"C:\abc\.bcf", false }, { @"C:\abc\bcf.", false }, { @"Readme.txt", false },
            { @"C:\abc\def", false }, { @"C:\abc d", false }, { @"\abcd\Readme.txt", false }, { " ", false },
            { @"C:\ab.c\def\r.txt", false }, { @"C:\abc:d", false }, { @".\abc", false }, { ".abc", false },
            { "abc", false }, { @"C:\abc6\def\r.txt", false }, { @"C:\work\r.txt", true }, { @"C:\abc\def\r.txt.txt", false },
            { @"C:\Program Files\<>*&^%$#@!.txt", false }, { @"C:\\work~\\r.txt~", false }
         };
         foreach (var testCase in sTests) {
            bool parse = FileNameParser.FileNameParse (testCase.Key, out _);
            Assert.AreEqual (parse, testCase.Value);
         }
      }
      #endregion
   }
   #endregion
}