// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Classlib.cs
// Program to implement double ended queue.
// ------------------------------------------------------------------------------------------------
namespace ClassLibrary {

   public class TDEndQueue<T> {
      #region Constructor -------------------------------------------
      /// <summary>Initializes a new instance of the <see cref="TDEndQue{T}"/> class</summary>
      public TDEndQueue () => mArr = new T[mCapacity];
      #endregion

      #region Properties --------------------------------------------
      /// <summary>Gets a capacity of the deque</summary>
      public int Capacity => mCapacity;

      /// <summary>Gets number of elements in the deque</summary>
      public int Count => mCount;

      /// <summary>Gets a value whether the deque is empty</summary>
      public bool IsEmpty => mCount == 0;

      /// <summary>Gets a value whether the deque is full</summary>
      public bool IsFull => mCount == mCapacity;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Adds an element to the end of the deque</summary>
      /// <param name="item">The element to add to the deque</param>
      public void EnqueueRear (T item) {
         if (mCount == mCapacity) ModifyCapacity ();
         mArr[mRear] = item;
         mRear = (mRear + 1) % mCapacity;
         mCount++;
      }

      /// <summary>Adds an element to the front of the deque</summary>
      /// <param name="item">The element to add to the deque</param>
      public void EnqueueFront (T item) {
         if (mCount == mCapacity) ModifyCapacity ();
         mFront = (mFront - 1 + mCapacity) % mCapacity;
         mArr[mFront] = item;
         mCount++;
      }

      /// <summary>Removes and returns the element at the front of the deque</summary>
      /// <returns>The element that was removed from the front of the deque</returns>
      /// <exception cref="InvalidOperationException">Thrown when the deque is empty</exception>
      public T DequeueFront () {
         if (IsEmpty)
            throw new InvalidOperationException ();
         T item = mArr[mFront];
         mArr[mFront] = default!;
         mFront = (mFront + 1) % mCapacity;
         mCount--;
         ModifyCapacity ();
         return item;
      }

      /// <summary>Removes and returns the element at the end of the deque</summary>
      /// <returns>The element that was removed from the end of the deque</returns>
      /// <exception cref="InvalidOperationException">Thrown when the deque is empty</exception>
      public T DequeueRear () {
         if (IsEmpty)
            throw new InvalidOperationException ();
         mRear = (mRear - 1 + mCapacity) % mCapacity;
         T item = mArr[mRear];
         mArr[mRear] = default!;
         mCount--;
         ModifyCapacity ();
         return item;
      }

      /// <summary>Modifies the capacity of the deque based on its current count</summary>
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
      #endregion

      #region Private Fields-----------------------------------------
      T[] mArr;
      int mCapacity = 4, mCount = 0, mFront, mRear;
      #endregion
   }
}