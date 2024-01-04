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
      // Boolean flags to track test results
      static bool capacity = false;
      static bool peek = false;

      static void Main () {
         Console.WriteLine ($"{"Method Name",-15}|{"Test Status"}");
         Console.WriteLine ("----------------------------");
         PrintTestStatus ("Enqueue", TestEnqueue ());
         PrintTestStatus ("Dequeue", TestDequeue ());
         PrintTestStatus ("Capacity", capacity);
         PrintTestStatus ("Peek", peek);
      }

      // Method to print test status with color-coded output
      static void PrintTestStatus (string methodName, bool status) {
         Console.Write ($"{methodName,-15}|");
         Console.ForegroundColor = status ? ConsoleColor.Green : ConsoleColor.Red;
         Console.WriteLine ($"{(status ? "Passed" : "Failed")}");
         Console.ResetColor ();
      }

      // Method to test the Enqueue functionality
      static bool TestEnqueue () {
         TQueue<int> tq = new ();
         Queue<int> q = new ();
         // Enqueue elements into both custom and built-in queues
         for (int i = 0; i < 4; i++) {
            tq.Enqueue (i);
            q.Enqueue (i);
         }
         // Check if counts are equal and capacity is 4
         if (tq.Count == q.Count && tq.Capacity == 4) {
            capacity = true;
            // Enqueue additional elements
            tq.Enqueue (4); q.Enqueue (4);
            tq.Enqueue (5); q.Enqueue (5);
            // Check if counts, capacity, Peek, and Dequeue operations are as expected
            if (tq.Count == q.Count && tq.Capacity == 8 &&
                tq.Peek () == q.Peek () && tq.Dequeue () == q.Dequeue ()) {
               peek = true;
               // Perform additional Dequeue operations and check final capacity
               tq.Dequeue (); q.Dequeue ();
               return tq.Capacity == 4;
            }
         }
         return false;
      }

      // Method to test the Dequeue functionality
      static bool TestDequeue () {
         TQueue<int> tq = new ();
         // Enqueue elements into the custom queue
         for (int i = 0; i < 4; i++) tq.Enqueue (i);
         // Dequeue elements and check if they match the expected values
         for (int i = 0; i < 4; i++) if (tq.Dequeue () == i) return true;
         return false;
      }
   }
}