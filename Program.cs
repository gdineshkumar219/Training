using System.Text.RegularExpressions;

Console.Write("Enter the password: ");
string password = Console.ReadLine ();
var hasNumber = new Regex (@"[0-9]+");
var hasUpperChar = new Regex (@"[A-Z]+");
var hasMinMaxChars = new Regex (@".{6,}");
var hasLowerChar = new Regex (@"[a-z]+");
var hasSymbols = new Regex (@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
var isValidated = hasNumber.IsMatch (password) && hasUpperChar.IsMatch (password) && hasMinMaxChars.IsMatch (password) && hasLowerChar.IsMatch(password)&& hasSymbols.IsMatch(password);
Console.WriteLine (isValidated);

