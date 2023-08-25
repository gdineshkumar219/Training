string password = GetPassword ();

string GetPassword () {
   string password;
   while (true) {
      Console.Write ("Enter the password: ");
      password = Console.ReadLine ();
      Console.WriteLine (passwordWarnings (password));
      if (IsValidPassword (password)) {
         Console.WriteLine (" =>Your Password is strong.");
         break;
      } else Console.WriteLine ("=>Your password is Weak. Please try again.");
   }
   return password;
}

string passwordWarnings (string password) {
   string warnings = "";
   warnings += (password.Length < 6) ? "\n!!!Password must have 6 characters or more." : "\nPassword has more than 5 character.";
   warnings += (password.Any (char.IsDigit) is false) ? "\n!!!Password must have atleast 1 number." : "\nPassword has a number.";
   warnings += (password.Any (char.IsUpper) is false) ? "\n!!!Password must have atleast one Upper case letter." : "\nPassword has an upper case letter.";
   warnings += (password.Any (char.IsLower) is false) ? "\n!!!Password must have atleast one lower case letter." : "\nPassword has a lower case letter.";
   warnings += (password.Any (ch => !Char.IsLetterOrDigit (ch)) is false) ? "\n!Password must have atleast 1 symbol." : "\nPassword has a symbol.";
   return warnings;
}

bool IsValidPassword (string password) => password.Length >= 6 && password.Any (char.IsUpper) && password.Any (char.IsLower) && password.Any (ch => !Char.IsLetterOrDigit (ch)) && password.Any (char.IsDigit);