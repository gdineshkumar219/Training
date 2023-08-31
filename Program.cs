if (args.Length == 0) {
   Console.WriteLine ("Please provide a positive integer as input.");
   return;
}
if (!int.TryParse (args[0], out int value) || value <= 0) {
   Console.WriteLine ("Invalid input. Please enter a positive integer.");
   return;
}
int result = CalculateNthArmstrong (value);
Console.WriteLine ($"The {value}th Armstrong number is: {result}");

static int CalculateNthArmstrong (int n) {
   if (n == 1) return 0;
   int count = 1, num = 1;
   while (count < n) {
      num++;
      if (IsArmstrong (num)) count++;
   }
   return num;
}

static bool IsArmstrong (int num) {
   int originalNum = num;
   int numDigits = num.ToString ().Length;
   int sum = 0;
   while (num > 0) {
      int digit = num % 10;
      sum += (int)Math.Pow (digit, numDigits);
      num /= 10;
   }
   return sum == originalNum;
}