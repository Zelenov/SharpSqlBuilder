﻿using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = ANY (...)</example>
    /// </summary>
    public class EqualsAnyOperator : BinaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 12;
                default: return 7;
            }
        }
        public EqualsAnyOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var left = LeftOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var right = RightOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.Postgres:
                    return $"{left} = {sqlOptions.Command("ANY")}({right})";
                default:
                    return $"{left} {sqlOptions.Command("IN")} ({right})";
            }
        }
    }
}