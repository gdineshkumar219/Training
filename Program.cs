for (int i = 1; i <= 10; i++) {
   Console.WriteLine ($"Multiplication Table for {i}:");
   for (int j = 1; j <= 10; j++) {
      int result = i * j;
      Console.WriteLine ($"{i,2} * {j,2} = {result}");
   }
   Console.WriteLine ();
}