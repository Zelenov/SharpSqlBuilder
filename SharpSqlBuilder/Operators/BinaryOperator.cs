using System;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    public abstract class BinaryOperator : Operator
    {
        public readonly Operand LeftOperand;
        public readonly Operand RightOperand;

        protected BinaryOperator(Operand leftOperand, Operand rightOperand)
        {
            LeftOperand = leftOperand ?? throw new ArgumentException(nameof(leftOperand));
            RightOperand = rightOperand ?? throw new ArgumentException(nameof(rightOperand));
        }

        public override bool Present(SqlOptions sqlOptions) =>
            LeftOperand.Present(sqlOptions) && RightOperand.Present(sqlOptions);
    }
}