using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>DELETE FROM ...</example>
    /// </summary>
    public class DeleteFromBlock : SqlBuilderEntity
    {
        public readonly TableEntity TableEntity;

        public DeleteFromBlock(DbMap dbMap) : this(new TableEntity(dbMap ?? throw new ArgumentException(nameof(dbMap))))
        {
        }

        public DeleteFromBlock(TableEntity tableEntity)
        {
            TableEntity = tableEntity ?? throw new ArgumentException(nameof(tableEntity));
        }

        public override bool Present(SqlOptions sqlOptions) => TableEntity.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var table = TableEntity.BuildSql(sqlOptions);
            var command = sqlOptions.Command("DELETE FROM");
            return $"{command} {table}";
        }
    }
}