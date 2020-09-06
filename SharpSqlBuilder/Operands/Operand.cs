using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Operands
{
    public abstract class Operand : SqlBuilderEntity
    {
        public override bool Present(SqlOptions sqlOptions) => true;

        public static TableColumnOperand From(DbMapItem self) => new TableColumnOperand(self);

        public static SqlFilterOperand From(SqlFilterItem self) => new SqlFilterOperand(self);

        public static CustomSqlOperand From(string data) => new CustomSqlOperand(data);

        public IsNullOperator IsNull() => new IsNullOperator(this);

        public IsTrueOperator IsTrue() => new IsTrueOperator(this);

        public IsFalseOperator IsFalse() => new IsFalseOperator(this);

        public NotNullOperator NotNull() => new NotNullOperator(this);

        public EqualsOneOperator EqualsOne(Operand other) => new EqualsOneOperator(this, other);

        public EqualsAnyOperator EqualsAny(Operand other) => new EqualsAnyOperator(this, other);

        public LessThanOperator LessThan(Operand other) => new LessThanOperator(this, other);

        public LessOrEqualToOperator LessOrEqualTo(Operand other) => new LessOrEqualToOperator(this, other);

        public MoreThanOperator MoreThan(Operand other) => new MoreThanOperator(this, other);

        public MoreOrEqualToOperator MoreOrEqualTo(Operand other) => new MoreOrEqualToOperator(this, other);
    }
}