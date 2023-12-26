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



#region Class TNumber --------------------------------------------
/// <summary> Represents a numeric token in the evaluator</summary>
abstract class TNumber : Token {
   public abstract double Value { get; }
}
#endregion

#region Class TLiteral -------------------------------------------
/// <summary>Represents a literal numeric token in the evaluator</summary>
class TLiteral : TNumber {
   public TLiteral (double f) => mValue = f;
   public override double Value => mValue;
   public override string ToString () => $"TLiteral {Value}";
   private readonly double mValue;
}
#endregion

#region Class TVariable ------------------------------------------
class TVariable : TNumber {
   public TVariable (Evaluator eval, string name) => (Name, mEval) = (name, eval);
   public string Name { get; private set; }
   public override double Value => mEval.GetVariable (Name);
   public override string ToString () => $"var:{Name}";
   readonly Evaluator mEval;
}
#endregion

#region Class TOperator ------------------------------------------------
/// <summary> Represents an operator token in the evaluator </summary>
abstract class TOperator : Token {
   protected TOperator (Evaluator eval) => mEval = eval;
   public abstract int Priority { get; }
   readonly protected Evaluator mEval;
}
#endregion

#region Classs TOpArithmetic -------------------------------------
class TOpArithmetic : TOperator {
   public TOpArithmetic (Evaluator eval, char ch) : base (eval) => Op = ch;
   public char Op { get; set; }
   public override string ToString () => $"TBinary {Op}:{Priority}";
   public override int Priority => sPriority[Op] + mEval.BasePriority;
   static Dictionary<char, int> sPriority = new () {
      ['+'] = 1, ['-'] = 1, ['*'] = 2, ['/'] = 2, ['^'] = 3, ['='] = 4,
   };
   public double Evaluate (double a, double b) {
      return Op switch {
         '-' => a - b,
         '+' => a + b,
         '*' => a * b,
         '/' => a / b,
         '^' => Math.Pow (a, b),
         _ => 0
      };
   }
}
#endregion

#region Class TOpUnary -------------------------------------------
class TOpUnary : TOperator {
   public TOpUnary (Evaluator eval, char op) : base (eval) => Op = op;
   public override int Priority => 6 + mEval.BasePriority;
   public double Evaluate (double a) =>
       Op switch {
          '+' => a,
          '-' => -a,
          _ => throw new EvalException ("Unary Operator not Implemented")
       };
   public override string ToString () => $"TUnary {Op}";

   public char Op { get; private set; }
}
#endregion

#region Class TOpFunc --------------------------------------------
/// <summary> Represents a function operator token in the evaluator</summary>
class TOpFunction : TOperator {
   public TOpFunction (Evaluator eval, string name) : base (eval) => Func = name;
   public override int Priority => 5 + mEval.BasePriority;
   public double Evaluate (double a) {
      return Func switch {
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
   }
   /// <summary> Returns a string representation of the function operator token </summary>
   public override string ToString () => $"TFunc {Func}";

   /// <summary> Gets the name of the function </summary>
   public string Func { get; private set; }

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
   public TPunctuation (char ch) => Punct = ch;
   public char Punct { get; private set; }
   public override string ToString () => $"punct:{Punct}";

   /// <summary> Gets the character representing the punctuation</summary>
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