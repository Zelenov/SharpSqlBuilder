using System;
using System.Collections.Generic;
using System.Text;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>LOWER(...)</example>
    /// </summary>
    public class LowerOperand : FunctionOperand
    {
        public LowerOperand(Operand operand) : base(operand, "LOWER")
        {
        }
    }
}
