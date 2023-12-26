// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Tokenizer.cs
// Tokenizer class is responsible for parsing input mathematical expressions and generating a list
// of tokens that can be used for evaluation by the associated Evaluator. 
// ------------------------------------------------------------------------------------------------
namespace Training;
#region Class Tokenizer ---------------------------------------------------------------------------
class Tokenizer {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes a new instance of the Tokenizer class</summary>
   public Tokenizer (Evaluator eval, string text) {
      mText = text; mN = 0; mEval = eval;
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Retrieves the next token from the input text</summary>
   /// <param name="tokens">List of tokens being constructed</param>
   /// <returns>Next token in the input text</returns>
   public Token Next (List<Token> tokens) {
      while (mN < mText.Length) {
         // Read the next character from the input text
         char ch = mText[mN++];
         switch (ch) {
            case ' ': continue;  // Skip whitespace
            case '+' or '-':
               // Check if the current character is a unary operator or an arithmetic operator
               return (tokens.Count == 0 || tokens[^1] is TOperator or TPunctuation { Punct: '(' })
                   ? new TOpUnary (mEval, ch) : new TOpArithmetic (mEval, ch);
            case '/' or '*' or '^' or '=':
               // Create an arithmetic operator token
               return new TOpArithmetic (mEval, ch);
            case >= '0' and <= '9':
               // Create a literal token
               return GetNumber ();
            case >= 'a' and <= 'z':
               // Create an identifier token
               return GetIdentifier ();
            case '(' or ')':
               // Adjust evaluator priority based on parentheses
               mEval.BasePriority += ch == '(' ? 10 : -10;
               // Create a punctuation token
               return new TPunctuation (ch);
            default:
               // Create an error token for unknown symbols
               return new TError ($"Unknown symbol: {ch}");
         }
      }
      // Return end of input token
      return new TEnd ();
   }
   #endregion

   #region Implementation -------------------------------------------
   /// <summary>
   /// Gets the identifier token from the current position in the input text
   /// </summary>
   /// <returns>Identifier token</returns>

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         if (ch is >= 'a' and <= 'z') continue;
         mN--; break;
      }
      string sub = mText[start..mN];
      if (mFuncs.Contains (sub)) return new TOpFunction (mEval, sub);
      else return new TVariable (mEval, sub);
   }
   readonly string[] mFuncs = { "sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan" };

   /// <summary>Gets the literal token from the current position in the input text</summary>
   /// <returns>The literal token</returns>
   Token GetNumber () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (ch is (>= '0' and <= '9') or '.') continue;
         mN--; break;
      }
      // Now, mN points to the first character of mText that is not part of the number
      string sub = mText[start..mN];
      if (double.TryParse (sub, out double f)) return new TLiteral (f);
      return new TError ($"Invalid number: {sub}");
   }
   #endregion

   #region Private Members ------------------------------------------
   readonly Evaluator mEval;  // The evaluator that owns this
   readonly string mText;     // The input text we're parsing through
   int mN;                    // Position within the text
   #endregion
}
#endregion