using System;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    ///     Represents custom data operand. Can be used for any data representation like '10', '2000-01-01'
    /// </summary>
    public class CustomSqlOperand : Operand
    {
        public readonly string Data;

        public CustomSqlOperand(string data)
        {
            Data = data;
        }

        public override bool Present(SqlOptions sqlOptions) => Data != null;

        public override string BuildSql(SqlOptions sqlOptions) => Data;
    }
}