using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>JOIN ... ON ...</example>
    /// </summary>
    public class JoinBlock : SqlBuilderEntity
    {
        public readonly JoinType JoinType;
        public readonly JoinOnBlock OnBlock;
        public readonly TableEntity TableEntity;

        public JoinBlock(JoinType joinType, SqlTable sqlTable, Operator on = null)
        {
            JoinType = joinType;
            TableEntity = new TableEntity(sqlTable ?? throw new ArgumentException(nameof(sqlTable)));
            OnBlock = new JoinOnBlock(on ?? throw new ArgumentException(nameof(on)));
        }

        public override bool Present(SqlOptions sqlOptions) => TableEntity.Present(sqlOptions);


        public override string BuildSql(SqlOptions sqlOptions)
        {
            string joinType;
            switch (JoinType)
            {
                case JoinType.Inner:
                    joinType = "INNER JOIN";
                    break;
                case JoinType.Left:
                    joinType = "LEFT JOIN";
                    break;
                case JoinType.Right:
                    joinType = "RIGHT JOIN";
                    break;
                case JoinType.FullOuter:
                    joinType = "FULL OUTER JOIN";
                    break;
                default: throw new ArgumentException(nameof(JoinType));
            }

            var command = sqlOptions.Command(joinType);
            var table = TableEntity.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var on = OnBlock.BuildSql(sqlOptions, FlowOptions.Construct(this));
            return $"{command} {table} {on}";
        }
    }
}