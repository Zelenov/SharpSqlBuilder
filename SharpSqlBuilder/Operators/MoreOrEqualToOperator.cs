﻿using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... &gt;= ...</example>
    /// </summary>
    public class MoreOrEqualToOperator : EqualityBinaryOperator
    {
        public MoreOrEqualToOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }


        protected override string Command { get; } = ">=";
    }
}