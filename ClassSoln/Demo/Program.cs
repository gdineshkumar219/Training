// ---------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.                                                
// ---------------------------------------------------------------------------
// Program.cs                                                                     
// Program to Implement a Stack<T> class using arrays as the underlying data structure.
// The Stack<T> should start with an initial capacity of 4 and double its capacity when needed.
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
      #region Methods -----------------------------------------------
      /// <summary>Adds an element to the end of the queue</summary>
      /// <param name="item">The element to add to the queue</param>
      public void Enqueue (T item) {
         ModifyCapacity ();
         mArr[mCount++] = item;
      }

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

      /// <summary>Removes and returns the element at the beginning of the queue</summary>
      /// <returns>The element that was removed from the beginning of the queue</returns>
      public T Dequeue () {
         if (IsEmpty)
            throw new InvalidOperationException ();
         T item = mArr[0];
         Array.Copy (mArr, 1, mArr, 0, --mCount);
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
      public int ModifyCapacity () {
         if (mCount == mCapacity) mCapacity *= 2;
         else if (mCount <= mCapacity / 2) mCapacity = mCount < 5 ? 4 : mCapacity / 2;
         Array.Resize (ref mArr, mCapacity);
         return mCapacity;
      }

      /// <summary>Returns total elements in queue</summary>
      /// <returns>Total number of elements present</returns>
      public int Count () => mCount;

      /// <summary>Gets a value indicating whether the queue is empty</summary>
      public bool IsEmpty => mCount == 0;
      #endregion
      #region Fields ------------------------------------------------
      T[] mArr;
      int mCapacity = 4, mCount = 0;
      #endregion
   }
   #endregion

   internal class Program {
      static void Main () { }
   }
}