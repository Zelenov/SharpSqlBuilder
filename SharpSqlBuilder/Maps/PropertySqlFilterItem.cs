using System;
using System.Reflection;

namespace SharpSqlBuilder.Maps
{
    public class PropertySqlFilterItem : SqlFilterItem
    {
        public PropertySqlFilterItem(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentException(nameof(propertyInfo));
            PropertyName = propertyInfo.Name;
        }

        public PropertyInfo PropertyInfo { get; set; }
    }
}