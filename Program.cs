namespace Training {
   /// <summary>Program that takes input, sorts characters, and adds a special character.</summary>
   internal class Program {
      /// <summary>The entry point of the program.</summary>
      static void Main () {
         string input = GetInput ("Enter the input characters: ");
         char specialChar = GetSpecialCharacter ();
         string sortOrder = GetSortOrder ();
         string result = SortAndAddSpecialCharToEnd (input.ToCharArray (), specialChar, sortOrder);
         Console.WriteLine ($"Sorted and moved special character: {result}");

         /// <summary>
         /// Prompts the user for input and validates that it contains only letters.
         /// </summary>
         /// <param name="prompt">The prompt message.</param>
         /// <returns>The user-provided input containing only letters.</returns>
         static string GetInput (string prompt) {
            string input;
            while (true) {
               Console.Write ($"{prompt}");
               input = Console.ReadLine ();
               if (!input.Any (char.IsDigit) && input.Length > 0) return input;
               Console.WriteLine ("Input should contain only letters . Please try again.");
            }
         }

         /// <summary>Prompts the user for the desired sort order and returns it.</summary>
         /// <returns>The sort order ("ascending" or "descending").</returns>
         static string GetSortOrder () {
            Console.WriteLine ("Do you want to sort the array in descending order? (y)es or (n)o");
            char orderKey = Console.ReadKey (true).KeyChar;
            return orderKey == 'y' ? "descending" : "ascending";
         }

         /// <summary>Prompts the user for a special character.</summary>
         /// <returns>The special character entered by the user.</returns>
         static char GetSpecialCharacter () {
            Console.Write ("Enter the special character you added.Otherwise,enter any character: ");
            char specialChar = Console.ReadKey ().KeyChar;
            Console.WriteLine ();
            return specialChar;
         }

         /// <summary>Sorts characters, filters out a special character, and appends it to the result.</summary>
         /// <param name="characters">An array of characters to be sorted.</param>
         /// <param name="specialChar">The special character to be filtered out and appended.</param>
         /// <param name="sortOrder">The desired sort order ("ascending" or "descending"). Default is "ascending".</param>
         /// <returns>The sorted characters with the special character appended.</returns>
         static string SortAndAddSpecialCharToEnd (char[] chArr, char splCh, string sortOrder = "ascending") {
            char[] filteredArr = chArr.Where (c => char.IsLetter (c)).ToArray ();
            char[] splChArr = chArr.Where (c => c == splCh && !char.IsLetter (c)).ToArray ();
            var sortedArr = sortOrder == "ascending" ? filteredArr.Order () : filteredArr.OrderDescending ();
            return string.Join (", ", sortedArr.Concat (splChArr));
         }
      }
   }
}