using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSqlBuilder.Maps
{
    public class OrderMap
    {
        public readonly List<OrderMapItem> Items = new List<OrderMapItem>();

        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> OrderProps =
            new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();

        
        public OrderMap Append(SqlTable map, IEnumerable<NamedOrderItem> namedOrderItems,
            bool throwOnMissingProperty = true, bool caseSensitive = true)
        {
            if (namedOrderItems == null)
                return this;

            var setOrders = namedOrderItems.Where(x => x != null).Select(p =>
            {
                if (!map.TryGetColumn(p.PropertyName, out var mapItem, caseSensitive))
                    if (throwOnMissingProperty)
                        throw new ArgumentException($"Order property {p.PropertyName} have no counterpart in the map");
                    else
                        return null;

                var item = new OrderMapItem(mapItem, p.Direction);
                return item;
            }).Where(x => x != null);
            Items.AddRange(setOrders);
            return this;
        }
        public OrderMap Append(OrderMap other)
        {
            if (other == null)
                return this;
            return Append(other.Items);
        }
        public OrderMap Append(IEnumerable<OrderMapItem> orderItems)
        {
            if (orderItems == null)
                return this;
            Items.AddRange(orderItems);
            return this;
        }
        public OrderMap Append(SqlTable map, object order,
            bool throwOnMissingProperty = true, bool caseSensitive = true)
        {
            if (order == null)
                return this;

            var mapType = order.GetType();
            var orderProps = OrderProps.GetOrAdd(mapType, o => o.GetProperties()
                .Where(p => p.PropertyType == typeof(OrderItem)).ToDictionary(p => p.Name));
            var entityProps = map;
            var setOrders = orderProps.Values.Select(p =>
            {
                if (!map.TryGetColumn(p.Name, out var mapItem, caseSensitive))
                    if (throwOnMissingProperty)
                        throw new ArgumentException($"Order property {p.Name} of type {mapType.Name} have no counterpart in the map");
                    else
                        return (null, null);


                var orderItem = (OrderItem)p.GetValue(order);
                return (mapItem, orderItem);
            })
                .Where(g=>g.orderItem!=null)
                .OrderBy(g => g.orderItem.Index)
                .Select(g => new OrderMapItem(g.mapItem, g.orderItem.Direction));
            return Append(setOrders);
        }
        public static OrderMap Build(SqlTable map, IEnumerable<NamedOrderItem> namedOrderItems,
            bool throwOnMissingProperty = true, bool caseSensitive = true)
        {
            return new OrderMap().Append(map, namedOrderItems, throwOnMissingProperty, caseSensitive);
        }

        public static OrderMap FromOrder<T>(SqlTable map, T order) where T : class
        {
            return new OrderMap().Append(map, order);
        }
    }
}