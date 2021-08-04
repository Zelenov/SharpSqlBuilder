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
    public class TestOrder : BaseTest
    {
        [Table("class1", Schema = "class1")]
        [NamingConvention(NamingConvention.SnakeCase)]
        public class Class1
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int EndpointRouteId { get; set; }

            public short StatusId { get; set; }

            [Column("filter_date_from")] public DateTime? DateFrom { get; set; }

            [Column("filter_date_to")] public DateTime? DateTo { get; set; }

            [Column("filter_limit")] public long? Limit { get; set; }

            [Column("filter_skip")] public long? Skip { get; set; }

            public string Filter { get; set; }

            public string ConsumerLastElementId { get; set; }

            public string ProducerLastElementId { get; set; }

            public string ConsumerHash { get; set; }

            public string ProducerHash { get; set; }

            public short SyncStatusId { get; set; }

            public bool IsFailed { get; set; }

            [IgnoreUpdate] [Column("c_date")] public DateTime Created { get; set; }

            [IgnoreUpdate] [Column("c_user_id")] public int CreatedUserId { get; set; }

            [Column("h_date")] public DateTime Changed { get; set; }

            [Column("h_user_id")] public int ChangedUserId { get; set; }
        }
    



        [Test]
        public void Order1_Success()
        {
            var table1 = new SqlTable<Class1>();
            NamedOrderItem[] order = new[]{new NamedOrderItem {PropertyName = nameof(Class1.Id), Direction = OrderDirection.Desc}};
            var orderMap = OrderMap.Build(table1, order);
            var sqlBuilder = SqlBuilder.Select.Star().From(table1).Order(orderMap);
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
    }

}