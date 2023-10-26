// --------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2023.
// Copyright (c) Metamation India.                                              
// ------------------------------------------------------------------------
// Program.cs
// Implement a custom MyList<T> class using arrays as the underlying data structure.
// The MyList<T> should start with an initial capacity of 4 and double its capacity when needed.
// Throw exceptions when necessary. 
// --------------------------------------------------------------------------------------------

using static System.Net.Mime.MediaTypeNames;

namespace Training {

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
         if (index > mCount) throw new IndexOutOfRangeException ();
         ModifyCapacity ();
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
         mArr[mCount--] = default;
         ModifyCapacity ();
      }

      /// <summary>Modifies the capacity of the list based on its current count</summary>
      void ModifyCapacity () {
         if (mCount == mCapacity) mCapacity *= 2;
         else if (mCount <= mCapacity / 2) mCapacity = mCount < 5 ? 4 : mCapacity / 2;
         Array.Resize (ref mArr, mCapacity);
      }
      #endregion

      #region Private Fields ------------------------------------------------
      T[] mArr;
      int mCount = 0, mCapacity = 4;
      #endregion
   }
   #endregion
   /// <summary>Contains the Main method to demonstrate the MyList class</summary>
   internal class Program {
      #region Methods -----------------------------------------------
      /// <summary>Entry Point of the program</summary>
      static void Main () {
         TestMyList ();
         TestList ();
      }

      /// <summary>Tests the custom MyList class</summary>
      static void TestMyList () {
         try {
            Console.WriteLine ("Testing MyList<T>");
            MyList<char> myList = new ();
            myList.Insert (0, 'a');
            myList.Add ('a');
            myList.Add ('b');
            myList.Insert (3, 'c');
            myList.Insert (4, 'd');
            myList.Insert (5, 'e');
            myList.Insert (6, 'f');
            myList.Insert (7, 'e');
            myList.Insert (8, 'h');
            myList.Insert (4, 'z');
            myList.Add ('k');
            Console.WriteLine ("Testing MyList methods:");
            Console.WriteLine ("Indexer Test: myList[3] = " + myList[3]);
            Console.WriteLine ("Remove Test: myList.Remove('a') = " + myList.Remove ('a'));
            myList.Clear ();
            Console.WriteLine ("Clear Test: myList.Count after Clear = " + myList.Count);
            myList.Insert (0, 'A');
            Console.WriteLine ("Insert Test: myList[0] after Insert = " + myList[0]);
            myList.RemoveAt (0);
            Console.WriteLine ("RemoveAt Test: myList.Count after RemoveAt = " + myList.Count);
            for (int i = 0; i < myList.Count; i++) Console.Write (myList[i]);
            Console.WriteLine ($"\nUsing MyList Capacity: {myList.Capacity}");
            Console.WriteLine ($"Using MyList Count: {myList.Count}");
         } catch (Exception ex) {
            Console.WriteLine ($"Exception in MyList: {ex.Message}");
         }
         Console.WriteLine ("---------------------------------------");
      }

      /// <summary>Tests the custom built-in List class</summary>
      static void TestList () {
         try {
            Console.WriteLine ("Testing built-in List<T>");
            List<char> list = new List<char> ();
            list.Insert (0, 'a');
            list.Add ('a');
            list.Add ('b');
            list.Insert (3, 'c');
            list.Insert (4, 'd');
            list.Insert (5, 'e');
            list.Insert (6, 'f');
            list.Insert (7, 'e');
            list.Insert (8, 'h');
            list.Insert (4, 'z'); ;
            list.Add ('k');
            Console.WriteLine ("Testing List methods:");
            Console.WriteLine ("Indexer Test: list[3] = " + list[3]);
            Console.WriteLine ("Remove Test: list.Remove('a') = " + list.Remove ('a'));
            list.Clear ();
            Console.WriteLine ("Clear Test: list.Count after Clear = " + list.Count);
            list.Insert (0, 'A');
            Console.WriteLine ("Insert Test: list[0] after Insert = " + list[0]);
            list.RemoveAt (0);
            Console.WriteLine ("RemoveAt Test: list.Count after RemoveAt = " + list.Count);
            Console.Write ("Elements of List(l):");
            for (int i = 0; i < list.Count; i++) Console.Write (list[i]);
            Console.WriteLine ($"\nUsing List Capacity: {list.Capacity}");
            Console.WriteLine ($"Using List Count: {list.Count}");
         } catch (Exception ex) {
            Console.WriteLine ($"Exception in List: {ex.Message}");
         }
      }
   }
   #endregion
}