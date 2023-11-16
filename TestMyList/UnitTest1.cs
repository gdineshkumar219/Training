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
      // Create instances of MyList<int>, List<int>, and an array of integers
      MyList<int> myList = new ();
      List<int> list = new ();
      int[] sq = { 1, 4, 9, 16, 25 };

      /// <summary>Method to test Add </summary>
      [TestMethod]
      public void TestAdd () {
         // Add elements to both custom list and standard list
         for (int i = 0; i < sq.Length; i++) {
            myList.Add (i);
            list.Add (i);
         }
         // Assert that the first element in both lists is equal
         Assert.AreEqual (myList[0], list[0]);
      }

      /// <summary>Method to test Remove</summary>
      [TestMethod]
      public void TestRemove () {
         // Add elements to both custom list and standard list
         for (int i = 0; i < sq.Length; i++) {
            myList.Add (sq[i]);
            list.Add (sq[i]);
         }
         // Remove an element from both lists
         list.Remove (4);
         Assert.IsTrue (myList.Remove (4));
         // Assert that counts of both lists are equal
         Assert.AreEqual (myList.Count, list.Count);
         // Try to remove an element that doesn't exist in the custom list
         Assert.IsFalse (myList.Remove (5));
         // Modify and access elements in the custom list, and assert expected exceptions
         myList[0] = 3;
         myList[0] = 7;
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList[4]);
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList[-7] = 10);
         Assert.AreEqual (7, myList[0]);
      }

      /// <summary>Method to test Insert</summary>
      [TestMethod]
      public void TestInsert () {
         // Insert elements at specified positions in both custom list and standard list
         for (int i = 0; i < sq.Length - 1; i++) {
            myList.Insert (i, sq[i]);
            list.Insert (i, sq[i]);
         }
         // Assert that the capacity of the custom list is 4
         Assert.AreEqual (4, myList.Capacity);
         // Insert an element at a specific position in both lists
         list.Insert (1, 25); myList.Insert (1, 25);
         // Assert that the elements at the inserted position are equal
         Assert.AreEqual (list[1], myList[1]);
         // Assert that the capacity of the custom list is 8
         Assert.AreEqual (8, myList.Capacity);
         // Try to insert an element at an invalid position and clear both lists
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList.Insert (8, 5));
      }

      /// <summary>Method to test Clear</summary>
      [TestMethod]
      public void TestClear () {
         // Add elements to the custom list
         for (int i = 0; i < sq.Length; i++) myList.Add (sq[i]);
         // Clear the custom list and assert that the count is 0
         myList.Clear ();
         Assert.AreEqual (0, myList.Count);
      }

      /// <summary>To test RemoveAt</summary>
      [TestMethod]
      public void TestRemoveAt () {
         // Add elements to both custom list and standard list
         for (int i = 0; i < sq.Length; i++) {
            myList.Add (sq[i]);
            list.Add (sq[i]);
         }
         // Remove an element at a specific index from both lists
         list.RemoveAt (3); myList.RemoveAt (3);
         // Assert that counts of both lists are equal
         Assert.AreEqual (list.Count, myList.Count);
         // Try to remove an element at an invalid index and assert expected exception
         Assert.ThrowsException<IndexOutOfRangeException> (() => myList.RemoveAt (8));
      }

      /// <summary>To check the capacity after change count</summary>
      [TestMethod]
      public void TestCapacity () {
         // Assert that the initial capacity of the custom list is 4
         Assert.AreEqual (4, myList.Capacity);
         // Add elements to the custom list and assert that the capacity is updated to 8
         for (int i = 0; i < sq.Length; i++) myList.Add (sq[i]);
         Assert.AreEqual (8, myList.Capacity);
         // Clear the custom list and assert that the capacity is reset to 4
         myList.Clear ();
         Assert.AreEqual (4, myList.Capacity);
      }
   }
}