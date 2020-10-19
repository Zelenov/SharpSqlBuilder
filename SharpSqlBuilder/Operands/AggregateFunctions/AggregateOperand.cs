using System;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operands
{
    public abstract class AggregateOperand : Operand
    {
        public readonly AggregatableEntity Entity;
        protected abstract string Command { get; }

        public AggregateOperand(AggregatableEntity aggregatableEntity)
        {
            Entity = aggregatableEntity ?? throw new ArgumentException(nameof(aggregatableEntity));
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var entity = Entity;
            var command =sqlOptions.Command(Command);
            return $"{command}({entity})";

        }
    }
}