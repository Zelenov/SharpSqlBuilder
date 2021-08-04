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
        public TableEntity(SqlTable sqlTable)
        {
            if (sqlTable == null)
                throw new ArgumentException(nameof(sqlTable));

            Schema = sqlTable.Schema;
            TableName = sqlTable.TableName;
        }

        public string Schema { get; set; }
        public string TableName { get; set; }

        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var schema = sqlOptions?.SchemaName(Schema);
            var tableName = sqlOptions.TableName(TableName);
            if (schema == null || !sqlOptions.IncludeSchemaName)
                return tableName;

            return $"{schema}.{tableName}";
        }
    }
}