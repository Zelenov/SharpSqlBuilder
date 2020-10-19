namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>MIN(...)</example>
    /// </summary>
    public class MinAggregate : AggregateOperand
    {
        public MinAggregate(AggregatableEntity aggregatableEntity) : base(aggregatableEntity)
        {
        }

        protected override string Command { get; } = "MIN";
    }
}