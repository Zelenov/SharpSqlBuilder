using System;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>LIMIT 10</example>
    /// </summary>
    public class LimitBlock : SqlBuilderEntity
    {
        public readonly Operand LimitBy;

        public LimitBlock(Operand limitBy)
        {
            LimitBy = limitBy ?? throw new ArgumentException(nameof(limitBy));
        }

        public override bool Present(SqlOptions sqlOptions) => LimitBy.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            string command;
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.MsSql:

                    command = sqlOptions.Command("FETCH NEXT");
                    break;
                default:
                    command = sqlOptions.Command("LIMIT");
                    break;
            }

            var limitBy = LimitBy?.BuildSql(sqlOptions);
            return $"{command} {limitBy}";
        }
    }
}