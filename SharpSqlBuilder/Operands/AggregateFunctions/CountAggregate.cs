namespace SharpSqlBuilder.Operands
{

    /// <summary>
    /// <example>COUNT(...)</example>
    /// </summary>
    public class CountAggregate : AggregateOperand
    {
        public CountAggregate(AggregatableEntity aggregatableEntity) : base(aggregatableEntity)
        {
        }

        protected override string Command { get; } = "COUNT";
    }
}
