Console.Write ("Enter the string:");
string inputString = Console.ReadLine();
Console.WriteLine ($"Is {inputString} ISOGRAM? {IsIsogram (inputString)}");
bool IsIsogram (string s) {
   char[] chars = s.ToLower().ToCharArray ();
   for (int i = 0; i < s.Length - 1; i++) if (chars[i] == chars[i + 1]) return false;
   return true;
}