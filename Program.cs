namespace Training {
   class TStack<T> {
      private T[] mArr;
      private int mCapacity = 4, mCount = 0;

      /// <summary>Pushes an element onto the stack</summary>
      /// <param name="item">The item to be pushed onto the stack</param>
      public void Push (T item) {
         try {
            ModifyCapacity ();
            mArr[mCount++] = item;
         } catch (IndexOutOfRangeException) { throw new IndexOutOfRangeException (); }
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
      /// <exception cref="InvalidOperationException"></exception>
      public T Peek () {
            if (IsEmpty) throw new InvalidOperationException ("Cannot peek an empty stack.");
            T item = Pop ();
            Push(item);
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

      /// <summary>Initializes a new instance of the TStack class</summary>
      public TStack () => mArr = new T[mCapacity];
   }

   internal class Program {
      static void Main () {
         var intTStack = new TStack<int> ();
         intTStack.Push (1);
         intTStack.Push (2);
         intTStack.Push (3);
         Console.WriteLine ($"First Pop using TStack: {intTStack.Pop ()}"); // 3
         Console.WriteLine ($"Peek using Tstack: {intTStack.Peek ()}"); // 2
         Console.WriteLine ($"Second Pop using TStack: {intTStack.Pop ()}"); // 2
         Console.WriteLine ($"Check IsEmpty using TStack: {intTStack.IsEmpty}"); // False
         intTStack.Clear ();
         Console.WriteLine ($"Check IsEmpty using TStack: {intTStack.IsEmpty}"); //True
         Console.WriteLine ("\n-------------------------------------------------\n");
         var intStack = new Stack<int> ();
         intStack.Push (1);
         intStack.Push (2);
         intStack.Push (3);
         Console.WriteLine ($"First Pop using TStack: {intStack.Pop ()}"); // 3
         Console.WriteLine ($"Peek using Tstack: {intStack.Peek ()}"); // 2
         Console.WriteLine ($"Second Pop using TStack: {intStack.Pop ()}"); // 2
         intStack.Clear ();
         Console.WriteLine ($"Count: {intStack.Count}");
      }
   }
}