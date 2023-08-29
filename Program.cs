Console.Write ("Enter the string: ");
string input = Console.ReadLine ();
string[] permutations = GetPermutations (input);
Console.WriteLine ($"The combination of {input}");
for (int i = 0; i < permutations.Length; i++) Console.WriteLine ($"{i + 1,3}.{permutations[i]}");

static string[] GetPermutations (string input) => (input.Length == 1) ? new string[] { input } : input.SelectMany ((c, i) => GetPermutations (input.Remove (i, 1)).Select (perm => c + perm)).ToArray ();