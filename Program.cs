namespace Training {

   /// <summary>A program for calculating the number of chocolates that can be bought with a given inputs and the number of wrappers required for an extra chocolate</summary>
   internal class Program {

      /// <summary>The entry point of the program</summary>
      static void Main () {
         int money = GetInput ("Enter the amount of money: ");
         int pricePerChocolate = GetInput ("Enter the price per chocolate: ");
         int wrappersRequired = GetInput ("Enter the number of wrappers required for an extra chocolate: ");
         Console.WriteLine ($"Input:(X = {money}, P = {pricePerChocolate}, W = {wrappersRequired})");
         (int chocolates, int remainingMoney, int remainingWrappers) = CalculateChocolates (money, pricePerChocolate, wrappersRequired);
         Console.WriteLine ($"Output:(C = {chocolates}, X = {remainingMoney}, W = {remainingWrappers})");
         Console.WriteLine ($"You can get {chocolates} chocolates, have {remainingMoney} leftover money, and {remainingWrappers} wrappers remaining.");
      }

      /// <summary>Gets an integer input from the user.</summary>
      /// <param name="prompt">The prompt message displayed to the user</param>
      /// <returns>The integer input from the user</returns>
      static int GetInput (string prompt) {
         while (true) {
            Console.Write (prompt);
            if (int.TryParse (Console.ReadLine (), out int value) && value > 1) return value;
            Console.WriteLine ("Invalid input. Please enter an integer.");
         }
      }

      /// <summary>Calculates the number of chocolates that can be bought with the given money, the remaining money, and the remaining wrappers</summary>
      /// <param name="money">The amount of money available</param>
      /// <param name="pricePerChocolate">The price per chocolate</param>
      /// <param name="wrappersRequired">The number of wrappers required for an extra chocolate</param>
      /// <returns>Tuple containing the number of chocolates, remaining money, and remaining wrappers</returns>
      static (int chocolates, int remainingMoney, int remainingWrappers) CalculateChocolates (int money, int pricePerChocolate, int wrappersRequired) {
         int chocolates = money / pricePerChocolate;
         int remainingMoney = money % pricePerChocolate;
         int wrappers = chocolates;
         while (wrappers >= wrappersRequired) {
            int newChocolates = wrappers / wrappersRequired;
            chocolates += newChocolates;
            wrappers = newChocolates + (wrappers % wrappersRequired);
         }
         return (chocolates, remainingMoney, wrappers);
      }
   }
}