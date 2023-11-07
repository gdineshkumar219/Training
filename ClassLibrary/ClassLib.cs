// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Implement a custom MyList<T> class using arrays as the underlying data structure.
// The MyList<T> should start with an initial capacity of 4 and double its capacity when needed.
// Throw exceptions when necessary. 
// --------------------------------------------------------------------------------------------
namespace ClassLibrary {

   #region MyList_Class ---------------------------------------------------------------------------
   /// <summary>Represents a generic list with dynamic resizing capability</summary>
   /// <typeparam name="T">The type of elements in the list.</typeparam>
   public class MyList<T> {
      #region Constructor -------------------------------------------
      /// <summary>Initializes a new instance of the <see cref="MyList{T}"/> class</summary>
      public MyList () => mArr = new T[mCapacity];
      #endregion

      #region Properties --------------------------------------------
      /// <summary>Gets the current capacity of the list</summary>
      public int Capacity => mCapacity;

      /// <summary>Gets the number of elements contained in the list</summary>
      public int Count => mCount;

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
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Adds an element to the end of the list</summary>
      /// <param name="item">The element to add to the list</param>
      public void Add (T item) {
         if (mCount == mCapacity) ModifyCapacity ();
         mArr[mCount++] = item;
      }

      /// <summary>Removes the first occurrence of a specific element from the list</summary>
      /// <param name="item">The element to remove</param>
      /// <returns>True if the element was successfully removed</returns>
      public bool Remove (T item) {
         int index = Array.IndexOf (mArr, item);
         if (!mArr.Contains (item)) return false;
         RemoveAt (index);
         return true;
      }

      /// <summary>Clears the entire list</summary>
      public void Clear () {
         Array.Clear (mArr, 0, mCapacity);
         mCount = 0;
         mCapacity = 4;
      }

      /// <summary>Inserts elements at the specified index</summary>
      /// <param name="index"></param>
      /// <param name="item"></param>
      /// <exception cref="IndexOutOfRangeException"></exception>
      public void Insert (int index, T item) {
         if (index > mCount) throw new IndexOutOfRangeException ();
         if (mCount == mCapacity) ModifyCapacity ();
         for (int i = mCount; i > index; i--) if (mCount != index) mArr[i] = mArr[i - 1];
         mArr[index] = item;
         mCount++;
      }

      /// <summary>Removes element at the specified index</summary>
      /// <param name="index"></param>
      /// <exception cref="IndexOutOfRangeException"></exception>
      public void RemoveAt (int index) {
         if (index > mCount) throw new IndexOutOfRangeException ();
         for (int i = index; i < mCount - 1; i++) mArr[i] = mArr[i + 1];
         mArr[mCount--] = default!;
         ModifyCapacity ();
      }

      /// <summary>Modifies the capacity of the list based on its current count</summary>
      void ModifyCapacity () {
         if (mCount <= mCapacity / 2) mCapacity = mCount < 5 ? 4 : mCapacity / 2;
         else mCapacity *= 2;
         Array.Resize (ref mArr, mCapacity);
      }
      #endregion

      #region Private Fields ------------------------------------------------
      T[] mArr;
      int mCount = 0, mCapacity = 4;
      #endregion
   }
   #endregion
}
