// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Classlib.cs
// Program for parsing file paths and extracting information such as drive letter,
// folder, file name, and extension.
// ------------------------------------------------------------------------------------------------
namespace ClassLibrary {
   /// <summary>Provides methods for parsing file paths and extracting information such as drive letter, 
   /// folder, file name, and extension</summary>
   #region FileNameParser -------------------------------------------
   public class FileNameParser {
      #region Methods -----------------------------------------------
      /// <summary>Represents the different states during the parsing process</summary>
      /// See File://fileParserDiagram.png
      public enum State {
         A,  // Start
         B,  // Drive Letter
         C,  // After drive letter colon
         D,  // In folder
         E,  // After folder backslash
         F,  // After extension dot
         G,  // After extension
         H,  // End
         Z   // Error
      }

      /// <summary>Parses the given file path and extracts drive letter, folder, file name, and extension</summary>
      /// <param name="filePath">The input file path to be parsed</param>
      /// <param name="result">A tuple containing drive letter, folder, file name, and extension if parsing is successful</param>
      /// <returns>True if parsing is successful; otherwise, false</returns>
      public static bool FileNameParse (string filePath, out (string dLetter, string folder, string flName, string ext) result) {
         filePath = filePath.ToUpper () + '~';
         State s = State.A;
         Action none = () => { }, todo;
         string folder = "", dLetter = "", flName = "", ext = ".";
         foreach (var ch in filePath.Trim ()) {
            // Transition based on the current state and the current character
            (s, todo) = (s, ch) switch {
               (State.A, >= 'A' and <= 'Z') => (State.B, () => dLetter = ch.ToString ()),
               (State.B, ':') => (State.C, none),
               (State.C or State.E, '\\') => (State.D, () => folder += ch),
               (State.D or State.E, >= 'A' and <= 'Z') => (State.E, () => folder += ch),
               (State.E, '.') => (State.F, () => flName += folder[(folder.LastIndexOf ('\\') + 1)..]),
               (State.F or State.G, >= 'A' and <= 'Z') => (State.G, () => ext += ch),
               (State.G, '~') => (State.H, none),
               _ => (State.Z, none),
            };
            todo ();
         }
         // Check if the parsing reached the end state
         if (s == State.H && folder.Count (item => item == '\\') > 1) {
            result = (dLetter, folder[1..].Remove (folder.LastIndexOf ('\\') - 1), flName + ext, ext);
            return true;
         }
         // Set result to default and return false if parsing was not successful
         result = (string.Empty, string.Empty, string.Empty, string.Empty);
         return false;
      }
      #endregion
   }
   #endregion
}