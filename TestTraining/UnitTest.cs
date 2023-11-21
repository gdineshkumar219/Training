// ------------------------------------------------------------------------------------------------
// Training 
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// UnitTest.cs
// Program to test the methods in implemented Stack<T> class 
// ------------------------------------------------------------------------------------------------

using ClassLibrary;

namespace TestTraining {
   /// <summary>Unit tests for the TStack class</summary>
   [TestClass]
   public class UnitTest {
      // Create an instance of the custom stack class TStack<int>
      TStack<int> tStack = new ();
      // Create an instance of the in-built stack class Stack<int>
      Stack<int> stack = new ();
      // Initialize an array with square values for testing
      int[] sq = { 1, 4, 9, 16 };

      /// <summary>Test the Push method</summary>
      [TestMethod]
      public void TestPush () {
         // Loop to push elements into both custom and built-in stacks
         for (int i = 0; i < 4; i++) {
            // Push an element from the 'sq' array into the custom stack
            tStack.Push (sq[i]);
            // Push the same element into the built-in stack
            stack.Push (sq[i]);
         }
         // Assert that the counts of both custom and built-in stacks are equal
         Assert.AreEqual (stack.Count, tStack.Count);
         // Push an additional element (25) into the custom stack
         tStack.Push (25);
         // Assert that the capacity of the custom stack is now 8
         Assert.AreEqual (8, tStack.Capacity);
         // Pop an element from the custom stack
         tStack.Pop ();
         // Assert that the counts of both custom and built-in stacks are equal after the pop operation
         Assert.AreEqual (stack.Count, tStack.Count);
         // Push a new element (36) into both custom and built-in stacks
         stack.Push (36); tStack.Push (36);
         // Assert that the Peek() result of both stacks is equal
         Assert.AreEqual (stack.Peek (), tStack.Peek ());
      }

      /// <summary>Test the Pop method</summary>
      [TestMethod]
      public void TestPop () {
         // Assert that attempting to Pop from an empty stack throws an InvalidOperationException
         Assert.ThrowsException<InvalidOperationException> (() => tStack.Pop ());
         // Loop to push elements into both custom and built-in stacks
         for (int i = 0; i < 4; i++) {
            tStack.Push (sq[i]);
            stack.Push (sq[i]);
         }
         stack.Pop (); tStack.Pop ();
         stack.Pop (); tStack.Pop ();
         stack.Push (25); tStack.Push (25);
         // Assert that Popping from both stacks yields the same result
         Assert.AreEqual (stack.Pop (), tStack.Pop ());
      }

      /// <summary>Test the Peek method</summary>
      [TestMethod]
      public void TestPeek () {
         // Assert that attempting to Peek from an empty stack throws an InvalidOperationException
         Assert.ThrowsException<InvalidOperationException> (() => tStack.Peek ());
         // Loop to push elements into both custom and built-in stacks
         for (int i = 0; i < 4; i++) {
            tStack.Push (sq[i]);
            stack.Push (sq[i]);
         }
         // Assert that Peeking from both stacks yields the same result
         Assert.AreEqual (stack.Peek (), tStack.Peek ());
         tStack.Pop (); stack.Pop ();
         // Push a new element (36) into both custom and built-in stacks
         stack.Push (36); tStack.Push (36);
         // Assert that the Peek() result of both stacks is equal
         Assert.AreEqual (stack.Peek (), tStack.Peek ());
      }

      /// <summary>Test the Clear method</summary>
      [TestMethod]
      public void TestClear () {
         // Push elements into the custom stack
         for (int i = 0; i < 4; i++) tStack.Push (sq[i]);
         // Clear the custom stack
         tStack.Clear ();
         // Assert that the count of the custom stack is now 0
         Assert.AreEqual (0, tStack.Count);
         // Push a new element (21) into the custom stack
         tStack.Push (21);
         // Push another new element (30) into the custom stack
         tStack.Push (30);
         // Assert that the custom stack is not empty using the IsEmpty property
         Assert.IsFalse (tStack.IsEmpty);
      }

      /// <summary>To check the capacity after change count</summary>
      [TestMethod]
      public void TestCapacity () {
         // Assert that the initial capacity of the custom list is 4
         Assert.AreEqual (4, tStack.Capacity);
         // Add elements to the custom list and assert that the capacity is updated to 8
         for (int i = 0; i < 4; i++) tStack.Push (sq[i]);
         tStack.Push (25);
         Assert.AreEqual (8, tStack.Capacity);
         // Clear the custom list and assert that the capacity is reset to 4
         tStack.Clear ();
         Assert.AreEqual (4, tStack.Capacity);
      }
   }
}