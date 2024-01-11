namespace Eval;

class Tokenizer {
   public Tokenizer (Evaluator eval, string text) {
      mText = text; mN = 0; mEval = eval;
   }
   readonly Evaluator mEval;  // The evaluator that owns this 
   readonly string mText;     // The input text we're parsing through
   int mN;                    // Position within the text

   public Token Next () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         Token? lastToken;
         switch (ch) {
            case ' ' or '\t': continue;
            case (>= '0' and <= '9') or '.': lastToken = GetNumber (); break;
            case '(' or ')':
               mEval.BasePriority += ch == '(' ? 10 : -10;
               lastToken = new TPunctuation (ch); break;
            case '+' or '-':
               lastToken = (mPrev is null || mPrev is TOperator or TPunctuation { Punct: '(' } or TOpUnary)
                   ? new TOpUnary (mEval, ch) : new TOpArithmetic (mEval, ch); break;
            case '*' or '/' or '^' or '=': lastToken = new TOpArithmetic (mEval, ch); break;
            case >= 'a' and <= 'z': lastToken = GetIdentifier (); break;
            default: lastToken = new TError ($"Unknown symbol: {ch}"); break;
         }
         mPrev = lastToken;
         return lastToken;
      }
      return new TEnd ();
   }
   private Token? mPrev = null;

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