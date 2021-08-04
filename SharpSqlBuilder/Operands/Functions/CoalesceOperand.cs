using System.Collections;
using System.Collections.Generic;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>COALESCE(...)</example>
    /// </summary>
    public class CoalesceOperand : FunctionOperand
    {
        public CoalesceOperand(IEnumerable<Operand> operands) : base("COALESCE", operands)
        {
        }
        public CoalesceOperand(params Operand[] operands) : this((IEnumerable<Operand>) operands)
        {
        }
    }
}