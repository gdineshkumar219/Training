string password = GetPassword ();

string GetPassword () {
   string password;
   while (true) {
      Console.Write ("Enter the password: ");
      password = Console.ReadLine ();
      Console.WriteLine ($"Hey you your,{PasswordWarnings (password)}");
      if (string.IsNullOrEmpty (PasswordWarnings (password))) {
         Console.WriteLine ("Password is strong.");
         break;
      } else Console.WriteLine ("Password is Weak. Please try again.");
   }
   return password;
}

string PasswordWarnings (string password) {
   string warnings = "";
   warnings += (password.Length < 6) ? "\nPassword must have 6 characters or more." : "";
   warnings += (password.Any (char.IsDigit) is false) ? "\nPassword must have atleast 1 number." : "";
   warnings += (password.Any (char.IsUpper) is false) ? "\nPassword must have atleast one Upper case letter." : "";
   warnings += (password.Any (char.IsLower) is false) ? "\nPassword must have atleast one lower case letter." : "";
   warnings += (password.Any (ch => !Char.IsLetterOrDigit (ch)) is false) ? "\nPassword must have atleast 1 symbol." : "";
   return warnings;
}