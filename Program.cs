Console.Write ("Enter the string:");
string inputString = Console.ReadLine ();
Console.WriteLine ($"The reduced string is {StringReduction (inputString)}");

static string StringReduction (string str) {
   string result = "";
   foreach (char ch in str) result = (result.Length > 0 && result[result.Length - 1] == ch) ? result.Substring (0, result.Length - 1) : result + ch;
   return result;
}