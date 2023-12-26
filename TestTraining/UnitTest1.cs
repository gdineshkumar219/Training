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
public class TestTDEQueue {
   [TestMethod]
   public void TestProperties () {
      Assert.IsTrue (mQ.IsEmpty);
      RandomEnqueue (1);
      Assert.IsFalse (mQ.IsEmpty);
      Equals (mQ.Count, 1);
      Equals (mQ.Capacity, 4);
      RandomEnqueue (4);
      Equals (mQ.Count, 5);
      Equals (mQ.Capacity, 8);
   }

   [TestMethod]
   public void TestEnqueue () {
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (0);
      mQ.EnqueueRear (9);
      Equals (mQ, "2 1 0 9 ");
      mQ.DequeueFront ();
      mQ.EnqueueRear (5);
      mQ.EnqueueRear (5);
      Equals (mQ, "2 3 4 5 5 ");
      mQ.DequeueFront ();
      mQ.EnqueueFront (-1);
      Equals (mQ, "-1 3 4 5 5 ");
      mQ.EnqueueFront (-2);
      Equals (mQ.Count, 5);
      Equals (mQ.Capacity, 8);
      mQ.EnqueueFront (-3);
      Equals (mQ, "-3 -2 -1 3 4 5 5 ");
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (4);
      Equals (mQ, "-3 -2 -1 3 4 5 1 2 4 5 ");
   }

   [TestMethod]
   public void TestDequeue () {
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueRear ());
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueFront ());
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (3);
      mQ.EnqueueRear (4);
      Equals (mQ.DequeueFront (), 1);
      Equals (mQ.Count, 3);
      mQ.EnqueueRear (5);
      Equals (mQ.DequeueRear (), 5);
      Equals (mQ.DequeueFront (), 2);
      Equals (mQ.Count, 2);
      mQ.EnqueueFront (-1);
      mQ.EnqueueFront (-2);
      mQ.EnqueueFront (-3);
      Equals (mQ.Count, 5);
      Equals (mQ.DequeueFront (), -3);
      Equals (mQ.Capacity, 8);
      Equals (mQ.Count, 4);
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (4);
      Equals (mQ.Count, 5);
      Equals (mQ.DequeueRear (), 4);
      Equals (mQ.Capacity, 8);
      Equals (mQ.Count, 4);
      Equals (mQ.Capacity, 8);
      mQ.DequeueRear ();
      mQ.DequeueFront ();
      mQ.DequeueRear ();
      mQ.DequeueFront ();
      mQ.DequeueRear ();
      mQ.DequeueFront ();
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueRear ());
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueFront ());
   }

   /// <summary>Checks if two elements are equal.</summary>
   public void Equals (string actual, string expected) =>
      Assert.AreEqual (expected, actual);

   /// <summary>Randomly removes elements from front and back of queue.</summary>
   /// <param name="count">No of elements to remove</param>
   public void RandomDequeue (int count) {
      for (int i = 0; i < count; i++) {
         int r = mRandom.Next ();
         if (r % 2 == 0) mQ.DequeueRear ();
         else mQ.DequeueFront ();
      }
   }

   /// <summary>Randomly adds intergers to front and back of queue.</summary>
   /// <param name="count">No. of integers to be added.</param>
   public void RandomEnqueue (int count) {
      for (int i = 0; i < count; i++) {
         int r = mRandom.Next ();
         if (r % 2 == 0) mQ.EnqueueFront (i);
         else mQ.EnqueueRear (i);
      }
   }
   Random mRandom = new ();
   TDEndQueue<int> mQ = new ();
}