// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------
// ClassLib.cs
// Program to Implement a Stack<T> class using arrays as the underlying data structure.
// The Stack<T> should start with an initial capacity of 4 and double its capacity when needed.
// class TStack<T> {
//    public void Push (T item) { }
//    public T Pop () { }
//    public T Peek () { }
//    public bool IsEmpty { get; }
// }
// --------------------------------------------------------------------------------------------

namespace ClassLibrary {
   #region TStack<T> ---------------------------------------------------------------------
   public class TStack<T> {
      #region Constructor -------------------------------------------
      /// <summary>Initializes a new instance of the TStack class</summary>
      public TStack () => mArr = new T[mCapacity];
      #endregion

      #region Properties --------------------------------------------
      public int Count => mCount;
      public int Capacity => mCapacity;

      /// <summary>Gets a value indicating whether the stack is empty</summary>
      public bool IsEmpty => mCount == 0;
      #endregion

      #region Methods ----------------------------------------------------------------------
      /// <summary>Pushes an element onto the stack</summary>
      /// <param name="item">The item to be pushed onto the stack</param>
      public void Push (T item) {
         if (mCount == mCapacity) ModifyCapacity ();
         mArr[mCount++] = item;
      }

      /// <summary>Removes and returns the top element from the stack</summary>
      public T Pop () {
         if (IsEmpty) throw new InvalidOperationException ("Cannot pop from an empty stack.");
         T item = mArr[--mCount];
         mArr[mCount] = default!;
         ModifyCapacity ();
         return item;
      }

      /// <summary>Returns the top element of the stack without removing it</summary>
      /// <returns>Top element of the stack</returns>
      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Cannot peek an empty stack.");
         T item = Pop ();
         Push (item);
         return item;
      }

      /// <summary>Clears the stack, removing all elements</summary>
      public void Clear () {
         Array.Clear (mArr, 0, mCapacity);
         mCount = 0;
         ModifyCapacity ();
      }

      /// <summary>Modifies the capacity based on its current count</summary>
      void ModifyCapacity () {
         if (mCount <= mCapacity / 2) mCapacity = mCount < 5 ? 4 : mCapacity / 2;
         else mCapacity *= 2;
         Array.Resize (ref mArr, mCapacity);
      }
      #endregion

      #region Private Fields ---------------------------------------- 
      T[] mArr;
      int mCapacity = 4, mCount = 0;
      #endregion
   }
}
#endregion