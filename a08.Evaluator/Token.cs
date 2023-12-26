namespace Eval;

abstract class Token {
}

abstract class TNumber : Token {
   public TNumber (Evaluator eval) => mEval = eval;

   public abstract double Value { get; }

   protected Evaluator mEval { get; }
}

class TLiteral : TNumber {

   public TLiteral (Evaluator eval, string num) : base (eval) => mValue = double.Parse (num);
   //public TLiteral (double f) => mValue = f;

   public override double Value => mValue;

   public override string ToString () => $"TLiteral {Value}";

   private readonly double mValue;
}

class TVariable : TNumber {
   public TVariable (Evaluator eval, string name) : base (eval) => Name = name;
   //public TVariable (Evaluator eval, string name) => (Name, mEval) = (name, eval);
   public string Name { get; private set; }
   public override double Value => mEval.GetVariable (Name);
   public override string ToString () => $"TVariable {Name}";
}

abstract class TOperator : Token {
   protected TOperator (Evaluator eval) => mEval = eval;
   public abstract int Priority { get; }
   readonly protected Evaluator mEval;
}

class TOpArithmetic : TOperator {
   public TOpArithmetic (Evaluator eval, char ch) : base (eval) => Op = ch;
   public char Op { get; set; }
   public override string ToString () => $"op:{Op}:{Priority}";
   public override int Priority => sPriority[Op] + mEval.BasePriority;
   static Dictionary<char, int> sPriority = new () {
      ['+'] = 1, ['-'] = 1, ['*'] = 2, ['/'] = 2, ['^'] = 3, ['='] = 4,
   };

   public double Evaluate (double a, double b) {
      return Op switch {
         '+' => a + b, '-' => a - b, 
         '*' => a * b, '/' => a / b,
         '^' => Math.Pow (a, b),
         _ => throw new EvalException ($"Unknown operator: {Op}"),
      };
   }
}

class TOpUnary : TOperator {
   public TOpUnary (Evaluator eval, char op) : base (eval) => Op = op;
   public override int Priority => 9 + mEval.BasePriority;
   public double Evaluate (double a) =>
       Op switch {
          '+' => a,
          '-' => -a,
          _ => throw new EvalException ("Unary Operator not Implemented")
       };
   public override string ToString () => $"TUnary {Op}";

   /// <summary> Gets the character representing the unary operation</summary>
   public char Op { get; private set; }
}

class TOpFunction : TOperator {
   public TOpFunction (Evaluator eval, string name) : base (eval) => Func = name;
   public string Func { get; private set; }
   public override string ToString () => $"func:{Func}:{Priority}";
   public override int Priority => 3 + mEval.BasePriority;

   public double Evaluate (double f) {
      return Func switch {
         "sin" => Math.Sin (D2R (f)), 
         "cos" => Math.Cos (D2R (f)),
         "tan" => Math.Tan (D2R (f)),
         "sqrt" => Math.Sqrt (f),
         "log" => Math.Log (f),
         "exp" => Math.Exp (f),
         "asin" => R2D (Math.Asin (f)),
         "acos" => R2D (Math.Acos (f)),
         "atan" => R2D (Math.Atan (f)),
         _ => throw new EvalException ($"Unknown function: {Func}")
      };

      double D2R (double f) => f * Math.PI / 180;
      double R2D (double f) => f * 180 / Math.PI;
   }
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
   public TError (string message) => Message = message;
   public string Message { get; private set; }
   public override string ToString () => $"error:{Message}";
}