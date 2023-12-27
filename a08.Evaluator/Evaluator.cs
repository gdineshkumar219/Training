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
      double f = Math.Round (mOperands.Pop (), 10);
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
            if (mOperators.Count > 0 && mOperators.Peek ().Priority >= op.Priority)
               ApplyOperator ();
            mOperators.Push (op);
            break;
         case TPunctuation p:
            //BasePriority += p.Punct == '(' ? 10 : -10;
            //break;
            if (p.Punct == '(') break;
            ApplyOperator ();
            break;
         default:
            throw new EvalException ($"Unknown token: {token}");
      }
   }
   readonly Stack<double> mOperands = new ();
   readonly Stack<TOperator> mOperators = new ();

   //void ApplyOperator () {
   //   var op = mOperators.Pop ();
   //   double a;
   //   try {
   //      a = mOperands.Pop ();
   //   } catch (Exception) {
   //      mOperators.Push (op);
   //      return;
   //   }
   //   switch (op) {
   //      case TOpFunction fun:
   //         mOperands.Push (fun.Evaluate (a));
   //         break;
   //      case TOpArithmetic bin:
   //         if (mOperands.Count < 1) throw new EvalException ("Insufficient operands provided");
   //         double b = mOperands.Pop ();
   //         mOperands.Push (bin.Evaluate (b, a));
   //         break;
   //      case TOpUnary u:
   //         mOperands.Push (u.Evaluate (a));
   //         break;
   //   }
   //}
   void ApplyOperator () {
      var op = mOperators.Pop ();
      if (op is TOpArithmetic binary) {
         if (mOperands.Count < 2) throw new EvalException ("Too few operands");
         double f1 = mOperands.Pop (), f2 = mOperands.Pop ();
         mOperands.Push (binary.Evaluate (f2, f1));
      }
      if (op is TOpUnary unary) {
         if (mOperands.Count < 1) throw new EvalException ("Too few operands");
         double f = mOperands.Pop ();
         mOperands.Push (unary.Evaluate (f));
      }
      if (op is TOpFunction func) {
         if (mOperands.Count < 1) throw new EvalException ("Too few operands");
         double f = mOperands.Pop ();
         mOperands.Push (func.Evaluate (f));
      }
   }

   void Reset () {
      mOperands.Clear ();
      mOperators.Clear ();
      BasePriority = 0;
   }
}