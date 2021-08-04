using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharpSqlBuilder.Attributes;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Tests.Common;
using NUnit.Framework;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Tests
{
    [TestFixture]
    public class TestAutoJoin : BaseTest
    {
        [Table("class1", Schema = "foo")]
        public class Class1
        {
            [Column("key")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }
        }



        [Table("class2", Schema = "foo")]
        public class Class2
        {
            [Key] [Column("key")] public Guid Key { get; set; }
        }

        [Table("class3", Schema = "foo")]
        [NamingConvention(NamingConvention.SnakeCase)]
        public class Class3
        {
            [Key]
            [Column("key1")]
            [ForeignKeyType(typeof(Class1))]
            public Guid Class1Id { get; set; }

            [ForeignKeyType(typeof(Class2))]
            [Column("key2")]
            [Key]
            public Guid Class2Id { get; set; }
        }

        [Table("class4", Schema = "foo")]
        public class Class4
        {
            [Column("key1")]
            [ForeignKeyType(typeof(Class1))]
            public Guid Id { get; set; }
        }

        [Test]
        public void Select_JoinViaFk1()
        {
            var table1 = new SqlTable<Class1>();
            var table4 = new SqlTable<Class4>();
            var sqlBuilder = SqlBuilder.Select.Star().From(table1).InnerJoin(table4);
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
                *
            FROM foo.class1
            INNER JOIN foo.class4 ON class4.key1 = class1.key
            ";
            Check(expected, actual);
        }

        [Test]
        public void Select_JoinViaFk2()
        {
            var table1 = new SqlTable<Class1>();
            var table4 = new SqlTable<Class4>();
            var sqlBuilder = SqlBuilder.Select.Star().From(table4).InnerJoin(table1);
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
                *
            FROM foo.class4
            INNER JOIN foo.class1 ON class1.key = class4.key1
            ";
            Check(expected, actual);
        }

        [Test]
        public void Select_JoinViaThirdTable()
        {
            var table1 = new SqlTable<Class1>();
            var table2 = new SqlTable<Class2>();
            var table3 = new SqlTable<Class3>();

            var sqlBuilder = SqlBuilder.Select.Star().From(table1).InnerJoin(table3).InnerJoin(table2);
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
                *
            FROM foo.class1
            INNER JOIN foo.class3 ON class3.key1 = class1.key
            INNER JOIN foo.class2 ON class2.key = class3.key2
            ";
            Check(expected, actual);

        }

    }
}