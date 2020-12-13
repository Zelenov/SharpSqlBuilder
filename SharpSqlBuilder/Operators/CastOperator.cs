using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>CAST (X as C)</example>
    /// </summary>
    public class CastOperator : Operator
    {
        private readonly Operand Value;
        private readonly string As;

        public CastOperator(Operand value, string @as)
        {
            Value = value;
            As = @as;
        }

        public override bool Present(SqlOptions sqlOptions) =>
            Value.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var value = Value.BuildSql(sqlOptions);
            var @as = As;
            if (@as == null)
                return value;
            var command = sqlOptions.Command("CAST");
            var asCommand = sqlOptions.Command("AS");
            return $"{command}({value} {asCommand} {@as})";
        }
    }
}