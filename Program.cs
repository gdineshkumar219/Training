namespace Training {
   /// <summary>Program that takes input, sorts characters, and adds a special character.</summary>
   internal class Program {
      /// <summary>The entry point of the program.</summary>
      static void Main () {
         string input = GetInput ("Enter the input characters: ");
         char specialChar = GetSpecialCharacter ();
         bool isDescending = IsDescending ();
         string result = SortAndAddSpecialCharToEnd (input.ToCharArray (), specialChar, isDescending);
         Console.WriteLine ($"Sorted and moved special character: {result}");

         /// <summary>Prompts the user for input and validates that it contains only letters.</summary>
         /// <param name="prompt">The prompt message.</param>
         /// <returns>The user-provided input containing only letters.</returns>
         static string GetInput (string prompt) {
            string input;
            while (true) {
               Console.Write ($"{prompt}");
               input = Console.ReadLine ();
               if (!input.Any (char.IsDigit) && input.Length > 0) return input;
               Console.WriteLine ("Input should contain only letters. Please try again.");
            }
         }

         ///<summary>Prompts the user to choose whether to sort the array in descending order.</summary>
         /// <returns><c>true<c/>if the user chooses to sort in descending order; otherwise, <c>false</c></returns>
         static bool IsDescending () {
            Console.WriteLine ("Do you want to sort the array in descending order? (y)es or (n)o");
            return Console.ReadKey (true).KeyChar == 'y';
         }

         /// <summary>Prompts the user for a special character.</summary>
         /// <returns>The special character entered by the user.</returns>
         static char GetSpecialCharacter () {
            Console.Write ("Enter the special character you added. Otherwise, enter any character: ");
            char specialChar = Console.ReadKey ().KeyChar;
            Console.WriteLine ();
            return specialChar;
         }

         /// <summary>Sorts characters, filters out a special character and appends it to the result.</summary>
         /// <param name="characters">An array of characters to be sorted.</param>
         /// <param name="specialChar">The special character to be filtered out and appended.</param>
         /// <param name="isDescending">A boolean value indicating whether the order should be ascending(false) or descending(true)</param>
         /// <returns>The sorted characters with the special character appended.</returns>
         static string SortAndAddSpecialCharToEnd (char[] arr, char spl, bool isDescending = false) {
            char[] filteredArr = arr.Where (c => c != spl).ToArray ();
            char[] splChArr = arr.Where (c => c == spl).ToArray ();
            var sortedArr = isDescending == false ? filteredArr.Order () : filteredArr.OrderDescending ();
            return string.Join (", ", sortedArr.Concat (splChArr));
         }
      }
   }
}