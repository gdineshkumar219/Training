// ----------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ----------------------------------------------------------------------------------------
// Program.cs
// Testcustomlist
// Create a test for testing the created MyList<T> with underlying structure as array using property,methods and private variables
// --------------------------------------------------------------------------------------------
using ClassLibrary;

namespace TestMyList {
   [TestClass]
   public class UnitTest1 {
      MyList<int> myList = new ();
      List<int> list = new ();

      /// <summary>Method to test Add </summary>
      [TestMethod]
      public void TestAdd () {
         for (int i = 1; i <= 5; i++) {
            myList.Add (i * i);
            list.Add (i * i);
         }
         Assert.AreEqual (myList[0], list[0]);
      }

      /// <summary>Method to test Remove</summary>
      [TestMethod]
      public void TestRemove () {
         for (int i = 1; i <= 5; i++) {
            myList.Add (i * i);
            list.Add (i * i);
         }
         list.Remove (4);
         Assert.IsTrue (myList.Remove (4));
         Assert.AreEqual (myList.Count, list.Count);
         Assert.IsFalse (myList.Remove (5));
         myList[0] = 3;
         myList[0] = 7;
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList[4]);
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList[-7] = 10);
         Assert.AreEqual (7, myList[0]);
      }

      /// <summary>Method to test Insert</summary>
      [TestMethod]
      public void TestInsert () {
         for (int i = 1; i <= 4; i++) {
            myList.Insert (i - 1, i * i);
            list.Insert (i - 1, i * i);
         }
         Assert.AreEqual (4, myList.Capacity);
         list.Insert (1, 25); myList.Insert (1, 25);
         Assert.AreEqual (list[1], myList[1]);
         Assert.AreEqual (8, myList.Capacity);
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList.Insert (8, 5));
         list.Clear (); myList.Clear ();
      }

      /// <summary>Method to test Clear</summary>
      [TestMethod]
      public void TestClear () {
         for (int i = 1; i <= 5; i++) {
            myList.Add (i * i);
            list.Add (i * i);
         }
         list.Clear (); myList.Clear ();
         Assert.AreEqual (0, myList.Count);
         list.Clear (); myList.Clear ();
      }

      /// <summary>To test RemoveAt</summary>
      [TestMethod]
      public void TestRemoveAt () {
         for (int i = 1; i <= 5; i++) {
            myList.Add (i * i);
            list.Add (i * i);
         }
         list.RemoveAt (3); myList.RemoveAt (3);
         Assert.AreEqual (list.Count, myList.Count);
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList.RemoveAt (8));
         list.Clear (); myList.Clear ();
      }

      /// <summary>To check the capacity after change count</summary>
      [TestMethod]
      public void TestCapacity () {
         Assert.AreEqual (4, myList.Capacity);
         for (int i = 1; i <= 5; i++) {
            myList.Add (i * i);
            list.Add (i * i);
         }
         Assert.AreEqual (8, myList.Capacity);
         myList.Clear ();
         Assert.AreEqual (4, myList.Capacity);
      }
   }
}