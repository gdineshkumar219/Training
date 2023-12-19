// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------
// Classlib.cs
// ------------------------------------------------------------------------------------------------
namespace ClassLibrary {
   /// <summary>
   /// </summary>
   public class FileNameParser {
      public static (string dLetter, string folder, string flName, string ext) FileNameParse (string fName) {
         State s = State.A;
         Action none = () => { },todo;
         string folder = "", dLetter = "", flName = "", ext = ".";
         foreach (var ch in fName.Trim ()) {
            (s, todo) = (s, ch) switch {
               (State.A, >= 'A' and <= 'Z') => (State.B, () => dLetter = ch.ToString ()),
               (State.B, ':') => (State.C, none),
               (State.C or State.E, '\\') => (State.D, () => folder += ch),
               (State.D or State.E, >= 'A' and <= 'Z') => (State.E, () => folder += ch),
               (State.E, '.') => (State.F, () => flName += folder[(folder.LastIndexOf ('\\') + 1)..]),
               (State.F or State.G, >= 'A' and <= 'Z') => (State.G, () => ext += ch),
               (State.G, '~') => (State.I, none),
               _ => (State.Z, none),
            };
            todo ();
         }
         if (s == State.I) {
            var res = (dLetter, folder[1..].Remove (folder.LastIndexOf ('\\') - 1), flName + ext, ext);
            return (res);
         }
         return (string.Empty, string.Empty, string.Empty, string.Empty);
      }
   }

   public enum State {
      A,
      B,
      C,
      D,
      E,
      F,
      G,
      I,
      Z
   }
}