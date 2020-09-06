using System;
using System.Reflection;

namespace SharpSqlBuilder.Extensions
{
    public static class MemberInfoExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo t) where TAttribute : Attribute
        {
            var type = typeof(TAttribute);
            var attr = (TAttribute) Attribute.GetCustomAttribute(t, type);
            return attr;
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumVal) where TAttribute : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TAttribute), false);
            return attributes.Length > 0 ? (TAttribute) attributes[0] : null;
        }
    }
}