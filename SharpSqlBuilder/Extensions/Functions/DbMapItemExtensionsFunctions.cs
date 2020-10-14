using System;
using System.Collections.Generic;
using System.Text;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Extensions
{
    public static class DbMapItemExtensionsFunctions
    {
        public static AbsOperand Abs(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Abs();

        public static AbsOperand Abs(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Abs();
        public static AcosOperand Acos(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Acos();

        public static AcosOperand Acos(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Acos();
        public static AsinOperand Asin(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Asin();

        public static AsinOperand Asin(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Asin();
        public static AtanOperand Atan(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Atan();

        public static AtanOperand Atan(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Atan();
        public static Atn2Operand Atn2(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Atn2();

        public static Atn2Operand Atn2(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Atn2();
        public static AvgOperand Avg(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Avg();

        public static AvgOperand Avg(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Avg();
        public static CeilingOperand Ceiling(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Ceiling();

        public static CeilingOperand Ceiling(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Ceiling();
        public static CountOperand Count(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Count();

        public static CountOperand Count(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Count();
        public static CosOperand Cos(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Cos();

        public static CosOperand Cos(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Cos();
        public static CotOperand Cot(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Cot();

        public static CotOperand Cot(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Cot();
        public static DegreesOperand Degrees(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Degrees();

        public static DegreesOperand Degrees(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Degrees();
        public static ExpOperand Exp(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Exp();

        public static ExpOperand Exp(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Exp();
        public static FloorOperand Floor(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Floor();

        public static FloorOperand Floor(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Floor();
        public static LogOperand Log(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Log();

        public static LogOperand Log(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Log();
        public static Log10Operand Log10(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Log10();

        public static Log10Operand Log10(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Log10();
        public static MaxOperand Max(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Max();

        public static MaxOperand Max(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Max();
        public static MinOperand Min(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Min();

        public static MinOperand Min(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Min();
        public static RadiansOperand Radians(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Radians();

        public static RadiansOperand Radians(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Radians();
        public static RoundOperand Round(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Round();

        public static RoundOperand Round(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Round();
        public static SignOperand Sign(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sign();

        public static SignOperand Sign(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Sign();
        public static SinOperand Sin(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sin();

        public static SinOperand Sin(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Sin();
        public static SqrtOperand Sqrt(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sqrt();

        public static SqrtOperand Sqrt(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Sqrt();
        public static SquareOperand Square(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Square();

        public static SquareOperand Square(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Square();
        public static SumOperand Sum(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Sum();

        public static SumOperand Sum(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Sum();
        public static TanOperand Tan(this SqlColumn self) =>
                    Operand.From(self ?? throw new ArgumentException(nameof(self))).Tan();

        public static TanOperand Tan(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Tan();

        public static LowerOperand Lower(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Lower();

        public static LowerOperand Lower(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Lower();
        public static UpperOperand Upper(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Upper();

        public static UpperOperand Upper(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Upper();
        public static LTrimOperand LTrim(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).LTrim();

        public static LTrimOperand LTrim(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).LTrim();
        public static RTrimOperand RTrim(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).RTrim();

        public static RTrimOperand RTrim(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).RTrim();
        public static TrimOperand Trim(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Trim();

        public static TrimOperand Trim(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).Trim();
    }
}
