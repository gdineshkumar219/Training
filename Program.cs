Console.Write ("Enter the string:");
string inputString = Console.ReadLine ();
Console.WriteLine ($"The reduced string is {StringReduction (inputString)}");

static string StringReduction (string str) {
   string result = "";
   for (int i = 0; i < str.Length; i++) {
      if (i == str.Length - 1 || str[i] != str[i + 1]) result += str[i];
      else i++;
   }
   return result;
}