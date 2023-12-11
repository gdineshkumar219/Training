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
      int openParenthesesCount = 0, closeParenthesesCount=0;
      while (mN < mText.Length) {
         // Read the next character from the input text
         char ch = mText[mN++];
         switch (ch) {
            case ' ': continue;  // Skip whitespace
            case '+' or '-':
               // Check if the current character is a unary operator or an arithmetic operator
               return (tokens.Count == 0 || tokens[^1] is TOpUnary || tokens[^1] is TOperator or TPunctuation { Punct: '(' })
                   ? new TOpUnary (mEval, ch) : new TOpArithmetic (mEval, ch);
            case '/' or '*' or '^' or '=':
               // Create an arithmetic operator token
               return new TOpArithmetic (mEval, ch);
            case >= '0' and <= '9':
               // Create a literal token
               return GetLiteral ();
            case >= 'a' and <= 'z':
               // Create an identifier token
               return GetIdentifier ();
            case '(' or ')':
               // Adjust evaluator priority based on parentheses
               mEval.BasePriority += ch == '(' ? 10 : -10;
               // Create a punctuation token
               return new TPunctuation (ch);
            //case '(' or ')':
            //   // Adjust evaluator priority based on parentheses
            //   if(mText.Any(ch=>ch=='(')) openParenthesesCount++;
            //   if (mText.Any (ch => ch == ')')) closeParenthesesCount++;
            //   if (mText.Count()) {
            //      // Adjust base priority when parentheses count is zero
            //      mEval.BasePriority += ch == '(' ? 10 : -10;
            //   } else throw new EvalException ("Unbalanced parantheses");

            //   // Create a punctuation token
            //   return new TPunctuation (ch);
            default:
               // Create an error token for unknown symbols
               return new TError ($"Unknown symbol: {ch}");

         }
      }
      //// Return end of input token
      //return new TEnd ();
      // Check if the number of open and closed parentheses is the same
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
      // Extract the identifier from the input text
      var id = new string (mText
        .Skip (start)
        .TakeWhile (char.IsLetter)
        .ToArray ());
      // Update the position
      mN = start + id.Length;
      // Check if the identifier is a predefined function and create the corresponding token
      return TOpFunction.funcs.Contains (id) && mText.Any (ch => char.IsDigit(ch))
          ? new TOpFunction (mEval, id) : new TVariable (mEval, id);
   }

   /// <summary>Gets the literal token from the current position in the input text</summary>
   /// <returns>The literal token</returns>
   TLiteral GetLiteral () {
      int start = mN - 1;
      // Read digits and a decimal point to form the literal
      while (mN < mText.Length && (char.IsDigit (mText[mN]) || mText[mN] == '.')) mN++;
      // Create a literal token
      return new TLiteral (mEval, mText[start..mN]);
   }
   #endregion

   #region Private Members ------------------------------------------
   readonly Evaluator mEval;  // The evaluator that owns this
   readonly string mText;     // The input text we're parsing through
   int mN;                    // Position within the text
   #endregion
}
#endregion