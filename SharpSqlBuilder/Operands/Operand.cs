using System.Linq;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Operands
{
    public abstract class Operand : SqlBuilderEntity
    {
        public override bool Present(SqlOptions sqlOptions) => true;

        public static TableColumnOperand From(SqlColumn self) => new TableColumnOperand(self);

        public static SqlFilterOperand From(SqlFilterItem self) => new SqlFilterOperand(self);

        public static CustomSqlOperand From(string data) => new CustomSqlOperand(data);

        public IsNullOperator IsNull() => new IsNullOperator(this);

        public IsTrueOperator IsTrue() => new IsTrueOperator(this);

        public IsFalseOperator IsFalse() => new IsFalseOperator(this);

        public NotNullOperator NotNull() => new NotNullOperator(this);
        public NotOperator Not() => new NotOperator(this);

        public EqualsOneOperator EqualsOne(Operand other) => new EqualsOneOperator(this, other);

        public EqualsAnyOperator EqualsAny(Operand other) => new EqualsAnyOperator(this, other);
        public EqualsAllOperator EqualsAll(Operand other) => new EqualsAllOperator(this, other);
        public NotEqualsOperator NotEquals(Operand other) => new NotEqualsOperator(this, other);

        public LessThanOperator LessThan(Operand other) => new LessThanOperator(this, other);

        public LessOrEqualToOperator LessOrEqualTo(Operand other) => new LessOrEqualToOperator(this, other);

        public MoreThanOperator MoreThan(Operand other) => new MoreThanOperator(this, other);

        public MoreOrEqualToOperator MoreOrEqualTo(Operand other) => new MoreOrEqualToOperator(this, other);
        public LikeOperator Like(Operand other) => new LikeOperator(this, other);
        public ILikeOperator ILike(Operand other) => new ILikeOperator(this, other);
        public AbsOperand Abs() => new AbsOperand(this);
        public AcosOperand Acos() => new AcosOperand(this);
        public AsinOperand Asin() => new AsinOperand(this);
        public AtanOperand Atan() => new AtanOperand(this);
        public Atn2Operand Atn2() => new Atn2Operand(this);
        public AvgOperand Avg() => new AvgOperand(this);
        public CeilingOperand Ceiling() => new CeilingOperand(this);
        public CosOperand Cos() => new CosOperand(this);
        public CotOperand Cot() => new CotOperand(this);
        public DegreesOperand Degrees() => new DegreesOperand(this);
        public ExpOperand Exp() => new ExpOperand(this);
        public FloorOperand Floor() => new FloorOperand(this);
        public LogOperand Log() => new LogOperand(this);
        public Log10Operand Log10() => new Log10Operand(this);
        public MaxOperand Max() => new MaxOperand(this);
        public MinOperand Min() => new MinOperand(this);
        public RadiansOperand Radians() => new RadiansOperand(this);
        public RoundOperand Round() => new RoundOperand(this);
        public SignOperand Sign() => new SignOperand(this);
        public SinOperand Sin() => new SinOperand(this);
        public SqrtOperand Sqrt() => new SqrtOperand(this);
        public SquareOperand Square() => new SquareOperand(this);
        public SumOperand Sum() => new SumOperand(this);
        public TanOperand Tan() => new TanOperand(this);
        public LowerOperand Lower() => new LowerOperand(this);
        public UpperOperand Upper() => new UpperOperand(this);
        public LTrimOperand LTrim() => new LTrimOperand(this);
        public RTrimOperand RTrim() => new RTrimOperand(this);
        public TrimOperand Trim() => new TrimOperand(this);

        public OrOperator Or(params Operand[] operators)
        {
            return new OrOperator(new[] { this }.Concat(operators));
        }

        public AndOperator And(params Operand[] operators)
        {
            return new AndOperator(new[] { this }.Concat(operators));
        }


    }
}