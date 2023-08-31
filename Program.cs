int n = GetIntegerInput ("Enter the number of integers u want: ");
int[] numbers = new int[n];
for (int i = 0; i < n; i++) numbers[i] = GetIntegerInput ($"Enter number {i + 1}: ");
Console.WriteLine ("\nArray before swapping: ");
foreach (var c in numbers) Console.Write ($"{c}\t");
Console.WriteLine ($"\nEnter the index to be swapped.\nEnter the index value between 0 to {n - 1}. ");
int pos1 = GetIndexInput ("Enter the value of index1: ");
int pos2 = GetIndexInput ("Enter the value of index2: ");
Console.WriteLine ("\nArray after swapping: ");
foreach (var ch in ToSwap (numbers, pos1, pos2)) Console.Write ($"{ch}\t");

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

static int[] ToSwap (int[] arr, int a, int b) {
   (arr[a], arr[b]) = (arr[b], arr[a]);
   return arr;
}