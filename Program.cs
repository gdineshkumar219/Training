Console.Write ("Enter the string: ");
string userInput = Console.ReadLine ();
string cleanedInput = RemoveSpaces (userInput);
string res = IsPalindrome (cleanedInput) ? $"{userInput} is a palindrome." : $"{userInput} is not a palindrome.";
Console.WriteLine (res);

string RemoveSpaces (string input) => new (input.ToLower ().Where (char.IsLetter).ToArray ());

bool IsPalindrome (string input) => input == new string (input.Reverse ().ToArray ());