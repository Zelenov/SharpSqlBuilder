using System;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Entities
{
    /// <summary>
    ///     Represents Schema + Table pair. Schema can be null.
    ///     <example>schema.table</example>
    /// </summary>
    public class TableEntity : SqlBuilderEntity
    {
        public TableEntity(DbMap dbMap)
        {
            if (dbMap == null)
                throw new ArgumentException(nameof(dbMap));

            Schema = dbMap.Schema;
            TableName = dbMap.TableName;
        }

        public string Schema { get; set; }
        public string TableName { get; set; }

        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var schema = sqlOptions?.SchemaName(Schema);
            var tableName = sqlOptions.TableName(TableName);
            if (schema == null)
                return tableName;

            return $"{schema}.{tableName}";
        }
    }
}