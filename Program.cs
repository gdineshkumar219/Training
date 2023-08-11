//using System.Text;
//int boardSize = 8;
//char[,] board = new char[boardSize, boardSize];
//Console.OutputEncoding = Encoding.UTF8;
//for (int row = 0; row < boardSize; row++) {
//   for (int col = 0; col < boardSize; col++) {
//      bool isWhiteSquare = (row + col) % 2 == 0;
//      char piece = ' ';
//      if (isWhiteSquare) {
//         piece = '\u25A1'; // White square
//      } else {
//         piece = '\u25A1'; // Black square
//      }
//      board[row, col] = piece;
//   }
//}
//// Set up white pieces
//board[0, 0] = '\u265C'; // Rook
//board[0, 1] = '\u265E'; // Knight
//board[0, 2] = '\u265D'; // Bishop
//board[0, 3] = '\u265B'; // Queen
//board[0, 4] = '\u265A'; // King
//board[0, 5] = '\u265D'; // Bishop
//board[0, 6] = '\u265E'; // Knight
//board[0, 7] = '\u265C'; // Rook
//// Set up black pieces
//for (int col = 0; col < boardSize; col++) {
//   board[1, col] = '\u265F'; // Pawn
//   board[6, col] = '\u2659'; // Pawn
//}
//board[7, 0] = '\u2656'; // Rook
//board[7, 1] = '\u2658'; // Knight
//board[7, 2] = '\u2657'; // Bishop
//board[7, 3] = '\u2655'; // Queen
//board[7, 4] = '\u2654'; // King
//board[7, 5] = '\u2657'; // Bishop
//board[7, 6] = '\u2658'; // Knight
//board[7, 7] = '\u2656'; // Rook
//// Display the board
//for (int row = 0; row < boardSize; row++) {
//   for (int col = 0; col < boardSize; col++) {
//      Console.Write (board[row, col]);
//   }
//   Console.WriteLine ();
//}
using System;

namespace Training {
   internal class Program {
      static void Main (string[] args) {
         Console.OutputEncoding = System.Text.Encoding.Unicode;

         for (int i = 1; i <= 8; i++) {
            PrintLine (i);
            Console.WriteLine ();
            for (int j = 1; j <= 8; j++) {
               Console.Write ('\u2502');
               DisplayChessItem (i, j);
            }
            Console.Write ('\u2502' + "\n");
            if (i != 8) Console.Write ('\u251C');
         }
         PrintLine (9);
      }

      static void PrintLine (int row) {
         char start = (row == 1) ? '\u250C' : (row == 9) ? '\u2514' : '\u251C';
         char mid = (row == 1 || row == 9) ? '\u2500' : '\u2500';

         Console.Write (start);
         for (int i = 1; i <= 8; i++) {
            Console.Write (mid + mid);
            Console.Write ((i == 8) ? ((row == 1) ? '\u2510' : '\u2518') : mid);
         }
      }

      static void DisplayChessItem (int i, int j) {
         char chessPiece = ' ';
         if ((i == 1 || i == 8) && (j == 1 || j == 8))
            chessPiece = (i == 1) ? '\u2656' : '\u265C';
         else if ((i == 1 || i == 8) && (j == 2 || j == 7))
            chessPiece = (i == 1) ? '\u2658' : '\u265E';
         else if ((i == 1 || i == 8) && (j == 3 || j == 6))
            chessPiece = (i == 1) ? '\u2657' : '\u265D';
         else if (i == 1 && j == 4)
            chessPiece = '\u2655';
         else if (i == 1 && j == 5)
            chessPiece = '\u2654';
         else if (i == 2)
            chessPiece = '\u2659';
         else if (i == 7)
            chessPiece = '\u265F';

         Console.Write (" " + chessPiece);
      }
   }
}
