using System;
using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Blocks;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders
{
    public class SqlSelectBuilder : SqlBuilderBase
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

        public override bool Present(SqlOptions sqlOptions) =>
            SelectValuesBlock.Present(sqlOptions) && FromTablesBlock.Present(sqlOptions);

        public SqlSelectBuilder Values(params DbMap[] dbMaps)
        {
            return Values(dbMaps?.SelectMany(dbMap => dbMap)) ?? throw new ArgumentException(nameof(dbMaps));
        }

        public SqlSelectBuilder Values(IEnumerable<DbMapItem> dbMapItems)
        {
            SelectValuesBlock.AddRange(dbMapItems?.Select(m => new SelectColumnBlock(m)) ??
                throw new ArgumentException(nameof(dbMapItems)));
            CurrentPosition = SqlSelectPosition.Select;
            return this;
        }

        public SqlSelectBuilder CustomSql(string customSql, SqlSelectPosition? type = null)
        {
            var customSelectType = type ?? CurrentPosition;
            var block = CustomSqlBlocks[customSelectType];
            block.Add(new SqlCustomSqlBlock(customSql));
            return this;
        }

        public SqlSelectBuilder From(params DbMap[] dbMaps)
        {
            FromTablesBlock.AddRange(dbMaps.Select(dbMap => new TableEntity(dbMap)));
            CurrentPosition = SqlSelectPosition.From;
            return this;
        }

        public SqlSelectBuilder InnerJoin(DbMap dbMap, Operator on = null)
        {
            JoinsBlock.Add(new JoinBlock(JoinType.Inner, dbMap, on));
            CurrentPosition = SqlSelectPosition.Join;
            return this;
        }


        public SqlSelectBuilder LeftJoin(DbMap dbMap, Operator on)
        {
            JoinsBlock.Add(new JoinBlock(JoinType.Left, dbMap, on));
            CurrentPosition = SqlSelectPosition.Join;
            return this;
        }

        public SqlSelectBuilder FullOuterJoin(DbMap dbMap, Operator on)
        {
            JoinsBlock.Add(new JoinBlock(JoinType.FullOuter, dbMap, on));
            CurrentPosition = SqlSelectPosition.Join;
            return this;
        }

        public SqlSelectBuilder RightJoin(DbMap dbMap, Operator on)
        {
            JoinsBlock.Add(new JoinBlock(JoinType.Right, dbMap, on));
            CurrentPosition = SqlSelectPosition.Join;
            return this;
        }

        public SqlSelectBuilder Where(params Operator[] operators)
        {
            WhereBlock.AddRange(operators);
            CurrentPosition = SqlSelectPosition.Where;
            return this;
        }

        public SqlSelectBuilder Order(OrderMap orderMap, object order)
        {
            OrdersBlock.AddRange(orderMap, order);
            CurrentPosition = SqlSelectPosition.Order;
            return this;
        }

        public SqlSelectBuilder OrderBy(OrderBlock orderBy)
        {
            OrdersBlock.Add(orderBy);
            CurrentPosition = SqlSelectPosition.Order;
            return this;
        }

        public SqlSelectBuilder OrderBy(IEnumerable<OrderBlock> orderBy)
        {
            OrdersBlock.AddRange(orderBy);
            CurrentPosition = SqlSelectPosition.Order;
            return this;
        }

        public SqlSelectBuilder LimitBy(Operand limitBy)
        {
            LimitBlock = new LimitBlock(limitBy);
            CurrentPosition = SqlSelectPosition.Limit;
            return this;
        }

        public SqlSelectBuilder LimitBy(long? limitBy) => LimitBy(Operand.From(limitBy?.ToString()));

        public SqlSelectBuilder LimitBy(SqlFilterItem limitBy) => LimitBy(Operand.From(limitBy));

        public SqlSelectBuilder Offset(Operand offset)
        {
            OffsetBlock = new OffsetBlock(offset);
            CurrentPosition = SqlSelectPosition.Offset;
            return this;
        }

        public SqlSelectBuilder Offset(SqlFilterItem offset) => Offset(Operand.From(offset));

        public SqlSelectBuilder Offset(long? offset) => Offset(Operand.From(offset?.ToString()));

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
            var commands = data.Where(b => CheckBlock(b, sqlOptions)).Select(b => b.BuildSql(sqlOptions));
            return string.Join(sqlOptions.NewLine(), commands);
        }
    }
}