using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    public class OrdersBlock : CollectionBlock<OrderBlock>
    {
        public void AddRange(OrderMap orderMap)
        {
            AddRange(ParseOrderFilter(orderMap));
        }

        private IEnumerable<OrderBlock> ParseOrderFilter(OrderMap orderMap)
        {
            if (orderMap == null)
                return Enumerable.Empty<OrderBlock>();

            var setOrders = orderMap.Items.Select(t => new OrderBlock(t.Prop, t.Direction));
            return setOrders;
        }
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var orders = Entities.Select(s => s.BuildSql(sqlOptions, FlowOptions.Construct(this)));
            var order = string.Join($",{sqlOptions.NewLine()}{sqlOptions.Indent()}", orders);
            return $"{sqlOptions.Command("ORDER BY")}{sqlOptions.NewLine()}{sqlOptions.Indent()}{order}";
        }
    }
}