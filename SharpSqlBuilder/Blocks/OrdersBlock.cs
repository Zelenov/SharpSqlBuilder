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
        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> OrderProps =
            new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();

        public void AddRange(OrderMap orderMap, object order)
        {
            AddRange(ParseOrderFilter(orderMap, order));
        }

        private IEnumerable<OrderBlock> ParseOrderFilter(OrderMap orderMap, object order)
        {
            if (order == null || orderMap == null)
                return Enumerable.Empty<OrderBlock>();

            var props = OrderProps.GetOrAdd(order.GetType(),
                o => o.GetProperties().Where(p => p.PropertyType == typeof(OrderItem)).ToDictionary(p => p.Name));
            var setOrders = props.Values.Select(p => (o: (OrderItem) p.GetValue(order), p))
               .Where(t => t.p != null && t.o != null)
               .Select(t => (orderMap: orderMap.Items[t.p.Name], t.o))
               .Where(t => t.orderMap != null)
               .OrderBy(t => t.o.Index)
               .Select(t => new OrderBlock(t.orderMap.Prop, t.o.Direction));
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