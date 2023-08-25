Console.Write ("Enter the string:");
string inputString=Console.ReadLine ();
Console.WriteLine (StringReduction(inputString));
string StringReduction(string str) {
   string result = "";
   char[] chars = str.ToCharArray ();
   for (int i=0; i < str.Length; i++) 
      if (chars[i] != chars[i + 1]) { result += chars[i] ; }
      
   
   return result;
}