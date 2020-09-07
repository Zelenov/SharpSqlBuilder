using System;
using System.ComponentModel.DataAnnotations.Schema;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Attributes
{
    public class ForeignKeyTypeAttribute : ForeignKeyAttribute
    {
        public ForeignKeyTypeAttribute(Type sqlTableType): base(sqlTableType?.GetTableName() ?? throw new ArgumentException(nameof(sqlTableType)))
        {
        }
    }
}