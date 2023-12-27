using System;

namespace Eval;

public class EvalException : Exception {
   public EvalException (string message) : base (message) { }
}
public class Evaluator {
   public double Evaluate (string text) {
      Reset ();
      List<Token> tokens = new ();
      var tokenizer = new Tokenizer (this, text);
      for (; ; ) {
         var token = tokenizer.Next (tokens);
         if (token is TEnd) break;
         if (token is TError err) throw new EvalException (err.Message);
         tokens.Add (token);
      }
      // Check if this is a variable assignment
      TVariable tVariable = null!;
      if (tokens.Count > 1 && tokens[0] is TVariable tv && tokens[1] is TOpArithmetic { Op: '=' }) {
         tVariable = tv;
         tokens.RemoveRange (0, 2);
      }
      for (int i = 0; i < tokens.Count; i++) {
         if (tokens[i] is TOpArithmetic bin && bin.Op is '+' or '-' && tokens[i + 1] is TOpUnary un) {
            bin.Op = bin.Op == un.Op ? '+' : '-';
            tokens.Remove (un);
         }
      }
      foreach (Token t in tokens) Process (t);
      while (mOperators.Count > 0) ApplyOperator ();
      if (mOperators.Count > 0) throw new EvalException ("Excessive use of operators");
      if (mOperands.Count != 1) throw new EvalException ("Excessive use of operands");
      if (BasePriority != 0) throw new EvalException ("Mismatched Paranthesis");
      double f = mOperands.Pop ();
      if (tVariable != null) mVars[tVariable.Name] = f;
      return f;
   }

   public int BasePriority { get; set; }

   public double GetVariable (string name) {
      if (mVars.TryGetValue (name, out double f)) return f;
      throw new EvalException ($"Unknown variable: {name}");
   }
   readonly Dictionary<string, double> mVars = new ();

   void Process (Token token) {
      switch (token) {
         case TNumber num:
            mOperands.Push (num.Value);
            break;
         case TOperator op:
            while (mOperators.Count > 0 && mOperands.Count > 0 && mOperators.Peek ().Priority >= op.Priority)
               ApplyOperator ();
            mOperators.Push (op);
            break;
         case TPunctuation p:
            BasePriority += p.Punct == '(' ? 10 : -10;
            break;
         default:
            throw new EvalException ($"Unknown token: {token}");
      }
   }
   readonly Stack<double> mOperands = new ();
   readonly Stack<TOperator> mOperators = new ();

   void ApplyOperator () {
      var op = mOperators.Pop ();
      var f1 = mOperands.Pop ();
      double f2;
      if (op is TOpArithmetic bin) {
         try {
            f2 = mOperands.Pop ();
         } catch (Exception) { throw new EvalException ("Too few operands"); }
         mOperands.Push (bin.Evaluate (f2, f1));
      } else mOperands.Push (op is TOpUnary u ? u.Evaluate (f1) : ((TOpFunction)op).Evaluate (f1));
   }
   void Reset () {
      mOperators.Clear ();
      mOperands.Clear ();
      BasePriority = 0;
   }
}