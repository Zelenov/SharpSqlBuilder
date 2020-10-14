using System;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>ON ... = ... XXX ... = ...</example>
    /// </summary>
    public class JoinOnBlock : SqlBuilderEntity
    {
        public readonly Operator Property;

        public JoinOnBlock(Operator on)
        {
            Property = on ?? throw new ArgumentException(nameof(on));
        }

        public override bool Present(SqlOptions sqlOptions) => Property.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var command = sqlOptions.Command("ON");
            var property = Property.BuildSql(sqlOptions, FlowOptions.Construct(this));
            return $"{command} {property}";
        }
    }
}