// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// ------------------------------------------------------------------------------------------------
using System.Diagnostics;

namespace ClassLibrary {
   #region PriorityQueue --------------------------------------------------------------------------
   /// <summary> Represents a priority queue data structure implemented using a binary heap </summary>
   public class PriorityQueue<T> where T : IComparable<T> {
      #region Constructor -------------------------------------------------------------------------
      /// <summary> Initializes a new instance of the <see cref="PriorityQueue{T}"/> class</summary>
      public PriorityQueue () => mHeapList = new List<T> ();
      #endregion

      #region Methods -----------------------------------------------------------------------------
      /// <summary> Gets a value indicating whether the priority queue is empty</summary>
      public bool IsEmpty => mHeapList.Count == 0;

      /// <summary>Inserts an element into the priority queue</summary>
      /// <param name="value">The value to insert</param>
      public void Enqueue (T value) {
         mHeapList.Add (value);
         SiftUp ();
      }

      /// <summary> Removes and returns the element with the highest priority in the priority queue </summary>
      ///<returns>The element with the highest priority</returns>
      public T Dequeue () {
         if (IsEmpty) throw new InvalidOperationException ("Priority queue is empty");
         T root = mHeapList[0];
         mHeapList[0] = mHeapList[^1];
         mHeapList.RemoveAt (mHeapList.Count - 1);
         SiftDown (0);
         return root;
      }

      /// <summary> Adjusts the heap upward from the specified index to maintain the heap property</summary>
      /// <param name="index">The index to start heapifying up from</param>
      void SiftUp () {
         int index = mHeapList.Count - 1;
         int parentIndex = (index - 1) / 2;
         while (index > 0 && mHeapList[index].CompareTo (mHeapList[parentIndex]) < 0) {
            Swap (index, parentIndex);
            index = parentIndex;
            parentIndex = (index - 1) / 2;
         }
      }


      /// <summary> Adjusts the heap downward from the specified index to maintain the heap property</summary>
      /// <param name="index">The index to start heapifying down from</param>
      void SiftDown (int index) {
         int leftChildIndex = 2 * index + 1;
         int rightChildIndex = 2 * index + 2;
         int smallestIndex = index;
         if (leftChildIndex < mHeapList.Count && mHeapList[leftChildIndex].CompareTo (mHeapList[smallestIndex]) < 0)
            smallestIndex = leftChildIndex;
         if (rightChildIndex < mHeapList.Count && mHeapList[rightChildIndex].CompareTo (mHeapList[smallestIndex]) < 0)
            smallestIndex = rightChildIndex;
         if (smallestIndex != index) {
            Swap (index, smallestIndex);
            SiftDown (smallestIndex);
         }
      }

      /// <summary> Swaps the elements at the specified indices in the heap </summary>
      /// <param name="i">The index of the first element </param>
      /// <param name="j">The index of the second element</param>
      private void Swap (int i, int j) => (mHeapList[j], mHeapList[i]) = (mHeapList[i], mHeapList[j]);

      /// <summary>Displays the heap </summary>

      public void DisplayHeap () {
         Console.ResetColor ();
         DisplayHeapAsTree (0, 0);
      }

      private void DisplayHeapAsTree (int index, int depth) {
         if (index < mHeapList.Count) {
            ConsoleColor originalColor = Console.ForegroundColor;
            // Set color based on depth (root, left, right)
            if (depth == 0) Console.ForegroundColor = ConsoleColor.Red; // Root node color
            else if (index % 2 == 0) Console.ForegroundColor = ConsoleColor.Blue; // Right child color
            else Console.ForegroundColor = ConsoleColor.Green; // Left child color
            if (index < mHeapList.Count) {
               DisplayHeapAsTree (2 * index + 2, depth + 1);
               Console.Write (new string (' ', 4 * depth));
               Console.WriteLine (mHeapList[index]);
               DisplayHeapAsTree (2 * index + 1, depth + 1);
            }
            Console.ForegroundColor = originalColor;
         }
      }
      #endregion

      #region Private data ------------------------------------------------------------------------
      private List<T> mHeapList;
      #endregion
   }
   #endregion
}