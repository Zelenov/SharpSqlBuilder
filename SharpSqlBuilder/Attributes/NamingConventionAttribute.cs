using System;

namespace SharpSqlBuilder.Attributes
{
    public class NamingConventionAttribute : Attribute
    {
        public readonly NamingConvention NamingConvention;

        public NamingConventionAttribute(NamingConvention namingConvention)
        {
            NamingConvention = namingConvention;
        }
    }
}