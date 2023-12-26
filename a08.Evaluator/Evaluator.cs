namespace Eval;

class EvalException : Exception {
   public EvalException (string message) : base (message) { }
}

class Evaluator {
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
      TVariable? tVariable = null;
      if (tokens.Count > 2 && tokens[0] is TVariable tvar && tokens[1] is TOpArithmetic { Op: '=' }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      for (int i = 0; i < tokens.Count; i++) {
         if (tokens[i] is TOpArithmetic bin && bin.Op is '+' or '-' && tokens[i + 1] is TOpUnary un) {
            bin.Op = bin.Op == un.Op ? '+' : '-';
            tokens.Remove (un);
         }
      }
      foreach (var t in tokens) Process (t);
      while (mOperators.Count > 0) ApplyOperator ();
      if (mOperators.Count != 0) throw new EvalException ("Excessive use of operators");
      if (mOperands.Count != 1) throw new EvalException ("Excessive use of operands");
      if (BasePriority != 0) throw new EvalException ("Mismatched Paranthesis");
      double f = mOperands.Pop ();
      if (tVariable != null) mVars[tVariable.Name] = f;
      return f; 
   }

   public int BasePriority { get; private set; }

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
            while (mOperators.Count > 0 && mOperators.Peek ().Priority > op.Priority)
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
      // Pop the top operator from the stack
      TOperator op = mOperators.Pop ();
      double a;
      try {
         // Try to pop the top operand from the stack
         a = mOperands.Pop ();
      } catch (Exception) {
         // If an exception occurs, push the operator back and return
         mOperators.Push (op);
         return;
      }
      // Switch on the type of the operator and apply it to the operands
      switch (op) {
         case TOpFunction fun:
            mOperands.Push (fun.Evaluate (a));
            break;
         case TOpArithmetic bin:
            if (mOperands.Count < 1) throw new EvalException ("Insufficient operands provided");
            double b = mOperands.Pop ();
            mOperands.Push (bin.Evaluate (b, a));
            break;
         case TOpUnary u:
            mOperands.Push (u.Evaluate (a));
            break;
      }
   }
   void Reset () {
      mOperands.Clear ();
      mOperators.Clear ();
      BasePriority = 0;
   }
}