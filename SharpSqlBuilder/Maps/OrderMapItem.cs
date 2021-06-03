namespace SharpSqlBuilder.Maps
{
    public class OrderMapItem
    {
        public OrderMapItem()
        {
        }

        public OrderMapItem(SqlColumn prop, OrderDirection direction)
        {
            Prop = prop;
            Direction = direction;
        }

        public SqlColumn Prop { get; set; }
        public OrderDirection Direction { get; set; }
    }
   
}