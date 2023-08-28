Console.Write ("Enter the string:");
string inputString = Console.ReadLine ();
Console.WriteLine ($"Is {inputString} ISOGRAM? {IsIsogram (inputString)}");

static bool IsIsogram (string str) => str.Distinct ().Count () == str.Length;
