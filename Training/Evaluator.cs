// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Evaluator.cs
// Program to solve mathematical expressions.
// ------------------------------------------------------------------------------------------------
namespace Training;
#region Class Evaluator ---------------------------------------------
/// <summary>
/// Represents an expression evaluator that can process mathematical expressions
/// </summary>
public class Evaluator {
   #region Methods --------------------------------------------------
   /// <summary>Evaluates the specified mathematical expression and returns the result</summary>
   /// <param name="text">The mathematical expression to be evaluated</param>
   /// <returns>The result of the evaluation</returns>
   public double Evaluate (string text) {
      Reset ();
      // Create a tokenizer for the expression
      Tokenizer tokenizer = new (this, text);
      List<Token> tokens = new ();
      // Tokenize the expression and add tokens to the list
      for (; ; ) {
         Token t = tokenizer.Next (tokens);
         if (t is TEnd) break;
         tokens.Add (t);
      }
      // Check for variable assignment at the beginning of the expression
      TVariable var = null!;
      if (tokens.Count > 1 && tokens[0] is TVariable tv && tokens[1] is TOpArithmetic { Op: '=' }) {
         var = tv;
         tokens.RemoveRange (0, 2);
      }
      for (int i = 0; i < tokens.Count; i++) {
         if (tokens[i] is TOpArithmetic bin && bin.Op is '+' or '-' && tokens[i + 1] is TOpUnary un) {
            bin.Op = bin.Op == un.Op ? '+' : '-';
            tokens.Remove (un);
         }
      }
      // Process each token in the expression
      foreach (Token t in tokens) Process (t);
      while (mOperators.Count > 0) ApplyOperator ();
      if (mOperators.Count != 0) throw new EvalException ("Excessive use of operators");
      if (mOperands.Count != 1) throw new EvalException ("Excessive use of operands");
      if (BasePriority != 0) throw new EvalException ("Mismatched Paranthesis");
      // Retrieve the final result and round it to 10 decimal places
      double result = Math.Round (mOperands.Pop (), 10);
      // If a variable was assigned, store the result in the variables dictionary
      if (var != null) mVars[var.Name] = result;
      return result;
   }

   /// <summary>Retrieves the value of the specified variable from the evaluator's variables</summary>
   /// <param name="name">The name of the variable to retrieve</param>
   /// <returns>The value of the specified variable</returns>
   public double GetVariable (string name) {
      // Try to retrieve the variable value from the dictionary
      if (mVars.TryGetValue (name, out double f)) return f;
      // If the variable is not found, throw an EvalException
      throw new EvalException ($"Unknown variable: {name}");
   }
   #endregion

   #region Implementation -------------------------------------------
   /// <summary> Processes the given token during expression evaluation</summary>
   /// <param name="token">The token to be processed</param>
   void Process (Token token) {
      // Switch on the type of the token
      switch (token) {
         case TNumber num:
            // If the token is a number, push its value to the operand stack
            mOperands.Push (num.Value);
            break;
         case TPunctuation p:
            // If the token is punctuation, check for '(' and apply operators
            if (p.Punct == '(') break;
            ApplyOperator ();
            break;
         case TOperator op:
            // If the token is an operator, handle its processing
            if (mOperators.Count != 0 && mOperators.Peek ().FinalPriority >= op.FinalPriority)
               ApplyOperator ();
            mOperators.Push (op);
            break;
         default:
            // If the token type is unknown, throw an EvalException
            throw new EvalException ($"Unknown token: {token}");
      }
   }

   /// <summary> Applies the top operator from the stack during expression evaluation</summary>
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
            mOperands.Push (fun.Apply (a));
            break;
         case TOpArithmetic bin:
            if (mOperands.Count < 1) throw new EvalException ("Insufficient operands provided");
            double b = mOperands.Pop ();
            mOperands.Push (bin.Apply (b, a));
            break;
         case TOpUnary u:
            mOperands.Push (u.Apply (a));
            break;
      }
   }
   /// <summary>Clear operand and operator stacks before processing a new expression </summary>
   void Reset () {
      mOperands.Clear ();
      mOperators.Clear ();
      BasePriority = 0;
   }
   #endregion

   /// <summary> Represents the base priority for operators in the evaluator</summary>
   internal int BasePriority { get; set; }

   #region Private Members ------------------------------------------
   // Dictionary to store variables and their values
   readonly Dictionary<string, double> mVars = new ();
   // Operand and operator stacks used during evaluation
   readonly Stack<double> mOperands = new ();
   readonly Stack<TOperator> mOperators = new ();
   #endregion
}
#endregion