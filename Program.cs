Console.Write ("Enter the number to convert:");
int num = Convert.ToInt32 (Console.ReadLine ());
string result = "";
int rem;
Console.WriteLine ($"The binary value of {num} is : {dec2bin (num)}.");
Console.WriteLine ($"The hexadecimal value of {num} is : {dec2hex (num)}.");
string dec2bin (int num) {
   while (num > 1) {
      rem = num % 2;
      result = Convert.ToString (rem) + result;num /= 2;
   }
   return result;
}
string dec2hex (int num) {
   string result = num.ToString ("X");
   return result;
}

