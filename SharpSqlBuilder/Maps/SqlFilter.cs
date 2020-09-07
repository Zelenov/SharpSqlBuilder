using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Maps
{
    public class SqlFilter
    {
        public readonly Dictionary<string, SqlFilterItem> Props;


        protected SqlFilter(IEnumerable<SqlFilterItem> sqlColumns)
        {
            Props = sqlColumns.ToDictionary(i => i.PropertyName);
        }

        public SqlFilterItem this[string property] => Props[property];

        public static SqlFilter<T> Construct<T>(T data) => new SqlFilter<T>(data);
    }

    public class SqlFilter<T> : SqlFilter
    {
        public readonly T Data;

        public SqlFilter(T data) : base(GetProps())
        {
            if (data == null)
                throw new ArgumentException(nameof(data));

            Data = data;
        }

        public SqlFilterItem this[Expression<Func<T, object>> exp] => base[exp.GetPropertyInfo().Name];

        protected static IEnumerable<SqlFilterItem> GetProps()
        {
            var type = typeof(T);
            return type.GetProperties().Select(p => new PropertySqlFilterItem(p));
        }
    }
}