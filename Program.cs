namespace Training {
   internal class Program {
     public class MyList<T> {
         public T[] items;
         public int count;
         public int capacity;
         public MyList () {
            capacity = 4;
            items = new T[capacity];
         }
         public int Count => count;
         public int Capacity => capacity;
         public void Add (T item) {
            items[count] = item;
            count++;
         }

         public bool Remove (T item) {
            int index = Array.IndexOf (items, item, 0, count);
            if (index == -1) throw new InvalidOperationException ("Item not found in the list");
            RemoveAt (index);
            return true;

         }
         public void Clear () {
            Array.Clear (items, 0, count);
            count = 0;
         }
         public void Insert (int index, T item) {
            for (int i = count; i > index; i--) {
               items[i] = items[i - 1];
            }
            items[index] = item;
            count++;
         }
         public void RemoveAt (int index) {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException ("Index is out of range");
            for (int i = index; i < count - 1; i++) items[i] = items[i + 1];         
            count--;
            items[count] = default;
         }

         public void UpdateCapacity () {
            if (count == capacity) {
               capacity *= 2;
               Array.Resize (ref items, capacity);
            }
         }
      }
      static void Main () {
         MyList<int> myList = new();
         myList.Add (1);
         myList.Add (2);
         myList.Add (3);
         Console.WriteLine ();
         for (int i = 0; i < myList.count; i++) 
            { Console.WriteLine (myList[i]); }
         }
   }
}