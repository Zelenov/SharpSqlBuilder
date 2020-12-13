using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SharpSqlBuilder.Attributes;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Extensions
{
    internal static class TypeExtensions
    {
        public static NamingConvention GetNamingConvention(this Type type)
        {
            var namingConventionAttr = type.GetAttribute<NamingConventionAttribute>();
            return namingConventionAttr?.NamingConvention ??
                NamingConvention.AsIs;
        }
        public static string GetTableName(this Type type)
        {
            var namingConvention = type.GetNamingConvention();
            var tableNameAttr = GetAttribute<TableAttribute>(type);
            return tableNameAttr?.Name ?? type.Name.ToNamingConvention(namingConvention);
        }

        public static string GetSchema(this Type type)
        {
            var tableNameAttr = GetAttribute<TableAttribute>(type);
            return tableNameAttr?.Schema;
        }

        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            var attr = (TAttribute)Attribute.GetCustomAttribute(type, attributeType);
            return attr;
        }
    }
}