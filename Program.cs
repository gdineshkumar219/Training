Console.Write ("Enter the string:");
string inputString = Console.ReadLine ();
Console.WriteLine ($"Is {inputString} ISOGRAM? {IsIsogram (inputString)}");
static bool IsIsogram (string str) {               
   return str.Where (char.IsLetter).GroupBy (char.ToLower).All (g => g.Count () == 1);
}