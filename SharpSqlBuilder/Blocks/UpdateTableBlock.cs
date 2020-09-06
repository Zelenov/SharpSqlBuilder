using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>UPDATE ...</example>
    /// </summary>
    public class UpdateTableBlock : SqlBuilderEntity
    {
        public readonly TableEntity TableName;

        public UpdateTableBlock(DbMap dbMap)
        {
            TableName = new TableEntity(dbMap ?? throw new ArgumentException(nameof(dbMap)));
        }

        public override bool Present(SqlOptions sqlOptions) => TableName.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var command = sqlOptions.Command("UPDATE");
            var tableName = TableName.BuildSql(sqlOptions);
            return $"{command} {tableName}";
        }
    }
}