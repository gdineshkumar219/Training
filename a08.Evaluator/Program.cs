﻿namespace Eval;

class Program {
   static void Main () {
      var eval = new Evaluator ();
      List<string> inputs = new () {
        "-10 ^ 2", "a = 4", "b = 3.5", "a + b", "asin sin 90", "atan tan 45", "sqrt 25",
        "log 1", "-2 -2", "10/2+3", "(+10+3)*2", "(a+2) * a", "cos 0", "exp 2", "cos acos 0",
        "10 -2 -2", "---5", "-5+10", "-2-4", "---4+5--2+3", "(2+3)*-4", "(2+3)*+5",
        "-4*(3+5)", "-4+5--8", "-4+5-(-8)", "10^(-4+2)", "-10-10^2", "---4+5--6-2", "sin 45",
        "sin -45", "-sin 45", "cos 45", "cos -45", "tan 45", "sin(45+45)", "(sin 90)*2",
        "sin --90", "log 10", "sqrt 100", "sqrt(10^2)", "(sqrt 100) - 10", "(tan 45)+10-20",
        "asin 1", "acos 0", "(log 10)+5", "log(10+5)", "(sin 90)--1", "(sin -90)--1",
        "sqrt(90+10)", "sqrt(110-10)", "atan 1", "asin -1", "atan -1", "(atan -1)+45",
        "exp 1", "exp 1-2", "exp(2-1)", "exp -1","sqrt -100", "log(-10+5)", "sin(sqrt-1)",
         "sqrt asin-1","3 + * 5", "(4 + 6", "2 + abc", "6 *", "3 + 2 *",
         "5 * (3 + 2))"
    };
      Console.WriteLine ($" {"input",-20} | {"Output",15} ");
      Console.WriteLine (new string ('-', 40));
      foreach (var input in inputs) {
         try {
            double result = eval.Evaluate (input);
            PrintString ($" {input,-20} |", "");
            PrintString ($" {result,15} \n", "Green");
         } catch (EvalException e) {
            PrintString ($" {input,-20} |", "");
            PrintString ($" {e.Message,15} \n", "DarkRed");
         }
      }
      for (; ; ) {
         PrintString ($"\nEnter expressions to evaluate or type 'exit' to end\n", "Cyan");
         Console.Write ("> ");
         string text = Console.ReadLine ()?.Trim ()?.ToLower ();
         if (text == "exit") break;
         try {
            double result = eval.Evaluate (text);
            PrintString ($"Result: {result}\n", "Green");
         } catch (EvalException e) {
            PrintString ($"Error: {e.Message}\n", "DarkRed");
         }
      }
      static void PrintString (string str, string color) {
         Console.ForegroundColor = Enum.TryParse (color, true, out ConsoleColor consoleColor) ?
            consoleColor : ConsoleColor.White;
         Console.Write (str);
         Console.ResetColor ();
      }
   }
}