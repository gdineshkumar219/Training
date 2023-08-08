Console.Write ("Enter the string: ");
string userInput = Console.ReadLine ();
string cleanedInput = RemoveSpacesAndLowercase (userInput);
string res = IsPalindrome (cleanedInput) ? $"{userInput} is a palindrome." : $"{userInput} is not a palindrome.";
Console.WriteLine (res);
string RemoveSpacesAndLowercase (string input) {
   return new string (input.ToLower ().Where (char.IsLetter).ToArray ());
}
bool IsPalindrome (string input) {
   return input == new string (input.Reverse ().ToArray ());
}