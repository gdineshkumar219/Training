namespace Training {

   /// <summary>Represents a generic list with dynamic resizing capability</summary>
   /// <typeparam name="T">The type of elements in the list.</typeparam>
   public class MyList<T> {
      private T[] mArr;
      private int mCount;
      private int mCapacity = 4;

      /// <summary>Gets the number of elements contained in the list</summary>
      public int Count => mCount;

      /// <summary>Gets the current capacity of the list</summary>
      public int Capacity => mCapacity;

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

      /// <summary>Adds an element to the end of the list</summary>
      /// <param name="item">The element to add to the list</param>
      public void Add (T item) {
         ModifyCapacity ();
         mArr[mCount++] = item;
      }

      /// <summary>Removes the first occurrence of a specific element from the list</summary>
      /// <param name="item">The element to remove</param>
      /// <returns>True if the element was successfully removed</returns>
      public bool Remove (T item) {
         int index = Array.IndexOf (mArr, item);
         if (!mArr.Any (c => c.Equals (item))) return false;
         if (index < 0) throw new InvalidOperationException ("Item not found in the list");
         RemoveAt (index);
         return true;
      }

      /// <summary>Clears the entire list</summary>
      public void Clear () {
         Array.Clear (mArr, 0, mCapacity);
         mCount = 0;
         ModifyCapacity ();
      }

      /// <summary>Inserts elements at the specified index</summary>
      /// <param name="index"></param>
      /// <param name="item"></param>
      /// <exception cref="IndexOutOfRangeException"></exception>
      public void Insert (int index, T item) {
         try {
            ModifyCapacity ();
            for (int i = mCount; i > index; i--) if (mCount != index) mArr[i] = mArr[i - 1];
            mArr[index] = item;
            mCount++;
         } catch (IndexOutOfRangeException) { throw new IndexOutOfRangeException (); }
      }

      /// <summary>Removes element at the specified index</summary>
      /// <param name="index"></param>
      /// <exception cref="IndexOutOfRangeException"></exception>
      public void RemoveAt (int index) {
         try {
            for (int i = index; i < mCount - 1; i++) mArr[i] = mArr[i + 1];
            mCount--;
            mArr[mCount] = default;
            ModifyCapacity ();
         } catch (IndexOutOfRangeException) { throw new IndexOutOfRangeException (); }
      }

      /// <summary>Modifies the capacity of the list based on its current count</summary>
      void ModifyCapacity () {
         if (mCount == mCapacity) mCapacity *= 2;
         else if (mCount <= mCapacity / 2) mCapacity = mCount < 5 ? 4 : mCapacity / 2;
         Array.Resize (ref mArr, mCapacity);
      }

      /// <summary>Initializes a new instance of the <see cref="MyList{T}"/> class</summary>
      public MyList () => mArr = new T[mCapacity];
   }

   /// <summary>Contains the Main method to demonstrate the MyList class</summary>
   internal class Program {

      /// <summary>Entry Point of the program</summary>
      static void Main () {
         MyList<char> myList = new ();
         myList.Insert (0,'a');
         myList.Add ('a');
         myList.Add ('b');
         myList.Insert (3, 'c');
         myList.Insert (4, 'd');
         myList.Insert (5, 'e');
         myList.Insert (6, 'f');
         myList.Insert (7, 'e');
         myList.Insert (8, 'h');
         myList.Add ('k');
         myList.Remove ('a');
         myList.Remove ('a');
         myList.Remove ('b');
         myList.RemoveAt (0);
         Console.Write ("Elements of MyList(myList):");
         for (int i = 0; i < myList.Count; i++) Console.Write (myList[i]);
         Console.WriteLine ($"\nUsing MyList Capacity:{myList.Capacity}");
         Console.WriteLine ($"Using MyList Count:{myList.Count}");
         Console.WriteLine ($"\n---------------------------------------");
         List<char> l = new ();
         l.Add ('a');
         l.Add ('a');
         l.Add ('b');
         l.Insert (3, 'c');
         l.Insert (4, 'd');
         l.Insert (5, 'e');
         l.Insert (6, 'f');
         l.Insert (7, 'e');
         l.Insert (8, 'h');
         l.Add ('k');
         l.Remove ('a');
         l.Remove ('a');
         l.Remove ('b');
         l.RemoveAt (0);
         Console.Write ("Elements of List(l)");
         for (int i = 0; i < l.Count; i++) Console.Write (l[i]);
         Console.WriteLine ($"\nUsing List Capacity:{l.Capacity}");
         Console.WriteLine ($"Using List Count:{l.Count}");
      }
   }
}