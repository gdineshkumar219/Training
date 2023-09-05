int n = GetIntegerInput ("Enter the length of an array: ");
int[] numArray = new int[n];
Random random = new ();
for (int i = 0; i < n; i++) numArray[i] = random.Next (10);
Console.WriteLine ("\nArray before swapping: ");
PrintArray (numArray);
Console.WriteLine ($"\n\nEnter the index to be swapped (between 0 to {n - 1}). ");
int pos1 = GetIndexInput ("Enter the value of index1: ");
int pos2 = GetIndexInput ("Enter the value of index2: ");
ToSwap (numArray, pos1, pos2);
Console.WriteLine ("\nArray after swapping: ");
PrintArray (numArray);
void PrintArray (int[] arr) => Console.Write (string.Join ($"\t", arr));

int GetIntegerInput (string prompt) {
   while (true) {
      Console.Write ($"{prompt}");
      if (int.TryParse (Console.ReadLine (), out int value) && value >= 0) return value;
      else Console.WriteLine ("Invalid input. Please enter an integer.");
   }
}

int GetIndexInput (string prompt) {
   while (true) {
      Console.Write ($"{prompt}");
      if (int.TryParse (Console.ReadLine (), out int value) && value < n) return value;
      else Console.WriteLine ($"Invalid input. Please enter an index value between 0 and {n - 1}.");
   }
}

void ToSwap (int[] arr, int a, int b) => (arr[a], arr[b]) = (arr[b], arr[a]);