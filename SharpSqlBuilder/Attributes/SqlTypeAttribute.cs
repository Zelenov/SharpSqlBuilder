using System;

namespace SharpSqlBuilder.Attributes
{
    public class SqlTypeAttribute : Attribute
    {
        public readonly string SqlType;

        public SqlTypeAttribute(string sqlType)
        {
            SqlType = sqlType;
        }
    }
}