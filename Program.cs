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
//         piece = '\u25A0'; // Black square
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
using System.Text;

int boardSize = 8;
char[,] board = new char[boardSize, boardSize];
Console.OutputEncoding = Encoding.UTF8;
for (int row = 0; row < boardSize; row++) {
   for (int col = 0; col < boardSize; col++) {
      bool isWhiteSquare = (row + col) % 2 == 0;
      char piece = ' ';

      if (isWhiteSquare) {
         piece = '\u25A1'; // White square
      } else {
         piece = '\u25A0'; // Black square
      }

      board[row, col] = piece;
   }
}

// Set up white pieces
board[0, 0] = '\u265C'; // Rook
board[0, 1] = '\u265E'; // Knight
board[0, 2] = '\u265D'; // Bishop
board[0, 3] = '\u265B'; // Queen
board[0, 4] = '\u265A'; // King
board[0, 5] = '\u265D'; // Bishop
board[0, 6] = '\u265E'; // Knight
board[0, 7] = '\u265C'; // Rook
// Set up black pieces
for (int col = 0; col < boardSize; col++) {
   board[1, col] = '\u265F'; // Pawn
   board[6, col] = '\u2659'; // Pawn
}
board[7, 0] = '\u2656'; // Rook
board[7, 1] = '\u2658'; // Knight
board[7, 2] = '\u2657'; // Bishop
board[7, 3] = '\u2655'; // Queen
board[7, 4] = '\u2654'; // King
board[7, 5] = '\u2657'; // Bishop
board[7, 6] = '\u2658'; // Knight
board[7, 7] = '\u2656'; // Rook

// Display the board
for (int row = 0; row < boardSize; row++) {
   for (int col = 0; col < boardSize; col++) {
      Console.Write (board[row, col]);
   }
   Console.WriteLine ();
}
