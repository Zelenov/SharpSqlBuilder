using System;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;
// ReSharper disable InconsistentNaming

namespace SharpSqlBuilder.Builders
{
    public sealed class SqlSelectBuilder<T1, T2> : SqlSelectBuilderHelper<SqlSelectBuilder<T1, T2>>
    {
        public delegate SqlTable ChooseTableFunc(SqlTable<T1> table1, SqlTable<T2> table2);
        public delegate SqlColumn ChooseColumnFunc(SqlTable<T1> table1, SqlTable<T2> table2);
        public delegate Operator ChooseOperatorFunc(SqlTable<T1> table1, SqlTable<T2> table2);
        public delegate Operator ChooseFilteredOperatorFunc<TFilter>(SqlFilter<TFilter> filter, SqlTable<T1> table1, SqlTable<T2> table2);
        public delegate Operator ChooseFilteredOperatorFunc(SqlFilter filter, SqlTable<T1> table1, SqlTable<T2> table2);

        public SqlSelectBuilder<T1, T2> From(ChooseTableFunc chooseTable) => base.From(RunDelegate(chooseTable));
        public SqlSelectBuilder<T1, T2> Join(JoinType joinType, ChooseTableFunc chooseTable, ChooseOperatorFunc on = null) => base.Join(joinType,RunDelegate(chooseTable), RunDelegate(on, nameof(on)));
        public SqlSelectBuilder<T1, T2> InnerJoin(ChooseTableFunc chooseTable, ChooseOperatorFunc on = null) => base.InnerJoin(RunDelegate(chooseTable), RunDelegate(on, nameof(on)));
        public SqlSelectBuilder<T1, T2> LeftJoin(ChooseTableFunc chooseTable, ChooseOperatorFunc on = null) => base.LeftJoin(RunDelegate(chooseTable), RunDelegate(on, nameof(on)));
        public SqlSelectBuilder<T1, T2> FullOuterJoin(ChooseTableFunc chooseTable, ChooseOperatorFunc on = null) => base.FullOuterJoin(RunDelegate(chooseTable), RunDelegate(on, nameof(on)));
        public SqlSelectBuilder<T1, T2> RightJoin(ChooseTableFunc chooseTable, ChooseOperatorFunc on = null) => base.RightJoin(RunDelegate(chooseTable), RunDelegate(on, nameof(on)));
        public SqlSelectBuilder<T1, T2> Where(ChooseOperatorFunc @operator) => base.Where(RunDelegate(@operator));
        public SqlSelectBuilder<T1, T2> Where<TFilter>(SqlFilter<TFilter> filter, ChooseFilteredOperatorFunc<TFilter> @operator) => base.Where(RunDelegate(filter, @operator));
        public SqlSelectBuilder<T1, T2> Where(SqlFilter filter, ChooseFilteredOperatorFunc @operator) => base.Where(RunDelegate(filter, @operator));
        public SqlSelectBuilder<T1, T2> OrderBy(ChooseColumnFunc sqlColumn, OrderDirection direction) => base.OrderBy(RunDelegate(sqlColumn), direction);

        public SqlSelectBuilder()
        {
            Values(t1, t2);
        }

        private SqlTable RunDelegate(ChooseTableFunc chooseTable, string argumentName = null)
        {
            return chooseTable?.Invoke(t1, t2) ?? throw new ArgumentException(argumentName ?? nameof(chooseTable));
        }
        private Operator RunDelegate(ChooseOperatorFunc @operator, string argumentName = null)
        {
            return @operator?.Invoke(t1, t2) ?? throw new ArgumentException(argumentName ?? nameof(@operator));
        }
        private SqlColumn RunDelegate(ChooseColumnFunc sqlColumn, string argumentName = null)
        {
            return sqlColumn?.Invoke(t1, t2) ?? throw new ArgumentException(argumentName ?? nameof(sqlColumn));
        }
        private Operator RunDelegate<TFilter>(SqlFilter<TFilter> sqlFilter, ChooseFilteredOperatorFunc<TFilter> @operator, string argumentName = null)
        {
            return @operator?.Invoke(sqlFilter, t1, t2) ?? throw new ArgumentException(argumentName ?? nameof(@operator));
        }
        private Operator RunDelegate(SqlFilter sqlFilter, ChooseFilteredOperatorFunc @operator, string argumentName = null)
        {
            return @operator?.Invoke(sqlFilter, t1, t2) ?? throw new ArgumentException(argumentName ?? nameof(@operator));
        }
#pragma warning disable IDE1006 // Naming Styles
        private static SqlTable<T1> t1 => SqlTable.ForType<T1>();
        private static SqlTable<T2> t2 => SqlTable.ForType<T2>();
#pragma warning restore IDE1006 // Naming Styles
    }

}