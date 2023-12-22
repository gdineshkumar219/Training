// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// Program.cs
// --------------------------------------------------------------------------------------------

using ClassLibrary;
namespace TestTraining;

[TestClass]
public class TestTDEQueue {
   TDEQueue<int> mQ = new TDEQueue<int> ();

   //[TestMethod]
   //public void TestProperties () {
   //   TDEQueue<int> mQ = new TDEQueue<int> ();
   //   Assert.IsTrue (mQ.IsEmpty);
   //   // Enqueue 1
   //   mQ.EnqueueRear (1);

   //   // Assert
   //   Assert.IsFalse (mQ.IsEmpty);
   //   Assert.AreEqual (1, mQ.Count);
   //   Assert.AreEqual (4, mQ.Capacity);

   //   // Enqueue 4
   //   mQ.EnqueueRear (4);

   //   // Assert
   //   Assert.AreEqual (5, mQ.Count);
   //   Assert.AreEqual (8, mQ.Capacity);
   //}

   [TestMethod]
   public void TestEnqueue () {
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (3);
      mQ.EnqueueRear (4);
      Assert.Equals (" 1 2 3 4 ", mQ);
      mQ.DequeueFront ();
      mQ.EnqueueRear (5);
      mQ.EnqueueRear (5); 
      Assert.Equals ("2 3 4 5 5 ", mQ);
      mQ.DequeueFront ();
      mQ.EnqueueFront (-1);
      Assert.Equals ("-1 3 4 5 5 ", mQ);
      mQ.EnqueueFront (-2);
      Assert.AreEqual (5, mQ.Count);
      Assert.AreEqual (8, mQ.Capacity);
      mQ.EnqueueFront (-3);
      Assert.Equals ("-3 -2 -1 3 4 5 5 ", mQ);
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (4);
      Assert.AreEqual ("-3 -2 -1 3 4 5 1 2 4 5 ", mQ);
   }

   [TestMethod]
   public void TestDequeue () {
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueRear ());
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueFront ());
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (3);
      mQ.EnqueueRear (4);
      Assert.AreEqual (1, mQ.DequeueFront ());
      Assert.AreEqual (3, mQ.Count);
      mQ.EnqueueRear (5);
      Assert.AreEqual(8, mQ.Capacity);
      Assert.AreEqual (5, mQ.DequeueRear ());
      Assert.AreEqual (2, mQ.DequeueRear ());
      Assert.AreEqual (4, mQ.Capacity);
      Assert.AreEqual (2, mQ.Count);
      mQ.EnqueueFront (-1);
      mQ.EnqueueFront (-2);
      mQ.EnqueueFront (-3);
      Assert.AreEqual (5, mQ.Count);
      Assert.AreEqual (-3, mQ.DequeueFront ());
      Assert.AreEqual (4, mQ.Count);
      Assert.AreEqual (8, mQ.Capacity);
      mQ.EnqueueRear (1);
      mQ.EnqueueRear (2);
      mQ.EnqueueRear (4);
      Assert.AreEqual (4, mQ.DequeueRear ());
      Assert.AreEqual (8, mQ.Capacity);
      Assert.AreEqual (4, mQ.Count);
   }
   public void Equals (string expected, string actual) =>
     Assert.AreEqual (expected, actual);
}
