using System;
using System.Collections.Generic;
using System.Text;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders.Delete
{
    public class SqlDeleteBuilder<T>: SqlDeleteBuilderHelper<SqlDeleteBuilder<T>>
    {
        public SqlDeleteBuilder(SqlTable sqlTable) : base(sqlTable)
        {
        }
        public new SqlDeleteBuilder<T> Where(ChooseOperatorFunc @operator) => base.Where(RunDelegate(@operator));
        public new SqlDeleteBuilder<T> Returns(ChooseColumnsFunc sqlColumns) => base.Returns(RunDelegate(sqlColumns));

        public SqlDeleteBuilder() : base(t)
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
