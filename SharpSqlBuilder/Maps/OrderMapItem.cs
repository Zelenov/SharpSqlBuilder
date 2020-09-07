namespace SharpSqlBuilder.Maps
{
    public class OrderMapItem
    {
        public OrderMapItem()
        {
        }

        public OrderMapItem(SqlColumn prop, string orderProp)
        {
            Prop = prop;
            OrderProp = orderProp;
        }

        public SqlColumn Prop { get; set; }
        public string OrderProp { get; set; }
    }
}