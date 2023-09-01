int n = GetIntegerInput ("Enter the number of integers: ");
int[] numbers = new int[n];
Random random = new ();
for (int i = 0; i < n; i++) numbers[i] = random.Next (10);
Console.WriteLine ("\nArray before swapping: ");
foreach (var c in numbers) Console.Write ($"{c}\t");
Console.WriteLine ($"\n\nEnter the index to be swapped (between 0 to {n - 1}). ");
int pos1 = GetIndexInput ("Enter the value of index1: ");
int pos2 = GetIndexInput ("Enter the value of index2: ");
ToSwap (numbers, pos1, pos2);
Console.WriteLine ("\nArray after swapping: ");
foreach (var ch in numbers) Console.Write ($"{ch}\t");

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