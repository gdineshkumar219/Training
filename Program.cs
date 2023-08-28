string password = GetPassword ();

string GetPassword () {
   string password;
   while (true) {
      Console.Write ("Enter the password: ");
      password = Console.ReadLine ();
      Console.WriteLine ($"Hey you your,{passwordWarnings (password)}");
      if (IsValidPassword (password)) {
         Console.WriteLine (" =>Your Password is strong.");
         break;
      } else Console.WriteLine ("=>Your password is Weak. Please try again.");
   }
   return password;
}

string passwordWarnings (string password) {
   string warnings = "";
   warnings += (password.Length < 6) ? "\nPassword must have 6 characters or more.":"";
   warnings += (password.Any (char.IsDigit) is false) ? "\nPassword must have atleast 1 number." : "";
   warnings += (password.Any (char.IsUpper) is false) ? "\nPassword must have atleast one Upper case letter." : "";
   warnings += (password.Any (char.IsLower) is false) ? "\nPassword must have atleast one lower case letter." : "";
   warnings += (password.Any (ch => !Char.IsLetterOrDigit (ch)) is false) ? "\n!Password must have atleast 1 symbol." : "";
   return warnings;
}

bool IsValidPassword (string password) => password.Length >= 6 && password.Any (char.IsUpper) && password.Any (char.IsLower) && password.Any (ch => !Char.IsLetterOrDigit (ch)) && password.Any (char.IsDigit);