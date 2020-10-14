using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    public class FlowOptions
    {
        public PriorityOperator Parent;

        public FlowOptions WithParent(PriorityOperator priorityOperator)
        {
            Parent = priorityOperator;
            return this;
        }

        public static FlowOptions Construct(PriorityOperator priorityOperator)
        {
           return new FlowOptions().WithParent(priorityOperator);
        }
        public static FlowOptions Construct(SqlBuilderEntity _)
        {
           return new FlowOptions();
        }
    }
}