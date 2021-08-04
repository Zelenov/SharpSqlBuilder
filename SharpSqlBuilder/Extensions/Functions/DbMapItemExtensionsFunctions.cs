using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Extensions
{
    public static class DbMapItemExtensionsFunctions
    {
        public static AbsOperand Abs(this IOperable self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Abs();

        public static AcosOperand Acos(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Acos();

        public static AsinOperand Asin(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Asin();

        public static AtanOperand Atan(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Atan();

        public static Atn2Operand Atn2(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Atn2();

        public static AvgOperand Avg(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Avg();

        public static CeilingOperand Ceiling(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Ceiling();

        public static CosOperand Cos(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Cos();

        public static CotOperand Cot(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Cot();

        public static DegreesOperand Degrees(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Degrees();

        public static ExpOperand Exp(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Exp();

        public static FloorOperand Floor(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Floor();

        public static LogOperand Log(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Log();

        public static Log10Operand Log10(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Log10();

        public static MaxOperand Max(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Max();

        public static MinOperand Min(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Min();

        public static RadiansOperand Radians(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Radians();

        public static RoundOperand Round(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Round();

        public static SignOperand Sign(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sign();

        public static SinOperand Sin(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sin();

        public static SqrtOperand Sqrt(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sqrt();

        public static SquareOperand Square(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Square();

        public static SumOperand Sum(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sum();

        public static TanOperand Tan(this IOperable self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Tan();


        public static LowerOperand Lower(this IOperable self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Lower();

        public static UpperOperand Upper(this IOperable self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Upper();

        public static LTrimOperand LTrim(this IOperable self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).LTrim();

        public static RTrimOperand RTrim(this IOperable self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).RTrim();

        public static TrimOperand Trim(this IOperable self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Trim();

        public static CoalesceOperand Coalesce(this IOperable self, params IOperable[] operands) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Coalesce(operands.Select(x=>x.AsOperand));
    }
}
