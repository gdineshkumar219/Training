﻿// ---------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.                                                
// ---------------------------------------------------------------------------
// Program.cs                                                                     
// Program to Implement a TQueue<T> class using arrays as the underlying data structure.
// TQueue<T> should start with an initial capacity of 4 and double its capacity when needed.
// class TQueue<T> {
//    public void Enqueue (T a) { } 
//    public T Dequeue () { }
//    public T Peek () { }
//    public bool IsEmpty {get;}
//}
// ---------------------------------------------------------------------------------------

namespace Training {
   #region ClassTQueue ----------------------------------------------------------------------------
   public class TQueue<T> {
      #region Constructor -------------------------------------------
      /// <summary>Initializes a new instance of the <see cref="TQueue{T}"/> class</summary>
      public TQueue () => mArr = new T[mCapacity];
      #endregion

      #region Properties --------------------------------------------
      /// <summary>Gets a capacity of the queue</summary>
      public int Capacity => mCapacity;

      /// <summary>Gets number of elements in the queue</summary>
      public int Count () => mCount;

      /// <summary>Gets a value whether the stack is empty</summary>
      public bool IsEmpty => mCount == 0;

      /// <summary>Gets a value whether the stack is empty</summary>
      public bool IsFull => mCount == mCapacity;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Adds an element to the end of the queue</summary>
      /// <param name="item">The element to add to the queue</param>
      public void Enqueue (T item) {
         if (mCount == mCapacity) ModifyCapacity ();
         mArr[mRear] = item;
         mRear = (mRear + 1) % mCapacity;
         mCount++;
      }

      /// <summary>Removes and returns the element at the beginning of the queue</summary>
      /// <returns>The element that was removed from the beginning of the queue</returns>
      /// <exception cref="InvalidOperationException">Thrown when the queue is empty</exception>
      public T Dequeue () {
         if (IsEmpty)
            throw new InvalidOperationException ("The queue is empty.");
         T item = mArr[mFront];
         mArr[mFront] = default;
         mFront = (mFront + 1) % mCapacity;
         mCount--;
         ModifyCapacity ();
         return item;
      }

      /// <summary>Returns the element at the beginning of the queue without removing it</summary>
      /// <returns>The element at the beginning of the queue</returns>
      /// <exception cref="InvalidOperationException">Thrown when the queue is empty</exception>
      public T Peek () {
         if (IsEmpty)
            throw new InvalidOperationException ("The queue is empty.");
         return mArr[mFront];
      }

      /// <summary>Modifies the capacity of the queue based on its current count</summary>
      void ModifyCapacity () {
         int newCapacity;
         if (mCount <= mCapacity / 2) newCapacity = mCount < 5 ? 4 : mCapacity / 2;
         else newCapacity = mCapacity * 2;
         T[] newArray = new T[newCapacity];
         if (mFront < mRear) Array.Copy (mArr, mFront, newArray, 0, mCount);
         else {
            Array.Copy (mArr, mFront, newArray, 0, mCapacity - mFront);
            Array.Copy (mArr, 0, newArray, mCapacity - mFront, mRear);
         }
         mArr = newArray;
         mFront = 0;
         mRear = mCount;
         mCapacity = newCapacity;
      }

      /// <summary>Displays the elements in the queue</summary>
      public void DisplayQueue () {
         if (IsEmpty) {
            Console.WriteLine ("The queue is empty.");
            return;
         }
         int i = mFront;
         Console.Write ("Elements in the queue: ");
         do {
            Console.Write (mArr[i] + " ");
            i = (i + 1) % mCapacity;
         } while (i != mRear);
         Console.WriteLine ();
      }
      #endregion

      #region Private Fields-----------------------------------------
      T[] mArr;
      int mCapacity = 4, mCount = 0, mFront = 0, mRear = 0;
      #endregion
   }
   #endregion
   internal class Program {
      #region Methods -----------------------------------------------
      static void Main () {
         TestTQueue ();
         TestBuiltInQueue ();
      }

      /// <summary>Tests the custom TQueue class</summary>
      static void TestTQueue () {
         Console.WriteLine ("Testing TQueue<T>");
         var tQueue = new TQueue<int> ();
         tQueue.Enqueue (1);
         tQueue.DisplayQueue ();
         for (int i = 1; i < 4; i++) tQueue.Enqueue (i * 3);
         tQueue.DisplayQueue ();
         Console.WriteLine ($"No of elements present: {tQueue.Count ()}");
         Console.WriteLine ($"Capacity: {tQueue.Capacity}");
         Console.WriteLine ($"Dequeued element:{tQueue.Dequeue ()}");
         tQueue.DisplayQueue ();
         Console.WriteLine ($"Peek:  {tQueue.Peek ()}");
         tQueue.Enqueue (37);
         tQueue.DisplayQueue ();
         tQueue.Enqueue (19);
         tQueue.DisplayQueue ();
         Console.WriteLine ($"Is Queue empty: {tQueue.IsEmpty}");
         Console.WriteLine ($"Is Queue full: {tQueue.IsFull}");
         Console.WriteLine ($"Capacity: {tQueue.Capacity}");
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         tQueue.DisplayQueue ();
         tQueue.Dequeue ();
         Console.WriteLine ($"Capacity: {tQueue.Capacity}");
         tQueue.DisplayQueue ();
         Console.WriteLine ("----------------------------------\n");
      }

      /// <summary>Tests the custom built-in Queue class</summary>
      static void TestBuiltInQueue () {
         Console.WriteLine ("Testing built-in Queue<T>");
         var queue = new Queue<int> ();
         queue.Enqueue (1);
         Console.WriteLine (queue.ToString ());
         for (int i = 0; i < 3; i++) queue.Enqueue (i * 3);
         Console.WriteLine ($"Dequeued element:{queue.Dequeue ()}");
         Console.WriteLine ($"Peek:  {queue.Peek ()}");
         queue.Enqueue (7);
         queue.Enqueue (9);
         Console.WriteLine ($"Is Queue empty: {queue.Count == 0}");
      }
      #endregion
   }
}