namespace Eval;

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
   public int Priority { get; protected set; }
   readonly protected Evaluator mEval;
}

class TOpArithmetic : TOperator {
   public TOpArithmetic (Evaluator eval, char ch) : base (eval) {
      Op = ch;
      Priority = sPriority[Op] + mEval.BasePriority;
   }

   public char Op { get; set; }
   public override string ToString () => $"TBinary {Op}:{Priority}";
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
   public TOpUnary (Evaluator eval, char op) : base (eval) {
      Op = op;
      Priority = 6 + mEval.BasePriority;
   }
   public char Op { get; private set; }
   public override string ToString () => $"TUnary {Op}";
   public double Evaluate (double a) =>
       Op switch {
          '+' => a,
          '-' => -a,
          _ => throw new EvalException ("Unary Operator not Implemented")
       };
}

class TOpFunction : TOperator {
   public TOpFunction (Evaluator eval, string name) : base (eval) {
      Func = name;
      Priority = 5 + mEval.BasePriority;
   }
   public string Func { get; private set; }
   public override string ToString () => $"TFunc {Func}";
   public double Evaluate (double f) {
      return Func switch {
         "sin" => Math.Sin (D2R (f)),
         "cos" => Math.Cos (D2R (f)),
         "tan" => Math.Tan (D2R (f)),
         "acos" => R2D (Math.Acos (f)),
         "asin" => R2D (Math.Asin (f)),
         "atan" => R2D (Math.Atan (f)),
         "log" => Math.Log (f),
         "exp" => Math.Exp (f),
         "sqrt" => Math.Sqrt (f),
         _ => throw new EvalException ("Function not Implemented")
      };
   }

   private static double D2R (double f) => f * Math.PI / 180;
   private static double R2D (double f) => f * 180 / Math.PI;
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