using System;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    public abstract class UnaryOperator : PriorityOperator
    {
        public readonly Operand Operand;

        protected UnaryOperator(Operand operand)
        {
            Operand = operand ?? throw new ArgumentException(nameof(operand));
        }

        public override bool Present(SqlOptions sqlOptions) => Operand.Present(sqlOptions);
    }
}