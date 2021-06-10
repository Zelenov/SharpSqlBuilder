using System;
using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Blocks;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

// ReSharper disable once CheckNamespace
namespace SharpSqlBuilder.Builders
{
    public class SqlSelectBuilderBase : AggregatableEntity 
    {
        protected readonly Dictionary<SqlSelectPosition, CustomSqlBlock> CustomSqlBlocks =
            new Dictionary<SqlSelectPosition, CustomSqlBlock>
            {
                {SqlSelectPosition.Start, new CustomSqlBlock()},
                {SqlSelectPosition.Select, new CustomSqlBlock()},
                {SqlSelectPosition.From, new CustomSqlBlock()},
                {SqlSelectPosition.Join, new CustomSqlBlock()},
                {SqlSelectPosition.Where, new CustomSqlBlock()},
                {SqlSelectPosition.Order, new CustomSqlBlock()},
                {SqlSelectPosition.Limit, new CustomSqlBlock()},
                {SqlSelectPosition.Offset, new CustomSqlBlock()}
            };

        protected readonly FromTablesBlock FromTablesBlock = new FromTablesBlock();
        protected readonly JoinsBlock JoinsBlock = new JoinsBlock();
        protected readonly OrdersBlock OrdersBlock = new OrdersBlock();
        protected readonly SelectValuesBlock SelectValuesBlock = new SelectValuesBlock();
        protected readonly WhereBlock WhereBlock = new WhereBlock();
        protected SqlSelectPosition CurrentPosition = SqlSelectPosition.Start;
        protected LimitBlock LimitBlock;
        protected OffsetBlock OffsetBlock;
        protected readonly IList<SqlTable> Tables = new List<SqlTable>();
        protected readonly IList<SqlColumn> FirstSqlColumns = new List<SqlColumn>();

        /// <summary>
        /// Use for Dapper's splitOn param
        /// </summary>
        public string SplitOn => string.Join(",", FirstSqlColumns.Select(c => c.PropertyName).Where(c => c != null));

        public override bool Present(SqlOptions sqlOptions) =>
            SelectValuesBlock.Present(sqlOptions) && FromTablesBlock.Present(sqlOptions);

        protected SqlSelectBuilderBase()
        {
        }

        protected SqlSelectBuilderBase(SqlSelectBuilderBase copyFrom)
        {
            CustomSqlBlocks = copyFrom.CustomSqlBlocks;
            FromTablesBlock = copyFrom.FromTablesBlock;
            JoinsBlock = copyFrom.JoinsBlock;
            OrdersBlock = copyFrom.OrdersBlock;
            SelectValuesBlock = copyFrom.SelectValuesBlock;
            WhereBlock = copyFrom.WhereBlock;
            CurrentPosition = copyFrom.CurrentPosition;
            Tables = copyFrom.Tables;
            FirstSqlColumns = copyFrom.FirstSqlColumns;
        }

        protected SqlSelectBuilderBase Values(params SqlTable[] sqlTables)
        {
            foreach (var sqlTable in sqlTables ?? throw new ArgumentException(nameof(sqlTables)))
            {
                Values((IEnumerable<SqlColumn>)sqlTable);
            }
            return this;
        }

        protected SqlSelectBuilderBase Values(IEnumerable<SqlColumn> sqlColumns)
        {
            var columns = sqlColumns as SqlColumn[] ?? sqlColumns.ToArray();
            SelectValuesBlock.AddRange(columns?.Select(m => new SelectColumnBlock(m)) ??
                throw new ArgumentException(nameof(sqlColumns)));
            var firstColumn = columns.FirstOrDefault();
            if (firstColumn != null)
                FirstSqlColumns.Add(firstColumn);
            CurrentPosition = SqlSelectPosition.Select;
            return this;
        }
        protected SqlSelectBuilderBase Values(IEnumerable<Operand> sqlColumns)
        {
            foreach (var operand in sqlColumns ?? throw new ArgumentException(nameof(sqlColumns)))
            {
                Value(operand);
            }
            CurrentPosition = SqlSelectPosition.Select;
            return this;
        }
        protected SqlSelectBuilderBase Value(Operand operand)
        {
            SelectValuesBlock.Add(new SelectColumnBlock(operand ?? throw new ArgumentException(nameof(operand))));
            CurrentPosition = SqlSelectPosition.Select;
            return this;
        }

        protected SqlSelectBuilderBase Star()
        {
            SelectValuesBlock.Add(new SelectStarBlock());
            CurrentPosition = SqlSelectPosition.Select;
            return this;
        }
        protected SqlSelectBuilderBase CountStar()
        {
            return Value(new CountStarAggregate());
        }

        protected SqlSelectBuilderBase CustomSql(string customSql, SqlSelectPosition? type = null)
        {
            var customSelectType = type ?? CurrentPosition;
            var block = CustomSqlBlocks[customSelectType];
            block.Add(new SqlCustomSqlBlock(customSql));
            return this;
        }

        protected SqlSelectBuilderBase From(params SqlTable[] sqlTables)
        {
            FromTablesBlock.AddRange(sqlTables.Select(sqlTable => new TableEntity(sqlTable)));
            foreach (var sqlTable in sqlTables)
            {
                Tables.Add(sqlTable);
            }
            CurrentPosition = SqlSelectPosition.From;
            return this;
        }

        protected SqlSelectBuilderBase Join(JoinType joinType, SqlTable sqlTable, Operator on = null)
        {
            if (on == null)
                on = AutoJoin(sqlTable);
            Tables.Add(sqlTable);
            JoinsBlock.Add(new JoinBlock(joinType, sqlTable, on));
            CurrentPosition = SqlSelectPosition.Join;
            return this;
        }

        private Operator AutoJoin(SqlTable sqlTable)
        {
            bool swap;
            SqlTable pkTable;
            var fkTables = Tables.Where(t => t.ForeignKeys.Any(column => column.ForeignKey == sqlTable.TableName))
               .ToArray();
            var fkTable = fkTables.FirstOrDefault(t => t.Schema == sqlTable.Schema) ?? fkTables.FirstOrDefault();
            if (fkTable != null)
            {
                pkTable = sqlTable;
                swap = false;
            }
            else
            {
                var pkTables = Tables.Where(table =>
                    sqlTable.ForeignKeys.Any(column => column.ForeignKey == table.TableName)).ToArray();
                pkTable = pkTables.FirstOrDefault(t => t.Schema == sqlTable.Schema) ?? pkTables.FirstOrDefault();
                if (pkTable == null)
                    throw new ArgumentException($"Failed to find foreign key for joining table {sqlTable.TableName}");

                fkTable = sqlTable;
                swap = true;
            }

            var join = AutoJoin(pkTable, fkTable);
            SqlColumn a;
            SqlColumn b;
            if (!swap)
            {
                a = join.Key;
                b = join.Value;
            }
            else
            {
                b = join.Key;
                a = join.Value;
            }

            return a.EqualsOne(b);
        }
        private KeyValuePair<SqlColumn, SqlColumn> AutoJoin(SqlTable pkTable, SqlTable fkTable)
        {
            var fks = fkTable.ForeignKeys.Where(fk => fk.ForeignKey == pkTable.TableName).ToArray();
            if (fks.Length > 1)
                throw new ArgumentException(
                    $"Table {fkTable.TableName} has more than one foreign key to table {pkTable.TableName}");

            var foreignKey = fks[0];
            var keys = pkTable.Keys.ToArray();
            if (keys.Length == 0)
                throw new ArgumentException($"Table {pkTable.TableName} has no key");

            if (keys.Length > 1)
                throw new ArgumentException($"Table {pkTable.TableName} has more than one key");

            var key = keys[0];
            return new KeyValuePair<SqlColumn,SqlColumn>(key, foreignKey);
        }
       


        protected SqlSelectBuilderBase InnerJoin(SqlTable sqlTable, Operator on = null)
        {
            return Join(JoinType.Inner, sqlTable, on);
        }
        protected SqlSelectBuilderBase LeftJoin(SqlTable sqlTable, Operator on = null)
        {
            return Join(JoinType.Left, sqlTable, on);
        }

        protected SqlSelectBuilderBase FullOuterJoin(SqlTable sqlTable, Operator on = null)
        {
            return Join(JoinType.FullOuter, sqlTable, on);
        }

        protected SqlSelectBuilderBase RightJoin(SqlTable sqlTable, Operator on = null)
        {
            return Join(JoinType.Right, sqlTable, on);
        }

        protected SqlSelectBuilderBase Where(params Operator[] operators)
        {
            WhereBlock.AddRange(operators);
            CurrentPosition = SqlSelectPosition.Where;
            return this;
        }

        protected SqlSelectBuilderBase Order(OrderMap orderMap)
        {
            OrdersBlock.AddRange(orderMap);
            CurrentPosition = SqlSelectPosition.Order;
            return this;
        }

        protected SqlSelectBuilderBase OrderBy(SqlColumn sqlColumn, OrderDirection direction)
        {
            OrdersBlock.Add(new OrderBlock(sqlColumn, direction));
            CurrentPosition = SqlSelectPosition.Order;
            return this;
        }

        protected SqlSelectBuilderBase OrderBy(IEnumerable<OrderBlock> orderBy)
        {
            OrdersBlock.AddRange(orderBy);
            CurrentPosition = SqlSelectPosition.Order;
            return this;
        }

        protected SqlSelectBuilderBase LimitBy(Operand limitBy)
        {
            LimitBlock = new LimitBlock(limitBy);
            CurrentPosition = SqlSelectPosition.Limit;
            return this;
        }

        protected SqlSelectBuilderBase LimitBy(long? limitBy) => LimitBy(Operand.From(limitBy?.ToString()));

        protected SqlSelectBuilderBase LimitBy(SqlFilterItem limitBy) => LimitBy(Operand.From(limitBy));

        protected SqlSelectBuilderBase Offset(Operand offset)
        {
            OffsetBlock = new OffsetBlock(offset);
            CurrentPosition = SqlSelectPosition.Offset;
            return this;
        }

        protected SqlSelectBuilderBase Offset(SqlFilterItem offset) => Offset(Operand.From(offset));

        protected SqlSelectBuilderBase Offset(long? offset) => Offset(Operand.From(offset?.ToString()));

        public override string BuildSql(SqlOptions sqlOptions)
        {
            if (sqlOptions == null)
                throw new ArgumentException(nameof(sqlOptions));

            CheckBeforeBuild(sqlOptions);
            IEnumerable<SqlBuilderEntity> data = new SqlBuilderEntity[]
            {
                CustomSqlBlocks[SqlSelectPosition.Start],
                SelectValuesBlock,
                CustomSqlBlocks[SqlSelectPosition.Select],
                FromTablesBlock,
                CustomSqlBlocks[SqlSelectPosition.From],
                JoinsBlock,
                CustomSqlBlocks[SqlSelectPosition.Join],
                WhereBlock,
                CustomSqlBlocks[SqlSelectPosition.Where],
                OrdersBlock,
                CustomSqlBlocks[SqlSelectPosition.Order],
                OffsetBlock,
                CustomSqlBlocks[SqlSelectPosition.Offset],
                LimitBlock,
                CustomSqlBlocks[SqlSelectPosition.Limit]
            };
            var commands = data.Where(b => CheckBlock(b, sqlOptions)).Select(b => b.BuildSql(sqlOptions, FlowOptions.Construct(this)));
            return string.Join(sqlOptions.NewLine(), commands);
        }
    }
}