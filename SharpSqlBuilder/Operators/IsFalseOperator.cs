using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = FALSE</example>
    /// </summary>
    public class IsFalseOperator : UnaryOperator
    {
        public IsFalseOperator(Operand operand) : base(operand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions);
            var command = new FalseOperand().BuildSql(sqlOptions);
            return $"{operand} = {command}";
        }
    }
}