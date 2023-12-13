// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2023.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Tokens.cs
// Represents a function operator token in the evaluator.
// ------------------------------------------------------------------------------------------------
namespace Training;
/// <summary> Represents a base class for tokens in the evaluator</summary>
abstract class Token {
}

#region Class EvalException -----------------------------------------
/// <summary>Represents an exception that occurs during evaluation in the Evaluator class</summary>
public class EvalException : Exception {
   /// <summary>
   /// Initializes a new instance of the EvalException class with a specified error message
   /// </summary>
   /// <param name="message">The error message that explains the reason for the exception</param>
   public EvalException (string message) : base (message) { }
}
#endregion

#region Class TNumber --------------------------------------------
/// <summary> Represents a numeric token in the evaluator</summary>
class TNumber : Token {
   /// <summary> Initializes a new instance of the <see cref="TNumber"/> class</summary>
   /// <param name="eval">The evaluator associated with the token</param>
   public TNumber (Evaluator eval) => mEval = eval;

   /// <summary>Gets the numeric value of the token</summary>
   public virtual double Value { get; }

   /// <summary> Gets the evaluator associated with the token</summary>
   protected Evaluator mEval { get; }
}
#endregion

#region Class TLiteral -------------------------------------------
/// <summary>Represents a literal numeric token in the evaluator</summary>
class TLiteral : TNumber {
   /// <summary> Initializes a new instance of the <see cref="TLiteral"/> class</summary>
   /// <param name="eval">The evaluator associated with the token</param>
   /// <param name="num">The numeric value of the literal</param>
   public TLiteral (Evaluator eval, string num) : base (eval) => mValue = double.Parse (num);

   /// <summary> Gets the numeric value of the literal token </summary>
   public override double Value => mValue;

   /// <summary> Returns a string representation of the literal token </summary>
   public override string ToString () => $"TLiteral {Value}";

   /// <summary> Gets the numeric value stored in the literal token</summary>
   private readonly double mValue;
}
#endregion

#region Class TVariable ------------------------------------------
/// <summary> Represents a variable token in the evaluator</summary>
class TVariable : TNumber {
   /// <summary>
   /// Initializes a new instance of the <see cref="TVariable"/> class.
   /// </summary>
   /// <param name="eval">The evaluator associated with the token.</param>
   /// <param name="name">The name of the variable.</param>
   public TVariable (Evaluator eval, string name) : base (eval) => Name = name;

   /// <summary> Gets the name of the variable</summary>
   public string Name { get; }

   /// <summary> Gets the value of the variable from the evaluator</summary>
   public override double Value => mEval.GetVariable (Name);

   /// <summary> Returns a string representation of the variable token</summary>
   public override string ToString () => $"TVariable {Name} = {Value}";
}
#endregion

#region Class TOperator ------------------------------------------------
/// <summary> Represents an operator token in the evaluator </summary>
class TOperator : Token {
   /// <summary> Initializes a new instance of the <see cref="TOperator"/> class</summary>
   /// <param name="eval">The evaluator associated with the token</param>
   internal TOperator (Evaluator eval) => mEval = eval;

   /// <summary> Gets the priority of the operator </summary>
   internal virtual int Priority { get; }

   /// <summary> Gets the final priority of the operator </summary>
   internal virtual int FinalPriority { get; }

   /// <summary> Gets the evaluator associated with the token</summary>
   readonly protected Evaluator mEval;
}
#endregion

#region Classs TOpArithmetic -------------------------------------
/// <summary> Represents an arithmetic operator token in the evaluator</summary>
class TOpArithmetic : TOperator {
   /// <summary>Initializes a new instance of the <see cref="TOpArithmetic"/> class</summary>
   /// <param name="eval">The evaluator associated with the token</param>
   /// <param name="ch">The character representing the arithmetic operation</param>
   public TOpArithmetic (Evaluator eval, char ch) : base (eval) {
      Op = ch;
      mFinalP = Priority + mEval.BasePriority;
   }

   /// <summary> Gets the priority of the arithmetic operator </summary>
   internal override int Priority => sPriority[Op];

   /// <summary> Gets the final priority of the arithmetic operator </summary>
   internal override int FinalPriority => mFinalP;

   /// <summary> Gets the final priority of the arithmetic operator</summary>
   private readonly int mFinalP;

   /// <summary> Applies the arithmetic operation on two operands </summary>
   /// <param name="a">The first operand</param>
   /// <param name="b">The second operand</param>
   /// <returns>The result of the arithmetic operation</returns>
   public double Apply (double a, double b) =>
       Op switch {
          '-' => a - b,
          '+' => a + b,
          '*' => a * b,
          '/' => a / b,
          '^' => Math.Pow (a, b),
          _ => 0
       };

   /// <summary> Returns a string representation of the arithmetic operator token </summary>
   public override string ToString () => $"TBinary {Op}";

   /// <summary> Gets the character representing the arithmetic operation </summary>
   public char Op { get; set; }

   /// <summary> Static dictionary for operator priorities</summary>
   private static readonly Dictionary<char, int> sPriority = new () {
      ['+'] = 1, ['-'] = 1, ['*'] = 2, ['/'] = 2, ['^'] = 3, ['='] = 4,
   };
}
#endregion

#region Class TOpUnary -------------------------------------------
/// <summary> Represents a unary operator token in the evaluator </summary>
class TOpUnary : TOperator {
   /// <summary> Initializes a new instance of the <see cref="TOpUnary"/> class </summary>
   /// <param name="eval">The evaluator associated with the token</param>
   /// <param name="op">The character representing the unary operation</param>
   public TOpUnary (Evaluator eval, char op) : base (eval) {
      Op = op;
      mFinalP = Priority + mEval.BasePriority;
   }

   /// <summary> Gets the priority of the unary operator</summary>
   internal override int Priority => 6;

   /// <summary>Applies the unary operation on an operand</summary>
   /// <param name="a">The operand</param>
   /// <returns>The result of the unary operation</returns>
   public double Apply (double a) =>
       Op switch {
          '+' => a,
          '-' => -a,
          _ => throw new EvalException ("Unary Operator not Implemented")
       };

   /// <summary> Gets the final priority of the unary operator</summary>
   internal override int FinalPriority => mFinalP;

   private readonly int mFinalP;

   /// <summary>String representation of the unary operator token.</summary>
   public override string ToString () => $"TUnary {Op}";

   /// <summary> Gets the character representing the unary operation</summary>
   internal char Op { get; private set; }
}
#endregion

#region Class TOpFunc --------------------------------------------
/// <summary> Represents a function operator token in the evaluator</summary>
class TOpFunction : TOperator {
   /// <summary>
   /// Initializes a new instance of the <see cref="TOpFunction"/> class
   /// </summary>
   /// <param name="eval">The evaluator associated with the token</param>
   /// <param name="func">The name of the function</param>
   internal TOpFunction (Evaluator eval, string func) : base (eval) {
      Op = func;
      mFinalP = Priority + mEval.BasePriority;
   }

   /// <summary> Gets the priority of the function operator </summary>
   internal override int Priority => 5;

   /// <summary>
   /// Gets the final priority of the function operator</summary>
   internal override int FinalPriority => mFinalP;

   private readonly int mFinalP;
   /// <summary> Applies the function operation on an operand</summary>
   /// <param name="a">The operand</param>
   /// <returns>The result of the function operation</returns>
   public double Apply (double a) =>
       Op switch {
          "sin" => Math.Sin (D2R (a)),
          "cos" => Math.Cos (D2R (a)),
          "tan" => Math.Tan (D2R (a)),
          "acos" => R2D (Math.Acos (a)),
          "asin" => R2D (Math.Asin (a)),
          "atan" => R2D (Math.Atan (a)),
          "log" => Math.Log (a),
          "exp" => Math.Exp (a),
          "sqrt" => Math.Sqrt (a),
          _ => throw new EvalException ("Function not Implemented")
       };

   /// <summary> Returns a string representation of the function operator token </summary>
   public override string ToString () => $"TFunc {Op}";

   /// <summary> Gets the name of the function </summary>
   internal string Op { get; private set; }

   /// <summary> Array of supported function names </summary>
   internal static readonly string[] funcs = { "sin", "cos", "tan", "asin", "acos", "atan", "log", "exp", "sqrt" };

   /// <summary>Converts degrees to radians</summary>
   /// <param name="d">Angle in degrees</param>
   /// <returns>Angle in radians</returns>
   private static double D2R (double d) => d * Math.PI / 180;

   /// <summary>Converts radians to degrees</summary>
   /// <param name="d">Angle in radians</param>
   /// <returns>Angle in degrees</returns>
   private static double R2D (double r) => r * 180 / Math.PI;
}
#endregion

#region Class TPunctuation ------------------------------------------
/// <summary>Represents a punctuation token in the evaluator</summary>
class TPunctuation : Token {
   /// <summary>
   /// Initializes a new instance of the <see cref="TPunctuation"/> class
   /// </summary>
   /// <param name="ch">The character representing the punctuation</param>
   public TPunctuation (char ch) => mPunc = ch;

   /// <summary> Gets the character representing the punctuation</summary>
   public char Punct => mPunc;
   private readonly char mPunc;
}
#endregion

#region Class TEnd -----------------------------------------------
/// <summary>Represents the end token in the evaluator</summary>
class TEnd : Token {
   /// <summary> Returns a string representation of the end token</summary>
   public override string ToString () => "end";
}
#endregion

#region Class TError ---------------------------------------------
/// <summary> Represents an error token in the evaluator</summary>
class TError : Token {
   /// <summary>
   /// Initializes a new instance of the <see cref="TError"/> class with the specified error message.
   /// </summary>
   /// <param name="msg">The error message</param>
   public TError (string msg) => Message = msg;

   /// <summary> Gets the error message associated with the token</summary>
   public string Message { get; private set; }

   /// <summary> Returns a string representation of the error token </summary>
   public override string ToString () => $"error:{Message}";
}
#endregion