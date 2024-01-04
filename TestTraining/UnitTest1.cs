// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// Program.cs
// --------------------------------------------------------------------------------------------

using ClassLibrary;

namespace TestTraining {
   [TestClass]
   public class UnitTest1 {
      // Create an instance of the custom queue class TQueue<int>
      TQueue<int> tq = new ();
      // Create an instance of the in-built queue class Queue<int>
      Queue<int> q = new ();
      // Initialize an array with square values for testing
      int[] sq = { 1, 4, 9, 16 };

      /// <summary>Test method to verify Enqueue</summary>
      [TestMethod]
      public void TestEnqueue () {
         // Enqueue four elements to both tested queue (tq) and reference queue (q)
         for (int i = 0; i < 4; i++) {
            tq.Enqueue (sq[i]);  // Enqueue element to tested queue
            q.Enqueue (sq[i]);   // Enqueue element to reference queue
         }
         // Assert that the counts of both queues are equal
         Assert.AreEqual (q.Count, tq.Count);
         // Enqueue the element '25' to both queues
         tq.Enqueue (25); q.Enqueue (25);
         // Assert that the capacity of the tested queue has been updated to 8
         Assert.AreEqual (8, tq.Capacity);
         // Dequeue an element from both queues
         tq.Dequeue (); q.Dequeue ();
         // Assert that the counts of both queues are still equal
         Assert.AreEqual (q.Count, tq.Count);
         // Enqueue the element '36' to both queues
         q.Enqueue (36); tq.Enqueue (36);
         // Assert that the Peek operation on both queues returns the same value
         Assert.AreEqual (q.Peek (), tq.Peek ());
      }

      /// <summary>Test method to verify Dequeue operation/// </summary>
      [TestMethod]
      public void TestDequeue () {
         // Assert that attempting to Dequeue from an empty queue throws an exception
         Assert.ThrowsException<InvalidOperationException> (() => tq.Dequeue ());
         // Enqueue four elements to both tested queue (tq) and reference queue (q)
         for (int i = 0; i < 4; i++) {
            tq.Enqueue (sq[i]);  // Enqueue element to tested queue
            q.Enqueue (sq[i]);   // Enqueue element to reference queue
         }
         // Dequeue two elements from both queues
         q.Dequeue (); tq.Dequeue ();
         q.Dequeue (); tq.Dequeue ();
         // Enqueue the element '25' to both queues and assert that the Dequeue operation returns the same value
         Assert.AreEqual (q.Dequeue (), tq.Dequeue ());
      }

      /// <summary>Test method to verify Peek operation/// </summary>
      [TestMethod]
      public void TestPeek () {
         // Assert that attempting to Peek from an empty queue throws an exception
         Assert.ThrowsException<InvalidOperationException> (() => tq.Peek ());
         // Enqueue four elements to both tested queue (tq) and reference queue (q)
         for (int i = 0; i < 4; i++) {
            tq.Enqueue (sq[i]);  // Enqueue element to tested queue
            q.Enqueue (sq[i]);   // Enqueue element to reference queue
         }
         // Assert that the Peek operation on both queues returns the same value
         Assert.AreEqual (q.Peek (), tq.Peek ());
         // Dequeue an element from both queues
         tq.Dequeue (); q.Dequeue ();
         // Enqueue the element '36' to both queues and assert that the Peek operation returns the same value
         Assert.AreEqual (q.Peek (), tq.Peek ());
      }

      /// <summary>Test method to verify ModifyCapacity operation/// </summary>
      [TestMethod]
      public void TestModifyCapacity () {
         // Assert that the initial capacity of the tested queue is 4
         Assert.AreEqual (4, tq.Capacity);
         // Enqueue four elements to the tested queue
         for (int i = 0; i < 4; i++) tq.Enqueue (sq[i]);
         // Enqueue an additional element to the tested queue, triggering a capacity increase to 8
         tq.Enqueue (25);
         // Assert that the capacity of the tested queue has been updated to 8
         Assert.AreEqual (8, tq.Capacity);
         // Dequeue an element from the tested queue, triggering a capacity decrease back to 4
         tq.Dequeue ();
         // Assert that the capacity of the tested queue has been updated back to 4
         Assert.AreEqual (4, tq.Capacity);
      }
   }
}
