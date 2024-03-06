// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// UnitTest1.cs
// Test to validate methods from Priority queue
// --------------------------------------------------------------------------------------------
using ClassLibrary;
namespace TestTraining {
   [TestClass]
   public class UnitTest1 {
      [TestMethod]
      public void TestPriorityQueueIsEmpty () {
         var pq = new PriorityQueue<int> ();
         Assert.IsTrue (pq.IsEmpty);
         pq.Enqueue (0);
         Assert.IsFalse (pq.IsEmpty);
      }

      [TestMethod]
      public void TestInvalidOperation () {
         var pq = new PriorityQueue<int> ();
         Assert.ThrowsException<InvalidOperationException> (() => pq.Dequeue ());
      }

      [TestMethod]
      public void TestEnqueueDequeue () {
         var pq = new PriorityQueue<int> ();
         for (int i = 1; i <= 10; i++) pq.Enqueue (i);
         for (int i = 1; i <= 10; i++) Assert.AreEqual (i, pq.Dequeue ());
      }

      [TestMethod]
      public void TestEnqueueDequeueRandomElements () {
         int n = 1000;
         var random = new Random ();
         var pq = new PriorityQueue<int> ();
         var elements = Enumerable.Range (1, n).OrderBy (_ => random.Next ()).ToArray ();
         foreach (var element in elements) pq.Enqueue (element);
         Array.Sort (elements);
         foreach (var expected in elements) {
            var actual = pq.Dequeue ();
            Assert.AreEqual (expected, actual);
         }
      }
   }
}