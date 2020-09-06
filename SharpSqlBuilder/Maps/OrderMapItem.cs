namespace SharpSqlBuilder.Maps
{
    public class OrderMapItem
    {
        public OrderMapItem()
        {
        }

        public OrderMapItem(DbMapItem prop, string orderProp)
        {
            Prop = prop;
            OrderProp = orderProp;
        }

        public DbMapItem Prop { get; set; }
        public string OrderProp { get; set; }
    }
}