using System;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Operands
{
    public abstract class FunctionOperand : Operand
    {
        public readonly Operand Operand;
        public readonly string Command;

        protected FunctionOperand(Operand operand, string command)
        {
            Operand = operand ?? throw new ArgumentException(nameof(operand));
            Command = command ?? throw new ArgumentException(nameof(command));
        }

        public override bool Present(SqlOptions sqlOptions) => Operand.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command(Command);
            return $"{command}({operand})";
        }
    }
}