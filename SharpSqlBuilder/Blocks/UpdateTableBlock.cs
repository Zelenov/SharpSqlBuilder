using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>UPDATE ...</example>
    /// </summary>
    public class UpdateTableBlock : SqlBuilderEntity
    {
        public readonly TableEntity TableName;

        public UpdateTableBlock(SqlTable sqlTable)
        {
            TableName = new TableEntity(sqlTable ?? throw new ArgumentException(nameof(sqlTable)));
        }

        public override bool Present(SqlOptions sqlOptions) => TableName.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var command = sqlOptions.Command("UPDATE");
            var tableName = TableName.BuildSql(sqlOptions, FlowOptions.Construct(this));
            return $"{command} {tableName}";
        }
    }
}