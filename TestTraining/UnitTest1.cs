// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// UnitTest1.cs
// Program to test Double ended queue
// --------------------------------------------------------------------------------------------
using ClassLibrary;
namespace TestTraining;

//[TestClass]
//public class TestTDEQueue {
//   [TestMethod]
//   public void TestProperties () {
//      Assert.IsTrue (mQ.IsEmpty);
//      RandomEnqueue (1);
//      Assert.IsFalse (mQ.IsEmpty);
//      Assert.AreEqual (mQ.Count, 1);
//      Assert.AreEqual (mQ.Capacity, 4);
//      RandomEnqueue (4);
//      Assert.AreEqual (mQ.Count, 5);
//      Assert.AreEqual (mQ.Capacity, 8);
//   }

//   [TestMethod]
//   public void TestEnqueue () {
//      mQ.EnqueueRear (2);
//      mQ.EnqueueRear (1);
//      mQ.EnqueueRear (0);
//      mQ.EnqueueRear (9);
//      Assert.AreEqual (mQ, "2 1 0 9 ");
//      mQ.DequeueFront ();
//      mQ.EnqueueRear (5);
//      mQ.EnqueueRear (5);
//      Assert.AreEqual (mQ, "2 3 4 5 5 ");
//      mQ.DequeueFront ();
//      mQ.EnqueueFront (-1);
//      Assert.AreEqual (mQ, "-1 3 4 5 5 ");
//      mQ.EnqueueFront (-2);
//      Assert.AreEqual (mQ.Count, 5);
//      Assert.AreEqual (mQ.Capacity, 8);
//      mQ.EnqueueFront (-3);
//      Assert.AreEqual (mQ, "-3 -2 -1 3 4 5 5 ");
//      mQ.EnqueueRear (1);
//      mQ.EnqueueRear (2);
//      mQ.EnqueueRear (4);
//      Assert.AreEqual (mQ, "-3 -2 -1 3 4 5 1 2 4 5 ");
//   }

//   [TestMethod]
//   public void TestDequeue () {
//      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueRear ());
//      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueFront ());
//      mQ.EnqueueRear (1);
//      mQ.EnqueueRear (2);
//      mQ.EnqueueRear (3);
//      mQ.EnqueueRear (4);
//      Assert.AreEqual(mQ.Count, 4);
//      Assert.AreEqual (mQ.DequeueFront (), 1);
//      Assert.AreEqual (mQ.Count, 3);
//      mQ.EnqueueRear (5);
//      Assert.AreEqual (mQ.DequeueRear (), 5);
//      Assert.AreEqual (mQ.DequeueFront (), 2);
//      Assert.AreEqual (mQ.Count, 2);
//      mQ.EnqueueFront (-1);
//      mQ.EnqueueFront (-2);
//      mQ.EnqueueFront (-3);
//      Assert.AreEqual (mQ.Count, 5);
//      Assert.AreEqual (mQ.DequeueFront (), -3);
//      Assert.AreEqual (mQ.Capacity, 4);
//      Assert.AreEqual (mQ.Count, 4);
//      mQ.EnqueueRear (1);
//      mQ.EnqueueRear (2);
//      mQ.EnqueueRear (4);
//      Assert.AreEqual (mQ.Count, 7);
//      Assert.AreEqual (mQ.DequeueRear (), 4);
//      Assert.AreEqual (mQ.Capacity, 8);
//      Assert.AreEqual (mQ.Count, 6);
//      mQ.DequeueRear ();
//      mQ.DequeueFront ();
//      mQ.DequeueRear ();
//      mQ.DequeueFront ();
//      mQ.DequeueRear ();
//      mQ.DequeueFront ();
//      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueRear ());
//      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueFront ());
//   }

//   /// <summary>Checks if two elements are equal.</summary>
//   //public void Equals (string actual, string expected) =>
//   //   Assert.AreEqual (expected, actual);

//   /// <summary>Randomly removes elements from front and back of queue.</summary>
//   /// <param name="count">No of elements to remove</param>
//   public void RandomDequeue (int count) {
//      for (int i = 0; i < count; i++) {
//         int r = mRandom.Next ();
//         if (r % 2 == 0) mQ.DequeueRear ();
//         else mQ.DequeueFront ();
//      }
//   }

//   /// <summary>Randomly adds intergers to front and back of queue.</summary>
//   /// <param name="count">No. of integers to be added.</param>
//   public void RandomEnqueue (int count) {
//      for (int i = 0; i < count; i++) {
//         int r = mRandom.Next ();
//         if (r % 2 == 0) mQ.EnqueueFront (i);
//         else mQ.EnqueueRear (i);
//      }
//   }
//   Random mRandom = new ();
//   TDEndQueue<int> mQ = new ();
//}
[TestClass]
public class UnitTest1 {
   TDEndQueue<int> mQ = new ();
   [TestMethod]
   public void TestRearEnqueue () {
      for (int i = 0; i < 4; i++) mQ.EnqueueRear (i + 1);
      for (int j = 0; j < 4; j++) Assert.AreEqual (j + 1, mQ.DequeueFront ());
   }

   [TestMethod]
   public void TestFrontEnqueue () {
      for (int i = 0; i < 4; i++) mQ.EnqueueFront (i + 1);
      for (int j = 0; j < 4; j++) Assert.AreEqual (j + 1, mQ.DequeueRear ());
   }

   [TestMethod]
   public void TestRearDequeue () {
      Assert.AreEqual (4, mQ.Capacity);
      Assert.IsTrue (mQ.IsEmpty);
      for (int i = 0; i < 5; i++) {
         if ((i + 1) > 2) mQ.EnqueueFront (i + 1);
         else mQ.EnqueueRear (2 - i);
      }
      Assert.AreEqual (mQ.Capacity,8);
      for (int j = 0; j < 5; j++) Assert.AreEqual (j + 1, mQ.DequeueRear ());
      Assert.AreEqual(4, mQ.Capacity);
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueRear ());
   }

   [TestMethod]
   public void TestFrontDequeue () {
      for (int i = 0; i < 5; i++) {
         if ((i + 1) > 2) mQ.EnqueueFront (i + 1);
         else mQ.EnqueueRear (2 - i);
      }
      for (int j = 5; j > 0; j--) Assert.AreEqual (j, mQ.DequeueFront ());
      Assert.ThrowsException<InvalidOperationException> (() => mQ.DequeueFront ());
   }
}