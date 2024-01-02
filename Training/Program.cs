// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// --------------------------------------------------------------------------------------------
using ClassLibrary;
namespace Training {
   internal class Program {
      static void Main () {
         TDEndQueue<int> myQueue = new ();
         // Enqueue elements to the rear of the deque
         myQueue.EnqueueRear (1);
         myQueue.EnqueueRear (2);
         myQueue.EnqueueRear (3);
         // Enqueue elements to the front of the deque
         myQueue.EnqueueFront (0);
         // Display the current state of the deque
         Console.WriteLine ($"Deque Capacity: {myQueue.Capacity}");
         Console.WriteLine ($"Deque Count: {myQueue.Count}");
         Console.WriteLine ($"Is Deque Empty: {myQueue.IsEmpty}");
         Console.WriteLine ($"Is Deque Full: {myQueue.IsFull}");
         // Dequeue elements from the front and rear of the deque
         int frontElement = myQueue.DequeueFront ();
         int rearElement = myQueue.DequeueRear ();
         // Display the deque after dequeuing
         Console.WriteLine ($"Dequeued Front Element: {frontElement}");
         Console.WriteLine ($"Dequeued Rear Element: {rearElement}");
         // Display the updated state of the deque
         Console.WriteLine ($"Updated Deque Count: {myQueue.Count}");
         // Enqueue more elements to demonstrate capacity modification
         myQueue.EnqueueRear (4);
         myQueue.EnqueueRear (5);
         myQueue.EnqueueRear (6);
         myQueue.DequeueRear ();
         // Display the final state of the deque
         Console.WriteLine ($"Final Deque Count: {myQueue.Capacity}");
         Console.WriteLine ($"Is Deque Empty: {myQueue.IsEmpty}");
         Console.WriteLine ($"Is Deque Full: {myQueue.IsFull}");
      }
   }
}
