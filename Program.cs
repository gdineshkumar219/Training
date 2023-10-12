/// <summary>Solves the N-Queens problem for ordinary and super queens and provides options to display unique or all solutions</summary>
internal class Program {
   static int cQueens;
   static char queenType, slnType;
   static List<int[]> slns = new ();
   static int[] chessBoard;

   static void Main () {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.WriteLine ("N-QUEENS PROBLEM");
      GetInput ();
      chessBoard = new int[cQueens];
      GetQueenType ();
      GetSolutionType ();
      NQueenSolver (0);
      if (slns.Count == 0) {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ($"{cQueens} queens cannnot be placed. They attack each other.☠");
         Console.ResetColor ();
      }
      DisplayBoard ();
   }

   /// <summary> Gets the number of queens to be placed from user input</summary>
   static void GetInput () {
      while (true) {
         Console.Write ("\nEnter the number of queens to be placed: ");
         if (int.TryParse (Console.ReadLine (), out int value) && value > 0) {
            cQueens = value;
            break;
         }
         Console.WriteLine ("Invalid input. Please enter a valid input");
      }
   }

   /// <summary> Gets the type of queen (ordinary or super) from the user</summary>
   static void GetQueenType () {
      Console.WriteLine ("Select your queen to be (o)rdinary queen or (s)uper queen?");
      while (true) {
         var key = Console.ReadKey (true).KeyChar;
         if (key == 'o' || key == 's') {
            queenType = key;
            break;
         }
         Console.WriteLine ("Invalid input. Please select (o)rdinary or (s)uper queen.");
      }
   }

   /// <summary> Get the type of solution (unique or all) from the user</summary>
   static void GetSolutionType () {
      Console.WriteLine ("You want to see (u)nique solutions or (a)ll solutions?");
      while (true) {
         var key = Console.ReadKey (true).KeyChar;
         if (key == 'u' || key == 'a') {
            slnType = key;
            break;
         }
         Console.WriteLine ("Invalid input. Please select (u)nique or (a)ll solutions.");
      }
   }

   /// <summary>Checks if placing a queen at a given position is safe</summary>
   /// <param name="row">The current row to place the queen</param>
   /// <param name="col">The column to place the queen</param>
   /// <returns>True if it is safe to place the queen, false otherwise</returns>
   static bool IsQueenSafe (int row, int col) {
      for (int prevRow = 0; prevRow < row; prevRow++) {
         int prevCol = chessBoard[prevRow];
         int dx = Math.Abs (prevRow - row);
         int dy = Math.Abs (prevCol - col);
         if (prevCol == col || dy == dx) return false;
         if (queenType == 's')
            if ((dx == 1 && dy == 2) || (dx == 2 && dy == 1)) return false;
      }
      return true;
   }

   /// <summary>Solves the N-Queens problem using backtracking</summary>
   /// <param name="rows">The current row to place queens</param>
   static void NQueenSolver (int rows) {
      if (rows == cQueens) AddSoln (chessBoard.ToArray ());
      for (int i = 0; i < cQueens; i++) {
         if (IsQueenSafe (rows, i)) {
            chessBoard[rows] = i;
            NQueenSolver (rows + 1);
         }
      }
   }

   /// <summary>Adds a solution to the list of solutions</summary>
   /// <param name="solution">Solution to be added</param>
   static void AddSoln (int[] solution) {
      if (slnType == 'u') {
         for (int rotation = 0; rotation < 4; rotation++) {
            solution = RotateSolution (solution);
            if (IsIdentical (solution) || IsIdentical (MirrorSolution (solution))) return;
         }
      }
      slns.Add (solution);
   }

   /// <summary>Rotates a solution by 90 degrees</summary>
   /// <param name="solution">The solution to be rotated</param>
   /// <returns>The rotated solution</returns>
   static int[] RotateSolution (int[] solution) {
      int[] rotatedSolution = new int[cQueens];
      for (int i = 0; i < cQueens; i++) rotatedSolution[solution[i]] = cQueens - i - 1;
      return rotatedSolution;
   }

   /// <summary>Mirrors a solution</summary>
   /// <param name="solution">The solution to be mirrored</param>
   /// <returns>The mirrored solution</returns>
   static int[] MirrorSolution (int[] solution) => solution.Reverse ().ToArray ();

   /// <summary>Checks if a solution is identical to any of the existing solutions</summary>
   /// <param name="soln">The solution to be checked for similarity</param>
   /// <returns>True if the solution is identical, false otherwise</returns>
   static bool IsIdentical (int[] soln) => slns.Any (s => s.SequenceEqual (soln));

   /// <summary>Displays the solutions on the chessboard</summary
   static void DisplayBoard () {
      int k = 0;
      while (k < slns.Count) {
         var solution = slns[k];
         Console.Clear ();
         Console.WriteLine ($"Solution: {k + 1}☟ of {slns.Count}");
         for (int i = 0; i < cQueens; i++) {
            for (int j = 0; j < cQueens; j++) {
               var black = ConsoleColor.Black;
               var white = ConsoleColor.White;
               var bg = ((i + j) % 2 == 0) ? white : black;
               var fg = (bg == ConsoleColor.White) ? black : white;
               Console.BackgroundColor = bg;
               Console.ForegroundColor = fg;
               Console.Write (j == solution[i] ? "♛ " : "  ");
               Console.ResetColor ();
            }
            Console.WriteLine ();
         }
         Console.WriteLine ("Press left arrow for the previous solution, right arrow for the next solution or 'Q' to quit.");
         var key = Console.ReadKey (true).Key;
         if (key == ConsoleKey.Q) break;
         else if (key == ConsoleKey.RightArrow) k = Math.Min (k + 1, slns.Count - 1);
         else if (key == ConsoleKey.LeftArrow) k = Math.Max (k - 1, 0);
      }
   }
}