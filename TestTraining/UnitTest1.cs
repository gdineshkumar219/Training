// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// UnitTest1.cs
// Create a test for testing the Double.TryParse with underlying structure as array using
//property,methods and private variables
// --------------------------------------------------------------------------------------------
using ClassLibrary;

namespace TestDouble {
   [TestClass]
   public class UnitTest1 {
      #region Test Methods ------------------------------------------

      [TestMethod]
      public void TestDoubleParse () {
         // Valid values to test
         string[] values = { "0", ".e2", "7", "08.6", "7897", "00990.009", "-7.78", "-7.78e1", "-7E2",
            "+778e0", "8e-3", "4e2.3", "+4e2.3", "0.003e","x", "", "8e9.-3", "8-.7e3", "3.4e4-.3",
            "3.4e4+.3", "-35.-354e1", "e1", "1e", "1jkse", "1++$6e"};
         // Invalid values to test
         string[] invalidVal = { ".34", "45." };
         // Test each valid value
         foreach (string val in values) {
            // Using DoubleParser
            DoubleParser.TryParse (val, out double res);
            // Using double.TryParse as a reference
            double.TryParse (val, out double res1);
            // Asserting equality
            Assert.AreEqual (res1, res);
         }
         // Test each invalid value
         foreach (string val in invalidVal) {
            // Using DoubleParser
            DoubleParser.TryParse (val, out double res);
            // Using double.TryParse as a reference
            double.TryParse (val, out double res1);
            // Asserting inequality
            Assert.AreNotEqual (res1, res);
         }
      }
      #endregion
   }
}