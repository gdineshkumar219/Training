/// <summary>Solves the N-Queens problem for ordinary and super queens and provides options to display unique or all solutions</summary>
internal class Program {

   /// <summary>Entry point of the program</summary>
   static void Main () {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.WriteLine ("N-QUEENS PROBLEM");
      int cQueens = GetInput ();
      int[] chessBoard = new int[cQueens];
      List<int[]> slns = new ();
      var queenType = GetQueenType ();
      var slnType = GetSolutionType ();
      bool found = NQueenSolver (chessBoard, slns, queenType, 0);
      if (!found) {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ($"{cQueens} queens cannot be placed. They attack each other.");
         Console.ResetColor ();
      }
      if (slnType == 'u') UniqueSolution (slns);
      for (int i = 0; i < slns.Count; i++) DisplayBoard (slns[i], i + 1);
   }

   /// <summary>Gets the number of queens to be placed from user input</summary>
   /// <returns>The number of queens to be placed</returns>
   static int GetInput () {
      while (true) {
         Console.Write ("\nEnter the number of queens to be placed: ");
         if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
         Console.WriteLine ("Invalid input. Please enter a valid input");
      }
   }

   /// <summary>Gets the type of queen (ordinary or super) from the user</summary>
   /// <returns>The selected queen type ('o' for ordinary queen and 's' for super queen)</returns>
   static char GetQueenType () {
      Console.WriteLine ("Select your queen to be (o)rdinary queen or (s)uper queen?");
      while (true) {
         var key = Console.ReadKey (true).KeyChar;
         if (key == 'o' || key == 's') return key;
         Console.WriteLine ("Invalid input. Please select (o)rdinary or (s)uper queen.");
      }
   }

   /// <summary>Get the type of solution (unique or all) from the user </summary>
   /// <returns>The selected solution type ('u' for unique and 'a' for all)</returns>
   static char GetSolutionType () {
      Console.WriteLine ("You want to see (u)nique solutions or (a)ll solutions?");
      while (true) {
         var key = Console.ReadKey (true).KeyChar;
         if (key == 'u' || key == 'a') return key;
         Console.WriteLine ("Invalid input. Please select (u)nique or (a)ll solutions.");
      }
   }

   /// <summary>Checks if placing a queen at a given position is safe</summary>
   /// <param name="chessBoard">The current state of the chess board</param>
   /// <param name="row">The current row to place the queen.</param>
   /// <param name="col">The column to place the queen</param>
   /// <param name="queenType">The type of queen ('o' for ordinary, 's' for super).</param>
   /// <returns>True if it is safe to place the queen, false otherwise</returns>
   static bool IsQueenSafe (int[] chessBoard, int row, int col, char queenType) {
      for (int prevRow = 0; prevRow < row; prevRow++) {
         int prevCol = chessBoard[prevRow];
         if (prevCol == col || Math.Abs (prevCol - col) == Math.Abs (prevRow - row)) return false;
         if (queenType == 's') {
            int dx = Math.Abs (prevRow - row);
            int dy = Math.Abs (prevCol - col);
            if (dx == 1 && dy == 2 || dx == 2 && dy == 1) return false;
         }
      }
      return true;
   }

   /// <summary>Solves the N-Queens problem using backtracking</summary>
   /// <param name="chessBoard">The current state of the chess board.</param>
   /// <param name="slns">A list to store solutions.</param>
   /// <param name="queenType">The type of queen ('o' for ordinary, 's' for super).</param>
   /// <param name="rows">The current row to place queens.</param>
   /// <returns>True if a solution is found, false otherwise.</returns>
   static bool NQueenSolver (int[] chessBoard, List<int[]> slns, char queenType, int rows) {
      if (rows == chessBoard.Length) {
         int[] solution = new int[chessBoard.Length];
         Array.Copy (chessBoard, solution, chessBoard.Length);
         slns.Add (solution);
         return true;
      }
      bool found = false;
      for (int i = 0; i < chessBoard.Length; i++) {
         if (IsQueenSafe (chessBoard, rows, i, queenType)) {
            chessBoard[rows] = i;
            found = NQueenSolver (chessBoard, slns, queenType, rows + 1) || found;
         }
      }
      return found;
   }

   /// <summary>Removes duplicate solutions from the list of solutions</summary>
   /// <param name="solutions">The list of solutions.</param>
   static void UniqueSolution (List<int[]> solutions) {
      for (int i = 0; i < solutions.Count - 1; i++)
         for (int j = i + 1; j < solutions.Count; j++)
            if (AreSolutionsIdentical (solutions[i], solutions[j])) solutions.RemoveAt (j--);
   }

   /// <summary>Checks if two solutions are identical, considering rotations and mirrors</summary>
   /// <param name="solution1">The first solution</param>
   /// <param name="solution2">The second solution</param>
   /// <returns>True if the solutions are identical, false otherwise</returns>
   static bool AreSolutionsIdentical (int[] solution1, int[] solution2) {
      for (int rotation = 0; rotation < 4; rotation++) {
         solution1 = RotateSolution (solution1);
         if (solution2.SequenceEqual (solution1)) return true;
         if (solution2.SequenceEqual (MirrorSolution (solution1))) return true;
      }
      return false;
   }

   /// <summary>Rotates a solution by 90 degrees</summary>
   /// <param name="solution">The solution to rotate</param>
   /// <returns>The rotated solution.</returns>
   static int[] RotateSolution (int[] solution) {
      int n = solution.Length;
      int[] rotatedSolution = new int[n];
      for (int i = 0; i < n; i++) rotatedSolution[solution[i]] = n - i - 1;
      return rotatedSolution;
   }

   /// <summary>Mirrors a solution</summary>
   /// <param name="solution">The solution to mirror.</param>
   static int[] MirrorSolution (int[] solution) => solution.Reverse ().ToArray ();

   /// <summary>Displays a chessboard with queen placements for a solution</summary>
   /// <param name="solutionIndex">The solution number.</param>
   static void DisplayBoard (int[] solution, int solutionIndex) {
      int cQueens = solution.Length;
      Console.WriteLine ($"Solution: {solutionIndex}");
      for (int i = 0; i < cQueens; i++) {
         for (int j = 0; j < cQueens; j++) {
            Console.BackgroundColor = ((i + j) % 2 == 0) ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = ((i + j) % 2 == 0) ? ConsoleColor.Black : ConsoleColor.White;
            Console.Write (j == solution[i] ? "♕ " : "  ");
            Console.ResetColor ();
         }
         Console.WriteLine ();
      }
   }
}