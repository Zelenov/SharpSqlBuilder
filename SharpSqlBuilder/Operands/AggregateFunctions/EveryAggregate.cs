namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>EVERY(...)</example>
    /// </summary>
    public class EveryAggregate : AggregateOperand
    {
        public EveryAggregate(AggregatableEntity aggregatableEntity) : base(aggregatableEntity)
        {
        }

        protected override string Command { get; } = "EVERY";
    }
}