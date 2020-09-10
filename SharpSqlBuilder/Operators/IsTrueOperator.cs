using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = TRUE</example>
    /// </summary>
    public class IsTrueOperator : UnaryOperator
    {
        public IsTrueOperator(Operand operand) : base(operand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions);
            var command = new TrueOperand().BuildSql(sqlOptions);
            return $"{operand} = {command}";
        }
    }
}