// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// --------------------------------------------------------------------------------------------

using ClassLibrary;

namespace Training {
   internal class Program {
      #region Methods -----------------------------------------------
      static void Main () {
         TestTQueue ();
         TestBuiltInQueue ();
      }

      /// <summary>Tests the custom TQueue class</summary>
      static void TestTQueue () {
         Console.WriteLine ("Testing TQueue<T>");
         var tQueue = new TQueue<int> ();
         tQueue.Enqueue (1);
         tQueue.DisplayQueue ();
         for (int i = 1; i < 4; i++) tQueue.Enqueue (i * 3);
         tQueue.DisplayQueue ();
         Console.WriteLine ($"No of elements present: {tQueue.Count}");
         Console.WriteLine ($"Capacity: {tQueue.Capacity}");
         Console.WriteLine ($"Dequeued element:{tQueue.Dequeue ()}");
         tQueue.DisplayQueue ();
         Console.WriteLine ($"Peek:  {tQueue.Peek ()}");
         tQueue.Enqueue (37);
         tQueue.DisplayQueue ();
         tQueue.Enqueue (19);
         tQueue.DisplayQueue ();
         Console.WriteLine ($"Is Queue empty: {tQueue.IsEmpty}");
         Console.WriteLine ($"Is Queue full: {tQueue.IsFull}");
         Console.WriteLine ($"Capacity: {tQueue.Capacity}");
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         tQueue.DisplayQueue ();
         tQueue.Dequeue ();
         Console.WriteLine ($"Capacity: {tQueue.Capacity}");
         tQueue.DisplayQueue ();
         Console.WriteLine ("----------------------------------\n");
      }

      /// <summary>Tests the custom built-in Queue class</summary>
      static void TestBuiltInQueue () {
         Console.WriteLine ("Testing built-in Queue<T>");
         var queue = new Queue<int> ();
         queue.Enqueue (1);
         for (int i = 0; i < 3; i++) queue.Enqueue (i * 3);
         Console.WriteLine ($"Dequeued element:{queue.Dequeue ()}");
         Console.WriteLine ($"Peek:  {queue.Peek ()}");
         queue.Enqueue (7);
         queue.Enqueue (9);
         Console.WriteLine ($"Is Queue empty: {queue.Count == 0}");
      }
      #endregion
   }
}