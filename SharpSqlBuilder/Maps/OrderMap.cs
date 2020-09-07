using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSqlBuilder.Maps
{
    public class OrderMap
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> OrderProps =
            new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();

        public readonly Dictionary<string, OrderMapItem> Items;

        public OrderMap()
        {
        }

        public OrderMap(params OrderMapItem[] items)
        {
            Items = items.ToDictionary(i => i.OrderProp);
        }

        private OrderMap(OrderMap one, OrderMap other)
        {
            Items = new Dictionary<string, OrderMapItem>(one.Items);
            foreach (var item in other.Items)
            {
                if (Items.ContainsKey(item.Key))
                    throw new ArgumentException($"Key duplicate: {item.Key}");

                Items.Add(item.Key, item.Value);
            }
        }


        public static OrderMap FromOrder<TOrder>(SqlTable map) => FromOrder(map, typeof(TOrder));

        public static OrderMap FromOrder(SqlTable map, Type orderType)
        {
            var orderProps = OrderProps.GetOrAdd(orderType, o => o.GetProperties().ToDictionary(p => p.Name));
            var entityProps = map;
            var setOrders = orderProps.Values.Select(p =>
            {
                var mapItem = map[p.Name];
                if (mapItem == null)
                    throw new ArgumentException(
                        $"Order property {p.Name} of type {orderType.Name} have no counterpart in the map");

                var item = new OrderMapItem(mapItem, p.Name);
                return item;
            });
            return new OrderMap(setOrders.ToArray());
        }

        public OrderMap Concat(OrderMap other) => new OrderMap(this, other);
    }
}