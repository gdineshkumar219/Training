using ClassLibrary;

namespace Training {
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