using ClassLibrary;

namespace Training {
   /// <summary>Contains the Main method to demonstrate the Double.TryParse class</summary>
   internal class Program {
      #region Methods -----------------------------------------------
      /// <summary>Entry Point of the program</summary>
      static void Main () {
         string[] values = { "0", "7", ".1", "08.6", "7897", "00990.009", "-7.78", "-7.78e1", "-7E2", "+778e0", "8e-3", "4e2.3", "+4e2.3", "0.003e",
                "x", "", "8e9.-3", "8-.7e3", "3.4e4-.3", "3.4e4+.3", "-35.-354e1", "e1", "1e", "1jkse", "1++$6e"};
         foreach (var val in values) {
            if (DoubleParser.TryParse (val, out double res)) Console.WriteLine ($"Parsed successfully (Custom): {val} as {res}");
            else Console.WriteLine ($"Couldn't parse (Custom): {val} {res}");
         }
         foreach (var val in values) {
            if (double.TryParse (val, out double res)) Console.WriteLine ($"Parsed successfully (double.TryParse): {val} as {res}");
            else Console.WriteLine ($"Couldn't parse (double.TryParse): {val} {res}");
         }
      }
      #endregion
   }
}