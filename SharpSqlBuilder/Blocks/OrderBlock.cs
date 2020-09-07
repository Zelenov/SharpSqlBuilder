using System;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>table.column DESC</example>
    /// <example>table.column ASC</example>
    /// </summary>
    public class OrderBlock : SqlBuilderEntity
    {
        public OrderDirection Direction;
        public TableColumnOperand OrderItemOperand;

        public OrderBlock(SqlColumn sqlColumn, OrderDirection direction)
        {
            OrderItemOperand = new TableColumnOperand(sqlColumn ?? throw new ArgumentException(nameof(sqlColumn)));
            Direction = direction;
        }

        public override bool Present(SqlOptions sqlOptions) => OrderItemOperand.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            string dir;
            switch (Direction)
            {
                case OrderDirection.Asc:
                    dir = "ASC";
                    break;
                case OrderDirection.Desc:
                    dir = "DESC";
                    break;
                default: return null;
            }

            var command = sqlOptions.Command(dir);
            var orderItem = OrderItemOperand?.BuildSql(sqlOptions) ??
                throw new ArgumentException(nameof(OrderItemOperand));
            return $"{orderItem} {command}";
        }
    }
}