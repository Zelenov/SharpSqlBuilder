namespace SharpSqlBuilder.Operators
{
    public abstract class PriorityOperator : Operator
    {
        public abstract int GetPriority(SqlOptions options);

        internal override string BuildSql(SqlOptions sqlOptions, FlowOptions flowOptions)
        {
            var r = base.BuildSql(sqlOptions, flowOptions);
            var parentPriority = flowOptions.Parent?.GetPriority(sqlOptions);
            var priority = GetPriority(sqlOptions);
            if (priority > parentPriority)
                return $"({r})";
            return r;
        }

    }
}