using System;
using System.Collections.Generic;

/// <summary>
/// Solves the N-Queens problem for ordinary and super queens and provides options to display unique or all solutions.
/// </summary>
class Program {
   static void Main (string[] args) {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.WriteLine ("N-QUEENS PROBLEM");
      int noOfQueens = GetIntegerInput ();
      int[] chessBoard = new int[noOfQueens + 1];
      List<int[]> allSolutions = new ();
      int soln = 0;
      Console.WriteLine ("Select your queen to be (o)rdinary queeen or (s)uper queen?");
      var queenKey = Console.ReadKey (true).KeyChar;
      Console.WriteLine ("You want to see (u)nique solutions or (a)ll solutions.");
      var key = Console.ReadKey (true).KeyChar;
      bool solutionFound = false;
      NQueenSolver (1);
      if (!solutionFound) Console.WriteLine ($"{noOfQueens} queens cannot be placed. They attack each other.");

      /// <summary>Gets an integer input from the user.</summary>
      /// <returns>The integer input from the user.</returns>
      int GetIntegerInput () {
         while (true) {
            Console.Write ("\nEnter the number of queens to be placed: ");
            if (int.TryParse (Console.ReadLine (), out int value) && value > 0) return value;
            else Console.WriteLine ("Invalid input.Please enter a valid input");
         }
      }

      /// <summary>Checks if placing a queen at position (rows, col) on the chessboard is safe.</summary>
      /// <param name="rows">The row to place the queen.</param>
      /// <param name="col">The column to place the queen.</param>
      /// <returns>True if it's safe to place the queen, otherwise false.</returns>
      bool IsQueenSafe (int rows, int col) {
         for (int j = 1; j < rows; j++) if (chessBoard[j] == col || Math.Abs (chessBoard[j] - col) == Math.Abs (j - rows)) return false;
         if (queenKey == 's') {
            List<(int, int)> superQueenMoves = new () { (-2, -1), (-2, 1), (2, -1), (2, 1), (-1, -2), (-1, 2), (1, 2), (1, -2) };
            int dx = superQueenMoves[0].Item1;
            int dy = superQueenMoves[0].Item2;
            foreach (var move in superQueenMoves) {
               int newX = col + move.Item1;
               int newY = rows + move.Item2;
               if (newX >= 1 && newX <= noOfQueens && newY >= 1 && newY <= rows - 1 && chessBoard[newY] == newX) return false;
            }
         }
         return true;
      }

      /// <summary>Solves the N-Queens problem using backtracking.</summary>
      /// <param name="rows">The current row to place the queen.</param>
      void NQueenSolver (int rows) {
         for (int i = 1; i <= noOfQueens; i++) {
            if (IsQueenSafe (rows, i)) {
               chessBoard[rows] = i;
               if (rows == noOfQueens) {
                  PrintSolution ();
                  solutionFound = true;
               } else NQueenSolver (rows + 1);
            }

         }
      }

      /// <summary>Prints the solution to the N-Queens problem based on user-selected options.</summary>
      void PrintSolution () {
         if (key == 'a') DisplayBoard ();
         else {
            bool isIdentical = false;
            int[] solution = new int[noOfQueens + 1];
            for (int i = 1; i <= noOfQueens; i++) solution[i] = chessBoard[i];
            foreach (var existingSoln in allSolutions) {
               if (AreSolutionsIdentical (solution, existingSoln)) {
                  isIdentical = true;
                  break;
               }
            }
            if (!isIdentical) DisplayBoard ();
         }
      }

      /// <summary>Checks if two solutions to the N-Queens problem are identical.</summary>
      /// <param name="solution1">The first solution.</param>
      /// <param name="solution2">The second solution.</param>
      /// <returns>True if the solutions are identical, otherwise false.</returns>
      bool AreSolutionsIdentical (int[] solution1, int[] solution2) {
         for (int rotation = 0; rotation < 4; rotation++) {
            for (int mirror = 0; mirror < 2; mirror++) {
               if (AreSolutionsEqual (solution1, solution2)) return true;
               RotateSolution (solution1);
               if (mirror == 1) MirrorSolution (solution1);// Apply a mirror to solution1       
            }
            RotateSolution (solution2);
         }
         return false;
      }

      /// <summary>Rotates a solution to the N-Queens problem by 90 degrees.</summary>
      /// <param name="solution">The solution to rotate.</param>
      void RotateSolution (int[] solution) {
         int n = solution.Length - 1;
         int[] rotatedSolution = new int[n + 1];
         for (int i = 1; i <= n; i++) rotatedSolution[solution[i]] = n - i + 1;
         for (int i = 1; i <= n; i++) solution[i] = rotatedSolution[i];
      }

      /// <summary>Mirrors a solution to the N-Queens problem horizontally.</summary>
      /// <param name="solution">The solution to mirror.</param>
      void MirrorSolution (int[] solution) {
         int n = solution.Length - 1;
         for (int i = 1; i <= n / 2; i++) (solution[i], solution[n - i + 1]) = (solution[n - i + 1], solution[i]);
      }

      /// <summary>Checks if two solutions to the N-Queens problem are equal.</summary>
      /// <param name="solution1">The first solution.</param>
      /// <param name="solution2">The second solution.</param>
      /// <returns>True if the solutions are equal, otherwise false.</returns>
      bool AreSolutionsEqual (int[] solution1, int[] solution2) {
         int n = solution1.Length - 1;
         for (int i = 1; i <= n; i++) if (solution1[i] != solution2[i]) return false;
         return true;
      }

      /// <summary>Displays the chessboard for a valid N-Queens solution.</summary>
      void DisplayBoard () {
         int[] solution = new int[noOfQueens + 1];
         for (int i = 1; i <= noOfQueens; i++) solution[i] = chessBoard[i];
         allSolutions.Add (solution);
         Console.WriteLine ($"Solution: {++soln}");
         for (int i = 1; i <= noOfQueens; i++) Console.Write ("┌───");
         Console.WriteLine ("┐");
         for (int i = 1; i <= noOfQueens; i++) {
            for (int j = 1; j <= noOfQueens; j++) {
               if (j == solution[i]) Console.Write ("│ ♕ ");
               else Console.Write ("│   ");
            }
            Console.WriteLine ("│");
            if (i < noOfQueens) {
               for (int j = 1; j <= noOfQueens; j++) Console.Write ("├───");
               Console.WriteLine ("┤");
            }
         }
         for (int i = 1; i <= noOfQueens; i++) Console.Write ("└───");
         Console.WriteLine ("┘");
      }
   }
}