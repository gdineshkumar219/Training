﻿namespace Eval;

class Tokenizer {
   public Tokenizer (Evaluator eval, string text) {
      mText = text; mN = 0; mEval = eval;
   }
   readonly Evaluator mEval;  // The evaluator that owns this
   readonly string mText;     // The input text we're parsing through
   int mN;                    // Position within the text

   public Token Next (List<Token> tokens) {
      while (mN < mText.Length) {
         char ch = mText[mN++];
         switch (ch) {
            case ' ' or '\t': continue;  // Skip whitespace
            case '+' or '-':
               return (tokens.Count == 0 || tokens[^1] is TOperator or TPunctuation { Punct: '(' })
                   ? new TOpUnary (mEval, ch) : new TOpArithmetic (mEval, ch);
            case '/' or '*' or '^' or '=':
               return new TOpArithmetic (mEval, ch);
            case >= '0' and <= '9':
               return GetNumber ();
            case >= 'a' and <= 'z':
               return GetIdentifier ();
            case '(' or ')':
               mEval.BasePriority += ch == '(' ? 10 : -10;
               return new TPunctuation (ch);
            default:
               return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }

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
}