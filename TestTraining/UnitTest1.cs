// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// UnitTest1.cs
// Program to test Double ended queue
// --------------------------------------------------------------------------------------------
using ClassLibrary;
namespace TestTraining;

[TestClass]
public class UnitTest1 {
   [TestMethod]
   public void TestRearEnqueue () {
      // Input: Enqueue the elements 1, 2, 3, and 4 at the rear of the queue
      for (int i = 1; i <= 4; i++) mQ.EnqueueRear (i);
      // Expected Output: Dequeue elements from the front and assert their order
      // The order should be 1, 2, 3, 4
      for (int j = 1; j <= 4; j++)
         Assert.AreEqual (j, mQ.DequeueFront ());
   }

   [TestMethod]
   public void TestFrontEnqueue () {
      // Input: Enqueue the elements 1, 2, 3, and 4 at the front of the queue
      for (int i = 1; i <= 4; i++) mQ.EnqueueFront (i);
      // Expected Output: Dequeue elements from the rear and assert their order
      // The order should be 1, 2, 3, 4
      for (int j = 1; j <= 4; j++) Assert.AreEqual (j, mQ.DequeueRear ());
   }

   [TestMethod]
   public void TestRearDequeue () {
      Assert.AreEqual (4, mQ.Capacity);
      Assert.IsTrue (mQ.IsEmpty);
      // Enqueue elements at the rear and front of the queue
      for (int i = 1; i <= 5; i++) {
         if ((i + 1) > 2) mQ.EnqueueFront (i);
         else mQ.EnqueueRear (2 - i);
      }
      // Check the updated queue capacity
      Assert.AreEqual (8, mQ.Capacity);
      // Dequeue elements from the rear of the queue and assert the order
      for (int j = 1; j <= 5; j++) Assert.AreEqual (j, mQ.DequeueRear ());
      // Check the final queue capacity and attempt to dequeue from an empty queue
      Assert.AreEqual (4, mQ.Capacity);
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueRear ());
   }

   [TestMethod]
   public void TestFrontDequeue () {
      // Enqueue elements at the rear and front of the queue
      for (int i = 1; i <= 5; i++) {
         if ((i + 1) > 2) mQ.EnqueueFront (i);
         else mQ.EnqueueRear (2 - i);
      }
      // Dequeue elements from the front of the queue and assert the order
      for (int j = 5; j >= 1; j--) Assert.AreEqual (j, mQ.DequeueFront ());
      // Attempt to dequeue from an empty queue
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueFront ());
   }
   TDEndQueue<int> mQ = new ();
}