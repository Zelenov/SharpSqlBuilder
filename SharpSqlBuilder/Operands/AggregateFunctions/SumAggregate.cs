namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>SUM(...)</example>
    /// </summary>
    public class SumAggregate : AggregateOperand
    {
        public SumAggregate(AggregatableEntity aggregatableEntity) : base(aggregatableEntity)
        {
        }

        protected override string Command { get; } = "SUM";
    }
}