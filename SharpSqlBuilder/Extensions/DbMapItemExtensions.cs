using System;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Extensions
{
    public static class DbMapItemExtensions
    {
        public static ExcludedOperand Excluded(this DbMapItem self) => new ExcludedOperand(self);

        public static TableColumnOperand Property(this DbMapItem self) => new TableColumnOperand(self);

        public static TableColumnOperand Column(this DbMapItem self) => new TableColumnOperand(self);

        public static EqualsOneOperator EqualsOne(this DbMapItem self, DbMapItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsOneOperator EqualsOne(this DbMapItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsOneOperator EqualsOne(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsOneOperator EqualsOne(this DbMapItem self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsOne(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this DbMapItem self, DbMapItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this DbMapItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static EqualsAnyOperator EqualsAny(this DbMapItem self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .EqualsAny(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this DbMapItem self, DbMapItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this DbMapItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessThanOperator LessThan(this DbMapItem self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this DbMapItem self, DbMapItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this DbMapItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static LessOrEqualToOperator LessOrEqualTo(this DbMapItem self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .LessOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this DbMapItem self, DbMapItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this DbMapItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreThanOperator MoreThan(this DbMapItem self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreThan(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this DbMapItem self, DbMapItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this DbMapItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this SqlFilterItem self, SqlFilterItem other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static MoreOrEqualToOperator MoreOrEqualTo(this DbMapItem self, string other) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self)))
               .MoreOrEqualTo(Operand.From(other) ?? throw new ArgumentException(nameof(self)));

        public static IsTrueOperator IsTrue(this DbMapItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsTrue();

        public static IsTrueOperator IsTrue(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsTrue();

        public static IsFalseOperator IsFalse(this DbMapItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsFalse();

        public static IsFalseOperator IsFalse(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsFalse();

        public static NotNullOperator NotNull(this DbMapItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).NotNull();

        public static NotNullOperator NotNull(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).NotNull();

        public static IsNullOperator IsNull(this DbMapItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsNull();

        public static IsNullOperator IsNull(this SqlFilterItem self) =>
            Operand.From(self ?? throw new ArgumentException(nameof(self))).IsNull();
    }
}