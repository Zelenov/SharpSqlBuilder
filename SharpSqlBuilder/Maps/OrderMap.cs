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
            bool throwOnMissingProperty = true)
        {
            if (namedOrderItems == null)
                return this;

            var setOrders = namedOrderItems.Where(x => x != null).Select(p =>
            {
                var mapItem = map[p.PropertyName];
                if (mapItem == null)
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
        public OrderMap Append(SqlTable map, object order)
        {
            if (order == null)
                return this;

            var mapType = order.GetType();
            var orderProps = OrderProps.GetOrAdd(mapType, o => o.GetProperties()
                .Where(p => p.PropertyType == typeof(OrderItem)).ToDictionary(p => p.Name));
            var entityProps = map;
            var setOrders = orderProps.Values.Select(p =>
            {
                var mapItem = map[p.Name];
                if (mapItem == null)
                    throw new ArgumentException(
                        $"Order property {p.Name} of type {mapType.Name} have no counterpart in the map");


                var orderItem = (OrderItem)p.GetValue(order);
                return (mapItem, orderItem);
            })
                .Where(g=>g.orderItem!=null)
                .OrderBy(g => g.orderItem.Index)
                .Select(g => new OrderMapItem(g.mapItem, g.orderItem.Direction));
            return Append(setOrders);
        }
        public static OrderMap Build(SqlTable map, IEnumerable<NamedOrderItem> namedOrderItems,
            bool throwOnMissingProperty = true)
        {
            return new OrderMap().Append(map, namedOrderItems, throwOnMissingProperty);
        }

        public static OrderMap FromOrder<T>(SqlTable map, T order) where T : class
        {
            return new OrderMap().Append(map, order);
        }
    }
}