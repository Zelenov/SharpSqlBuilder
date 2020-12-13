using CaseExtensions;

namespace SharpSqlBuilder.Extensions
{
    internal static class StringExtension
    {
        public static string ToNamingConvention(this string str, NamingConvention namingConvention)
        {
            switch (namingConvention)
            {
                case NamingConvention.PascalCase: return str.ToPascalCase();
                case NamingConvention.CamelCase: return str.ToCamelCase();
                case NamingConvention.KebabCase: return str.ToKebabCase();
                case NamingConvention.SnakeCase: return str.ToSnakeCase();
                case NamingConvention.TrainCase: return str.ToTrainCase();
                default: return str;
            }
        }
    }
}