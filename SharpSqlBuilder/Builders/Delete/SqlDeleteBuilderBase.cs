﻿using System;
using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Blocks;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders
{
    public class SqlDeleteBuilderBase : SqlBuilderEntity
    {
        protected readonly Dictionary<SqlDeletePosition, CustomSqlBlock> CustomBlocks =
            new Dictionary<SqlDeletePosition, CustomSqlBlock>
            {
                {SqlDeletePosition.Start, new CustomSqlBlock()},
                {SqlDeletePosition.From, new CustomSqlBlock()},
                {SqlDeletePosition.Where, new CustomSqlBlock()},
                {SqlDeletePosition.Return, new CustomSqlBlock()}
            };

        protected readonly SqlTable SqlTable;
        protected readonly DeleteFromBlock DeleteFromBlock;
        protected readonly ReturningBlock ReturningsBlock = new ReturningBlock();
        protected readonly WhereBlock WhereBlock = new WhereBlock();
        protected SqlDeletePosition CurrentPosition = SqlDeletePosition.Start;

        protected SqlDeleteBuilderBase(SqlTable sqlTable)
        {
            SqlTable = sqlTable;
            DeleteFromBlock = new DeleteFromBlock(sqlTable ?? throw new ArgumentException(nameof(sqlTable)));
            CurrentPosition = SqlDeletePosition.From;
        }

        public override bool Present(SqlOptions sqlOptions) => DeleteFromBlock?.Present(sqlOptions) == true;

        protected SqlDeleteBuilderBase CustomSql(string customSql, SqlDeletePosition? type = null)
        {
            var customSelectType = type ?? CurrentPosition;
            var block = CustomBlocks[customSelectType];
            block.Add(new SqlCustomSqlBlock(customSql));
            return this;
        }


        protected SqlDeleteBuilderBase Where(params Operator[] operators) => Where((IEnumerable<Operator>)operators);

        protected SqlDeleteBuilderBase Where(IEnumerable<Operator> operators)
        {
            WhereBlock.AddRange(operators);
            CurrentPosition = SqlDeletePosition.Where;
            return this;
        }

        protected SqlDeleteBuilderBase WhereKeysEquals()
        {
            return Where(SqlTable.Keys.Select(m => m.Property().EqualsOne(m.Column())));
        }

        protected SqlDeleteBuilderBase ReturnsAutoGeneratedKeys() => Returns(SqlTable.AutoGeneratedColumns);

        protected SqlDeleteBuilderBase ReturnsAllValues() => Returns(SqlTable);

        protected SqlDeleteBuilderBase Returns(IEnumerable<SqlColumn> sqlColumns)
        {
            ReturningsBlock.AddRange(sqlColumns.Select(m => new ColumnAsPropertyEntity(m)));
            CurrentPosition = SqlDeletePosition.Return;
            return this;
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            if (sqlOptions == null)
                throw new ArgumentException(nameof(sqlOptions));

            var updatedSqlOptions =((SqlOptions)sqlOptions.Clone()).WithoutTableNames();

            CheckBeforeBuild(updatedSqlOptions);
            IEnumerable<SqlBuilderEntity> data = new SqlBuilderEntity[]
            {
                CustomBlocks[SqlDeletePosition.Start],
                DeleteFromBlock,
                CustomBlocks[SqlDeletePosition.From],
                WhereBlock,
                CustomBlocks[SqlDeletePosition.Where],
                ReturningsBlock,
                CustomBlocks[SqlDeletePosition.Return]
            };
            var commands = data.Where(b => CheckBlock(b, updatedSqlOptions)).Select(b => b.BuildSql(updatedSqlOptions, FlowOptions.Construct(this)));
            return string.Join(sqlOptions.NewLine(), commands);
        }
    }
}