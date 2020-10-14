using System;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Extensions
{
    public static class DbMapItemExtensions
    {
        public static ExcludedOperand Excluded(this SqlColumn self) => new ExcludedOperand(self);

        public static SqlFilterOperand Property(this SqlColumn self) => new SqlFilterOperand(self);

        public static TableColumnOperand Column(this SqlColumn self) => new TableColumnOperand(self);

        public static EqualsOneOperator EqualsOne(this SqlColumn self, SqlColumn other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsOneOperator EqualsOne(this SqlColumn self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsOneOperator EqualsOne(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsOneOperator EqualsOne(this SqlColumn self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this SqlColumn self, SqlColumn other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this SqlColumn self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this SqlColumn self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this SqlColumn self, SqlColumn other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this SqlColumn self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this SqlColumn self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this SqlColumn self, SqlColumn other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this SqlColumn self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this SqlColumn self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this SqlColumn self, SqlColumn other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this SqlColumn self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this SqlColumn self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this SqlColumn self, SqlColumn other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this SqlColumn self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this SqlColumn self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static IsTrueOperator IsTrue(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsTrue();

        public static IsTrueOperator IsTrue(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsTrue();

        public static IsFalseOperator IsFalse(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsFalse();

        public static IsFalseOperator IsFalse(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsFalse();

        public static NotNullOperator NotNull(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).NotNull();

        public static NotNullOperator NotNull(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).NotNull();

        public static IsNullOperator IsNull(this SqlColumn self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsNull();

        public static IsNullOperator IsNull(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsNull();

        public static ILikeOperator ILike(this SqlColumn self, SqlColumn other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .ILike(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static ILikeOperator ILike(this SqlColumn self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .ILike(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static ILikeOperator ILike(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .ILike(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static ILikeOperator ILike(this SqlColumn self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .ILike(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

    }
}