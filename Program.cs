Console.Write ("Enter the string:");
string inputString = Console.ReadLine ();
Console.WriteLine ($"Is {inputString} ISOGRAM? {IsIsogram (inputString)}");
static bool IsIsogram (string str) => str.Where (char.IsLetterOrDigit).GroupBy (char.ToLower).All (g => g.Count () == 1);
