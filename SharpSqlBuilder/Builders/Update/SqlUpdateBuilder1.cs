using System;
using System.Collections.Generic;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;
// ReSharper disable InconsistentNaming

namespace SharpSqlBuilder.Builders
{
    public class SqlUpdateBuilder<T>: SqlUpdateBuilderHelper<SqlUpdateBuilder<T>>
    {
        public SqlUpdateBuilder<T> Values(ChooseColumnsFunc sqlColumns) => base.Values(RunDelegate(sqlColumns));
        public SqlUpdateBuilder<T> Where(ChooseOperatorFunc @operator) => base.Where(RunDelegate(@operator));
        public SqlUpdateBuilder<T> Returns(ChooseColumnsFunc sqlColumns) => base.Returns(RunDelegate(sqlColumns));
        public SqlUpdateBuilder() : base(t)
        {
        }

        public delegate SqlColumn ChooseColumnFunc(SqlTable<T> table);
        public delegate Operator ChooseOperatorFunc(SqlTable<T> table);
        public delegate IEnumerable<SqlColumn> ChooseColumnsFunc(SqlTable<T> table);
        private SqlColumn RunDelegate(ChooseColumnFunc sqlColumn, string argumentName = null)
        {
            return sqlColumn?.Invoke(t) ?? throw new ArgumentException(argumentName ?? nameof(sqlColumn));
        }
        private IEnumerable<SqlColumn> RunDelegate(ChooseColumnsFunc sqlColumns, string argumentName = null)
        {
            return sqlColumns?.Invoke(t) ?? throw new ArgumentException(argumentName ?? nameof(sqlColumns));
        }
        private Operator RunDelegate(ChooseOperatorFunc @operator, string argumentName = null)
        {
            return @operator?.Invoke(t) ?? throw new ArgumentException(argumentName ?? nameof(@operator));
        }

#pragma warning disable IDE1006 // Naming Styles
        private static SqlTable<T> t => SqlTable.ForType<T>();
#pragma warning restore IDE1006 // Naming Styles

    }
}
