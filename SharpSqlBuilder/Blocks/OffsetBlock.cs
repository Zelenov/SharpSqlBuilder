using System;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>OFFSET 10</example>
    /// </summary>
    public class OffsetBlock : SqlBuilderEntity
    {
        public Operand Offset;

        public OffsetBlock(Operand offset)
        {
            Offset = offset ?? throw new ArgumentException(nameof(offset));
        }

        public OffsetBlock(SqlFilterItem offset)
        {
            Offset = Operand.From(offset ?? throw new ArgumentException(nameof(offset)));
        }

        public OffsetBlock(string offset)
        {
            Offset = Operand.From(offset ?? throw new ArgumentException(nameof(offset)));
        }

        public override bool Present(SqlOptions sqlOptions) => Offset.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var offset = Offset?.BuildSql(sqlOptions);
            return $"{sqlOptions.Command("OFFSET")} {offset}";
        }
    }
}