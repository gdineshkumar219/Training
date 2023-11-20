// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Program to implement built-in stack and custom stack.
// --------------------------------------------------------------------------------------------

using ClassLibrary;

namespace Training {
   internal class Program {
      static void Main () {
         var tStack = new TStack<int> ();
         var stack = new Stack<int> ();
         for (int i = 1; i < 4; i++) {
            tStack.Push (i * i);
            stack.Push (i * i);
         }
         Console.WriteLine ("---------TStack---------");
         Console.WriteLine ($"Initial Stack Count: {tStack.Count} \nInitial Stack Capacity:{tStack.Capacity}");
         Console.WriteLine ($"First Pop using TStack: {tStack.Pop ()}");
         Console.WriteLine ($"Peek using Tstack: {tStack.Peek ()}");
         Console.WriteLine ($"Second Pop using TStack: {tStack.Pop ()}");
         Console.WriteLine ($"Third Pop using TStack: {tStack.Pop ()}");
         Console.WriteLine ($"Check IsEmpty using TStack: {tStack.IsEmpty}");
         Console.WriteLine ($"Stack Count: {tStack.Count}\nStack Capacity:{tStack.Capacity}");
         tStack.Clear ();
         Console.WriteLine ($"Check IsEmpty using TStack: {tStack.IsEmpty}");
         Console.WriteLine ("\n-------------------------------------------------\n");
         Console.WriteLine ("---------Stack---------");
         Console.WriteLine ($"Initial Stack Count: {stack.Count} ");
         Console.WriteLine ($"First Pop using Stack: {stack.Pop ()}");
         Console.WriteLine ($"Peek using Stack: {stack.Peek ()}");
         Console.WriteLine ($"Second Pop using Stack: {stack.Pop ()}");
         Console.WriteLine ($"Third Pop using Stack: {stack.Pop ()}");
         Console.WriteLine ($"Check IsEmpty using Stack: {stack.Count == 0}");
         Console.WriteLine ($"Stack Count: {stack.Count}");
         stack.Clear ();
         Console.WriteLine ($"Check IsEmpty using Stack: {stack.Count == 0}");
      }
   }
}