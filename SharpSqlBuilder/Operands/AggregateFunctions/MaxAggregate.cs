namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>MAX(...)</example>
    /// </summary>
    public class MaxAggregate : AggregateOperand
    {
        public MaxAggregate(AggregatableEntity aggregatableEntity) : base(aggregatableEntity)
        {
        }

        protected override string Command { get; } = "MAX";
    }
}