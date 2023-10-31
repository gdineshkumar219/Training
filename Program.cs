// ---------------------------------------------------------------------------------------
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
      /// <summary>Gets or sets the element at the specified index</summary>
      /// <param name="index">The zero-based index of the element to get or set</param>
      /// <returns>The element at the specified index.</returns>
      /// <exception cref="IndexOutOfRangeException">Thrown when the index is out of range [0, Count)</exception>
      public T this[int index] {
         get {
            if (index < 0 || index >= mCount)
               throw new IndexOutOfRangeException ($"Index {index} is out of range [0, {mCount}).");
            return mArr[index];
         }
         set {
            if (index < 0 || index >= mCount)
               throw new IndexOutOfRangeException ($"Index {index} is out of range [0, {mCount}).");
            mArr[index] = value;
         }
      }

      public int Capacity => mCapacity;

      /// <summary>Gets a value indicating whether the queue is empty</summary>
      public bool IsEmpty => mCount == 0;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Adds an element to the end of the queue</summary>
      /// <param name="item">The element to add to the queue</param>
      public void Enqueue (T item) {
         if (mCapacity == mCount) ModifyCapacity ();
         mArr[mCount++] = item;
      }

      /// <summary>Removes and returns the element at the beginning of the queue</summary>
      /// <returns>The element that was removed from the beginning of the queue</returns>
      public T Dequeue () {
         if (IsEmpty)
            throw new InvalidOperationException ();
         T item = mArr[0];
         Array.Copy (mArr, 1, mArr, 0, --mCount);
         ModifyCapacity ();
         return item;
      }

      /// <summary>Returns the element at the beginning of the queue without removing it</summary>
      /// <returns>The element at the beginning of the queue</returns>
      public T Peek () {
         if (IsEmpty)
            throw new InvalidOperationException ("The queue is empty.");
         return mArr[0];
      }

      /// <summary>Modifies the capacity based on its current count</summary>
      void ModifyCapacity () {
         if (mCount <= mCapacity / 2) mCapacity = mCount < 5 ? 4 : mCapacity / 2;
         else mCapacity *= 2;
         Array.Resize (ref mArr, mCapacity);
      }

      /// <summary>Returns total elements in queue</summary>
      /// <returns>Total number of elements present</returns>
      public int Count () => mCount;
      #endregion

      #region Fields ------------------------------------------------
      T[] mArr;
      int mCapacity = 4, mCount = 0;
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
         Console.WriteLine ($"Initial capacity of queue: {tQueue.Capacity}");
         for (int i = 0; i < 10; i++) tQueue.Enqueue (i + 1);
         Console.WriteLine ($"Capacity after enqueuing elements: {tQueue.Capacity}");
         Console.WriteLine ($"Peek:  {tQueue.Peek ()}");
         Console.WriteLine ($"Dequeued: {tQueue.Dequeue ()}");
         Console.WriteLine ($"Is Empty: {tQueue.IsEmpty}");
         Console.WriteLine ($"Count: {tQueue.Count ()}");
         Console.WriteLine ($"Element at index 5: {tQueue[5]}");
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         tQueue.Dequeue ();
         Console.WriteLine ($"Capacity afer dequeue: {tQueue.Capacity}");
         Console.WriteLine ("--------------------------------\n");
      }

      /// <summary>Tests the custom built-in Queue class</summary>
      static void TestBuiltInQueue () {
         Console.WriteLine ("Testing built-in Queue<T>");
         var queue = new Queue<int> ();
         queue.Enqueue (1);
         Console.WriteLine ($"Peek:  {queue.Peek ()}");
         Console.WriteLine ($"Dequeued: {queue.Dequeue ()}");
         Console.WriteLine ($"Is Empty: {queue.Count == 0}");
         for (int i = 0; i < 10; i++) queue.Enqueue (i + 1);
         Console.WriteLine ($"Count: {queue.Count ()}");
         Console.WriteLine ($"Element at index 5: {queue.ElementAt (5)}");
         Console.WriteLine ("--------------------------------\n");
      }
      #endregion
   }
}