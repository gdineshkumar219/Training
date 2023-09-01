decimal inputNumber = GetInput ();
DisplayParts (inputNumber);

decimal GetInput () {
   while (true) {
      Console.Write ("Enter a decimal number: ");
      if (decimal.TryParse (Console.ReadLine (), out decimal value) && value > 0) return value;
      Console.WriteLine ("Invalid input. Please enter a valid decimal number");
   }
}

void DisplayParts (decimal number) {
   string integralDigits = string.Join (" ", Math.Floor (number).ToString ().ToCharArray ());
   string fractionalDigits = string.Join (" ", GetFractionalDigits (number));
   Console.WriteLine ($"Integral part: {integralDigits}");
   Console.WriteLine ($"Fractional part: {fractionalDigits}");
}

char[] GetFractionalDigits (decimal number) => (number.ToString ().Contains ('.') ? number.ToString ().Split ('.')[1] : "0").ToCharArray ();