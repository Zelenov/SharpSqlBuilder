using System;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    public abstract class UnaryOperator : Operator
    {
        public readonly Operand Operand;

        protected UnaryOperator(Operand operand)
        {
            Operand = operand ?? throw new ArgumentException(nameof(operand));
        }

        public override bool Present(SqlOptions sqlOptions) => Operand.Present(sqlOptions);
    }
}