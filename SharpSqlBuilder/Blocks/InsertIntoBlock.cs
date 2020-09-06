using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>INSERT INTO ...</example>
    /// </summary>
    public class InsertIntoBlock : SqlBuilderEntity
    {
        public readonly TableEntity Entity;

        public InsertIntoBlock(TableEntity entity)
        {
            Entity = entity ?? throw new ArgumentException(nameof(entity));
        }

        public override bool Present(SqlOptions sqlOptions) => Entity.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var tableName = Entity.BuildSql(sqlOptions);
            var command = sqlOptions.Command("INSERT INTO");
            return $"{command} {tableName}";
        }
    }
}