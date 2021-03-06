﻿using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = FALSE</example>
    /// </summary>
    public class IsFalseOperator : UnaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 17;
                default: return 4;
            }
        }
        public IsFalseOperator(Operand operand) : base(operand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = new FalseOperand().BuildSql(sqlOptions, FlowOptions.Construct(this));
            return $"{operand} = {command}";
        }

    }
}