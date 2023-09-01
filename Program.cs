Console.Write ("Enter the string: ");
string input = Console.ReadLine ();
string[] permutations = GetPermutations (input);
Console.WriteLine ($"The combination of {input}");
for (int i = 0; i < permutations.Length; i++) Console.WriteLine ($"{i}.{permutations[i]}");

//BaseCase: If the input has only one character ,return an array with that character.
//RecursiveCase:Iterate through each character of input string-->input.SelectMany (c, i)
//Create a new string without the current character-->input.Remove (i, 1)
//Recursively call on remaining characters creates substrings
//Combine the current character with each substring and return the concatenated array-->.Select (perm => c + perm)
static string[] GetPermutations (string input) => (input.Length == 1) ? new string[] { input } : input.SelectMany ((c, i) => GetPermutations (input.Remove (i, 1)).Select (perm => c + perm)).ToArray ();