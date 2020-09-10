using System;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Maps
{
    public class SqlTableTyped : SqlTable
    {
        public readonly Type Type;
        public SqlTableTyped(Type type) : base(type.GetSchema(), type.GetTableName())
        {
            Type = type;
            AddRange(GetColumns(type));
        }
    }
}