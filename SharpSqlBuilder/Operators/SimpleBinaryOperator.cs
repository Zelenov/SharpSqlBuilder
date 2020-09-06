using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    public abstract class SimpleBinaryOperator : BinaryOperator
    {
        protected SimpleBinaryOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        protected abstract string Command { get; }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var left = LeftOperand.BuildSql(sqlOptions);
            var right = RightOperand.BuildSql(sqlOptions);
            var command = Command;
            return $"{left} {command} {right}";
        }
    }
}