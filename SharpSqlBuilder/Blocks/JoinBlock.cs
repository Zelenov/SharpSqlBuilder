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

        public JoinBlock(JoinType joinType, DbMap dbMap, Operator on = null)
        {
            JoinType = joinType;
            TableEntity = dbMap != null ? new TableEntity(dbMap) : throw new ArgumentException(nameof(dbMap));
            OnBlock = on == null ? null : new JoinOnBlock(on);
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
            var table = TableEntity.BuildSql(sqlOptions);
            var on = OnBlock?.Present(sqlOptions) != true ? null : $" {OnBlock.BuildSql(sqlOptions)}";
            return $"{command} {table}{on}";
        }
    }
}