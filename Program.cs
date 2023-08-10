using static System.Console;
int lowLimit = 0, highLimit = 100, mid;
WriteLine ("Think of a number between 1 to 100\nThe computer will guess it");
for (int tries = 1; tries <= 6;) {
   mid = (lowLimit + highLimit) / 2;
   Write ($"\nIs the number greater than {mid}? (Y)es / (N)o: ");
   switch (ReadKey ().Key) {
      case ConsoleKey.Y:
         lowLimit = mid + 1;
         tries++;
         break;
      case ConsoleKey.N:
         highLimit = mid;
         tries++;
         break;
      default:
         WriteLine ("\nEnter a valid key");
         break;
   }
}
WriteLine ($"\nThe secret number is {highLimit}");
