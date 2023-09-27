using System.Linq;
using static System.Console;

OutputEncoding = System.Text.Encoding.UTF8;
string blankRows = "├" + ToRepeatChar ("───┼", 7) + "───┤";
string middleLines = ToRepeatChar ("│   ", 8) + "│";
WriteLine ("┌" + ToRepeatChar ("───┬", 7) + "───┐");
WriteLine ("│ ♜ │ ♞ │ ♝ │ ♛ │ ♚ │ ♝ │ ♞ │ ♜ │");
WriteLine (blankRows + "\n" + ToRepeatChar ("│ ♟ ", 8) + "│");
Write (ToRepeatChar ($"{blankRows} \n{middleLines}\n", 4));
WriteLine (blankRows + "\n" + ToRepeatChar ("│ ♙ ", 8) + "│");
WriteLine (blankRows + "\n│ ♖ │ ♘ │ ♗ │ ♕ │ ♔ │ ♗ │ ♘ │ ♖ │");
WriteLine ("└" + ToRepeatChar ("───┴", 7) + "───┘");

static string ToRepeatChar (string str, int n) => string.Concat (Enumerable.Repeat (str, n));