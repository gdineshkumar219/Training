int inputNumber = GetIntegerInput ();
Console.WriteLine (IsArmstrongNumber (inputNumber) is true ? $"{inputNumber} is an ARMSTRONG number." : $"{inputNumber} is not an ARMSTRONG number.");

int GetIntegerInput () {
   while (true) {
      Console.Write ("\nEnter a number to check whether it is ARMSTRONG number or not: ");
      if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
      Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

bool IsArmstrongNumber (int num) {
   int p = num.ToString ().Length;
   int result = 0;
   for (int i = num; i > 0; i /= 10) result += (int)Math.Pow (i % 10, p);
   return result == num;
}