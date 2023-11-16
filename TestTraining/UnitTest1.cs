// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// Program.cs
// Create a test for testing the Double.TryParse with underlying structure as array using
//property,methods and private variables
// --------------------------------------------------------------------------------------------
using ClassLibrary;

namespace TestDouble {
   [TestClass]
   public class UnitTest1 {
      [TestMethod]
      public void TestDoubleParse () {
         string[] values = { "0", "7", ".1", "08.6", "7897", "00990.009", "-7.78", "-7.78e1", "-7E2", "+778e0", "8e-3", "4e2.3", "+4e2.3", "0.003e",
                "x", "", "8e9.-3", "8-.7e3", "3.4e4-.3", "3.4e4+.3", "-35.-354e1", "e1", "1e", "1jkse", "1++$6e"};
         foreach (string val in values) {
            DoubleParser.TryParse (val, out double res);
            double.TryParse (val, out double res1);
            Assert.AreEqual (res1, res);
         }
      }
   }
}
