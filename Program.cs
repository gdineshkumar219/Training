string password = GetPassword ();

string GetPassword () {
   string password;
   while (true) {
      Console.Write ("Enter the password: ");
      password = Console.ReadLine ();
      Console.WriteLine (passwordWarnings (password));
      if (IsValidPassword (password)) {
         Console.WriteLine ("Password is strong.");
         break;
      } else Console.WriteLine ("Invalid password. Please try again.");
   }
   return password;
}

string passwordWarnings (string password) {
   string warnings = "";
   warnings += (password.Length < 6) ? "\n!Password must have 6 characters or more." : "\n!Password has more than 5 character.";
   warnings += (password.Any (char.IsDigit) is false) ? "\n!Password must have atleast 1 number." : "\n!Password has a number.";
   warnings += (password.Any (char.IsUpper) is false) ? "\n!Password must have atleast one Upper case letter." : "\n!Password has an upper case letter.";
   warnings += (password.Any (char.IsLower) is false) ? "\n!Password must have atleast one lower case letter." : "\n!Password has a lower case letter.";
   warnings += (password.Any (ch => !Char.IsLetterOrDigit (ch)) is false) ? "\n!Password must have atleast 1 symbol." : "\n!Password has a symbol.";
   return warnings;
}

bool IsValidPassword (string password) => password.Length >= 6 && password.Any (char.IsUpper) && password.Any (char.IsLower) && password.Any (ch => !Char.IsLetterOrDigit (ch)) && password.Any (char.IsDigit);