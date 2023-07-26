using System;

Console.WriteLine ("I'm thinking of a number between 1 and 100.");
Console.WriteLine ("You have to guess it.");
int targetNumber = new Random ().Next (1, 101);
for (; ; ) {
    Console.Write ("Enter your guess:");
    string value=Console.ReadLine ();
   if (int.TryParse (value, out int result)) return result;
   if (num == targetNumber) {
        Console.WriteLine ("Your guess is correct"); break;
    } 
    else if (num > targetNumber)
        Console.WriteLine ("Your guess is too high");
    else
        Console.WriteLine ("Your guess is too small");
 }

