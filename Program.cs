// ---------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.                                                
// ---------------------------------------------------------------------------
// Program.cs                                                                     
// Program to Implement a Stack<T> class using arrays as the underlying data structure.
// The Stack<T> should start with an initial capacity of 4 and double its capacity when needed.
// class TStack<T> {
//    public void Push (T item) { }
//    public T Pop () { }
//    public T Peek () { }
//    public bool IsEmpty { get; }
// }
// ---------------------------------------------------------------------------------------

namespace Training {
   #region TStack<T> ---------------------------------------------------------------------
   class TStack<T> {
      #region Constructor -------------------------------------------
      /// <summary>Initializes a new instance of the TStack class</summary>
      public TStack () => mArr = new T[mCapacity];
      #endregion

      #region Methods ----------------------------------------------------------------------
      /// <summary>Pushes an element onto the stack</summary>
      /// <param name="item">The item to be pushed onto the stack</param>
      public void Push (T item) {
         ModifyCapacity ();
         mArr[mCount++] = item;
      }

      /// <summary>Removes and returns the top element from the stack</summary>
      public T Pop () {
         if (IsEmpty) throw new InvalidOperationException ("Cannot pop from an empty stack.");
         T item = mArr[--mCount];
         mArr[mCount] = default;
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

      /// <summary>Gets a value indicating whether the stack is empty</summary>
      public bool IsEmpty => mCount == 0;

      /// <summary>Clears the stack, removing all elements</summary>
      public void Clear () {
         Array.Clear (mArr, 0, mCapacity);
         mCount = 0;
         ModifyCapacity ();
      }

      /// <summary>Modifies the capacity based on its current count</summary>
      void ModifyCapacity () {
         if (mCount == mCapacity) mCapacity *= 2;
         else if (mCount <= mCapacity / 2) mCapacity = mCount < 5 ? 4 : mCapacity / 2;
         Array.Resize (ref mArr, mCapacity);
      }
      #endregion

      #region Private Fields ---------------------------------------- 
      T[] mArr;
      int mCapacity = 4, mCount = 0;
      #endregion
   }
   #endregion

   internal class Program {
      static void Main () {
         var tStack = new TStack<int> ();
         tStack.Push (1);
         tStack.Push (2);
         tStack.Push (3);
         Console.WriteLine ($"First Pop using TStack: {tStack.Pop ()}");
         Console.WriteLine ($"Peek using Tstack: {tStack.Peek ()}");
         Console.WriteLine ($"Second Pop using TStack: {tStack.Pop ()}");
         Console.WriteLine ($"Check IsEmpty using TStack: {tStack.IsEmpty}");
         tStack.Clear ();
         Console.WriteLine ($"Check IsEmpty using TStack: {tStack.IsEmpty}");
         Console.WriteLine ("\n-------------------------------------------------\n");
         var stack = new Stack<int> ();
         stack.Push (1);
         stack.Push (2);
         stack.Push (3);
         Console.WriteLine ($"First Pop using TStack: {stack.Pop ()}");
         Console.WriteLine ($"Peek using Tstack: {stack.Peek ()}");
         Console.WriteLine ($"Second Pop using TStack: {stack.Pop ()}");
         Console.WriteLine ($"Check IsEmpty using Stack: {stack.Count == 0}");
         stack.Clear ();
         Console.WriteLine ($"Check IsEmpty using Stack: {stack.Count == 0}");
      }
   }
}