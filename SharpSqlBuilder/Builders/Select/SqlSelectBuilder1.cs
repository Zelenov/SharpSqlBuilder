using System;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;
// ReSharper disable InconsistentNaming

namespace SharpSqlBuilder.Builders
{
    public sealed class SqlSelectBuilder<T> : SqlSelectBuilderHelper<SqlSelectBuilder<T>>
    {
        public delegate SqlTable ChooseTableFunc(SqlTable<T> table);
        public delegate SqlColumn ChooseColumnFunc(SqlTable<T> table);
        public delegate Operator ChooseOperatorFunc(SqlTable<T> table);

        public SqlSelectBuilder<T> Where(ChooseOperatorFunc @operator) => base.Where(RunDelegate(@operator));
        public SqlSelectBuilder<T> OrderBy(ChooseColumnFunc sqlColumn, OrderDirection direction) => base.OrderBy(RunDelegate(sqlColumn), direction);

        public SqlSelectBuilder()
        {
            Values(t);
            From(t);
        }

        private SqlTable RunDelegate(ChooseTableFunc chooseTable, string argumentName = null)
        {
            return chooseTable?.Invoke(t) ?? throw new ArgumentException(argumentName ?? nameof(chooseTable));
        }
        private Operator RunDelegate(ChooseOperatorFunc @operator, string argumentName = null)
        {
            return @operator?.Invoke(t) ?? throw new ArgumentException(argumentName ?? nameof(@operator));
        }
        private SqlColumn RunDelegate(ChooseColumnFunc sqlColumn, string argumentName = null)
        {
            return sqlColumn?.Invoke(t) ?? throw new ArgumentException(argumentName ?? nameof(sqlColumn));
        }

#pragma warning disable IDE1006 // Naming Styles
        private static SqlTable<T> t => SqlTable.ForType<T>();
#pragma warning restore IDE1006 // Naming Styles
    }

}