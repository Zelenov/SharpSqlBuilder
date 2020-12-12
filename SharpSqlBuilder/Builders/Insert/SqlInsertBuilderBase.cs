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
    public class SqlInsertBuilderBase : SqlBuilderEntity
    {
        protected readonly OnConflictBlock ConflictUpdate = new OnConflictBlock();

        protected readonly Dictionary<SqlInsertPosition, CustomSqlBlock> CustomBlocks =
            new Dictionary<SqlInsertPosition, CustomSqlBlock>
            {
                {SqlInsertPosition.Start, new CustomSqlBlock()},
                {SqlInsertPosition.Into, new CustomSqlBlock()},
                {SqlInsertPosition.Values, new CustomSqlBlock()},
                {SqlInsertPosition.Conflict, new CustomSqlBlock()},
                {SqlInsertPosition.Do, new CustomSqlBlock()},
                {SqlInsertPosition.Where, new CustomSqlBlock()},
                {SqlInsertPosition.Return, new CustomSqlBlock()}
            };

        protected readonly SqlTable SqlTable;
        protected readonly DoUpdateBlock DoUpdateBlock = new DoUpdateBlock();
        protected DoNothingBlock DoNothingBlock;
        protected readonly InsertColumnsBlock InsertColumnsBlock = new InsertColumnsBlock();
        protected readonly InsertIntoBlock InsertIntoBlock;
        protected readonly InsertValuesBlock InsertValuesBlock = new InsertValuesBlock();
        protected readonly ReturningBlock ReturningBlock = new ReturningBlock();
        protected readonly WhereBlock WhereBlock = new WhereBlock();
        protected SqlInsertPosition CurrentPosition = SqlInsertPosition.Start;


        protected SqlInsertBuilderBase(SqlTable sqlTable)
        {
            SqlTable = sqlTable;
            InsertIntoBlock = new InsertIntoBlock(new TableEntity(sqlTable ?? throw new ArgumentException(nameof(sqlTable))));
            CurrentPosition = SqlInsertPosition.Into;
        }

        public override bool Present(SqlOptions sqlOptions) =>
            InsertIntoBlock?.Present(sqlOptions) == true && InsertColumnsBlock.Present(sqlOptions) &&
            InsertValuesBlock.Present(sqlOptions);


        protected SqlInsertBuilderBase Values(IEnumerable<SqlColumn> sqlColumns)
        {
            var mapItems = sqlColumns as SqlColumn[] ?? sqlColumns.ToArray();
            InsertColumnsBlock.AddRange(mapItems.Select(m => new ColumnEntity(m)));
            InsertValuesBlock.AddRange(mapItems.Select(m => m.Property()));
            CurrentPosition = SqlInsertPosition.Values;
            return this;
        }

        protected SqlInsertBuilderBase CustomSql(string customSql, SqlInsertPosition? type = null)
        {
            var customSelectType = type ?? CurrentPosition;
            var block = CustomBlocks[customSelectType];
            block.Add(new SqlCustomSqlBlock(customSql));
            return this;
        }

        protected SqlInsertBuilderBase OnConflict(IEnumerable<SqlColumn> sqlColumns)
        {
            ConflictUpdate.AddRange(sqlColumns.Select(m => new ColumnEntity(m)));
            CurrentPosition = SqlInsertPosition.Conflict;
            return this;
        }

        protected SqlInsertBuilderBase OnConflict(IEnumerable<string> sqlColumns)
        {
            ConflictUpdate.AddRange(sqlColumns.Select(m => new ColumnEntity(m)));
            CurrentPosition = SqlInsertPosition.Conflict;
            return this;
        }

        protected SqlInsertBuilderBase DoUpdate(IEnumerable<SqlColumn> sqlColumns)
        {
            DoUpdateBlock.AddRange(sqlColumns.Select(m => new OnConflictUpdateValueBlock(m)));
            CurrentPosition = SqlInsertPosition.Do;
            return this;
        }
        protected SqlInsertBuilderBase DoNothing()
        {
            DoNothingBlock = new DoNothingBlock();
            CurrentPosition = SqlInsertPosition.Do;
            return this;
        }

        protected SqlInsertBuilderBase Where(params Operator[] operators)
        {
            WhereBlock.AddRange(operators);
            CurrentPosition = SqlInsertPosition.Where;
            return this;
        }

        protected SqlInsertBuilderBase Returns(IEnumerable<SqlColumn> sqlColumns)
        {
            ReturningBlock.AddRange(sqlColumns.Select(m => new ColumnAsPropertyEntity(m)));
            CurrentPosition = SqlInsertPosition.Return;
            return this;
        }

        protected SqlInsertBuilderBase AllStaticValues() => Values(SqlTable.StaticColumns);

        protected SqlInsertBuilderBase OnKeysConflict() => OnConflict(SqlTable.Keys);

        protected SqlInsertBuilderBase DoUpdateAllStaticColumns() => DoUpdate(SqlTable.NotKeys);

        protected SqlInsertBuilderBase ReturnsAutoGeneratedColumns() => Returns(SqlTable.AutoGeneratedColumns);

        protected SqlInsertBuilderBase ReturnsAllValues() => Returns(SqlTable);


        public override string BuildSql(SqlOptions sqlOptions)
        {
            if (sqlOptions == null)
                throw new ArgumentException(nameof(sqlOptions));

            CheckBeforeBuild(sqlOptions);
            IEnumerable<SqlBuilderEntity> data = new SqlBuilderEntity[]
            {
                CustomBlocks[SqlInsertPosition.Start],
                InsertIntoBlock,
                CustomBlocks[SqlInsertPosition.Into],
                InsertColumnsBlock,
                InsertValuesBlock,
                CustomBlocks[SqlInsertPosition.Values],
                ConflictUpdate,
                CustomBlocks[SqlInsertPosition.Conflict],
                DoUpdateBlock,
                DoNothingBlock,
                CustomBlocks[SqlInsertPosition.Do],
                WhereBlock,
                CustomBlocks[SqlInsertPosition.Where],
                ReturningBlock,
                CustomBlocks[SqlInsertPosition.Return],
            };
            var commands = data.Where(b => CheckBlock(b, sqlOptions)).Select(b => b.BuildSql(sqlOptions, FlowOptions.Construct(this)));
            return string.Join(sqlOptions.NewLine(), commands);
        }
    }
}