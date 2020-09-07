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
    public class SqlDeleteBuilder : SqlBuilderBase
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

        public SqlDeleteBuilder(SqlTable sqlTable)
        {
            SqlTable = sqlTable;
            DeleteFromBlock = new DeleteFromBlock(sqlTable ?? throw new ArgumentException(nameof(sqlTable)));
            CurrentPosition = SqlDeletePosition.From;
        }

        public override bool Present(SqlOptions sqlOptions) => DeleteFromBlock?.Present(sqlOptions) == true;

        public SqlDeleteBuilder CustomSql(string customSql, SqlDeletePosition? type = null)
        {
            var customSelectType = type ?? CurrentPosition;
            var block = CustomBlocks[customSelectType];
            block.Add(new SqlCustomSqlBlock(customSql));
            return this;
        }


        public SqlDeleteBuilder Where(params Operator[] operators) => Where((IEnumerable<Operator>) operators);

        public SqlDeleteBuilder Where(IEnumerable<Operator> operators)
        {
            WhereBlock.AddRange(operators);
            CurrentPosition = SqlDeletePosition.Where;
            return this;
        }

        public SqlDeleteBuilder WhereKeysEquals()
        {
            return Where(SqlTable.Keys.Select(m => m.Property().EqualsOne(m.Column())));
        }

        public SqlDeleteBuilder ReturnsAutoGeneratedKeys() => Returns(SqlTable.AutoGeneratedColumns);

        public SqlDeleteBuilder ReturnsAllValues() => Returns(SqlTable);

        public SqlDeleteBuilder Returns(IEnumerable<SqlColumn> sqlColumns)
        {
            ReturningsBlock.AddRange(sqlColumns.Select(m => new ColumnEntity(m)));
            CurrentPosition = SqlDeletePosition.Return;
            return this;
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            if (sqlOptions == null)
                throw new ArgumentException(nameof(sqlOptions));

            CheckBeforeBuild(sqlOptions);

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
            var commands = data.Where(b => CheckBlock(b, sqlOptions)).Select(b => b.BuildSql(sqlOptions));
            return string.Join(sqlOptions.NewLine(), commands);
        }
    }
}