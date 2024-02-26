// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Simple Program to Print the text 'Hello, World!' in Console
// ------------------------------------------------------------------------------------------------
using ClassLibrary;
namespace Training {
   internal class Program {
      static void Main (string[] args) {
         PriorityQueue<int> pq = new ();
         int n = 10;
         var random = new Random ();
         var elements = Enumerable.Range (1, n).OrderBy (_ => random.Next ()).ToArray ();
         foreach (var element in elements) pq.Enqueue (element);
         Console.WriteLine ("Priority queue after enqueue: ");
         pq.DisplayHeap ();
         Console.WriteLine (new string ('_', 50));
         Array.Sort (elements);
         foreach (var expected in elements) {
            var actual = pq.Dequeue ();
            if (actual == expected) {
               pq.DisplayHeap ();
               Console.WriteLine ($"The minimum value returned from the heap is : {actual}");
               Console.WriteLine (new string ('_', 50));
            }
         }
      }
   }
}