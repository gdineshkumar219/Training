using System;
Console.WriteLine ("I'm thinking of a number between 1 and 100.");
Console.WriteLine ("You have to guess it.");
int num, targetNumber = new Random().Next (1, 101);
for (; ; ) {
    Console.Write ("Enter your guess:");
    num = int.Parse (Console.ReadLine ());
    if (num == targetNumber) {
        Console.WriteLine ("Your guess is correct"); break;
    } 
    else if (num > targetNumber)
        Console.WriteLine ("Your guess is too high");
    else
        Console.WriteLine ("Your guess is too small");
 }