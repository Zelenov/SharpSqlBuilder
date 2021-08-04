using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Operands
{
    public abstract class FunctionOperand : Operand
    {
        public readonly List<Operand> Operands = new List<Operand>();
        public readonly string Command;

        protected FunctionOperand(Operand operand, string command): this(command, new[]{operand})
        {
        }
        protected FunctionOperand(string command, IEnumerable<Operand> operands)
        {
            Operands.AddRange(operands ?? throw new ArgumentException(nameof(operands)));
            Command = command ?? throw new ArgumentException(nameof(command));
        }

        public override bool Present(SqlOptions sqlOptions) => Operands.Any(operand=>operand.Present(sqlOptions));

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operands = string.Join(", ",
                Operands.Where(operand => operand.Present(sqlOptions))
                   .Select(operand => operand.BuildSql(sqlOptions, FlowOptions.Construct(this))));
            var command = sqlOptions.Command(Command);
            return $"{command}({operands})";
        }
    }
}