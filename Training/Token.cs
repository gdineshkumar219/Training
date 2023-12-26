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

abstract class TNumber : Token {
   public abstract double Value { get; }
}

class TLiteral : TNumber {
   public TLiteral (double f) => mValue = f;
   public override double Value => mValue;
   public override string ToString () => $"TLiteral {Value}";
   private readonly double mValue;
}

class TVariable : TNumber {
   public TVariable (Evaluator eval, string name) => (Name, mEval) = (name, eval);
   public string Name { get; private set; }
   public override double Value => mEval.GetVariable (Name);
   public override string ToString () => $"var:{Name}";
   readonly Evaluator mEval;
}

abstract class TOperator : Token {
   protected TOperator (Evaluator eval) => mEval = eval;
   public abstract int Priority { get; }
   readonly protected Evaluator mEval;
}
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

class TOpFunction : TOperator {
   public TOpFunction (Evaluator eval, string name) : base (eval) => Func = name;
   public string Func { get; private set; }
   public override string ToString () => $"TFunc {Func}";
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

   private static double D2R (double d) => d * Math.PI / 180;
   private static double R2D (double r) => r * 180 / Math.PI;
}

class TPunctuation : Token {
   public TPunctuation (char ch) => Punct = ch;
   public char Punct { get; private set; }
   public override string ToString () => $"punct:{Punct}";
}

class TEnd : Token {
   public override string ToString () => "end";
}

class TError : Token {
   public TError (string msg) => Message = msg;
   public string Message { get; private set; }
   public override string ToString () => $"error:{Message}";
}