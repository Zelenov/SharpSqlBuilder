﻿using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... IS NOT NULL</example>
    /// </summary>
    public class NotNullOperator : UnaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 10;
                default: return 4;
            }
        }
        public NotNullOperator(Operand operand) : base(operand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command("IS NULL");
            return $"{operand} {command}";
        }
    }
}