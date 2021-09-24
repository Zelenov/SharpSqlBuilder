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
    public class TestOrderMap : BaseTest
    {
        [Table("class1", Schema = "foo")]
        public class Class1
        {
            [Column("id")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }
            
            [Column("value1")] public Guid Value1 { get; set; }
        }

        [Test]
        public void Order_CaseSensitive_Success()
        { var table = new SqlTable<Class1>();

            var order = new[]
            {
                new NamedOrderItem{PropertyName = "Value1", Direction = OrderDirection.Desc},
                new NamedOrderItem{PropertyName = "Id", Direction = OrderDirection.Asc},
                null
            };
            var orderMap = OrderMap.Build(table, order, true, true);

            var sqlBuilder = SqlBuilder.Select.Order(orderMap);
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            ORDER BY
              class1.value1 DESC,
              class1.id ASC
            ";

            Check(expected, actual);
        }

         [Test]
        public void Order_CaseInsensitive_Success()
        { var table = new SqlTable<Class1>();

            var order = new[]
            {
                new NamedOrderItem{PropertyName = "value1", Direction = OrderDirection.Desc},
                new NamedOrderItem{PropertyName = "Id", Direction = OrderDirection.Asc},
                null
            };
            var orderMap = OrderMap.Build(table, order, true, false);

            var sqlBuilder = SqlBuilder.Select.Order(orderMap);
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            ORDER BY
              class1.value1 DESC,
              class1.id ASC
            ";

            Check(expected, actual);
        }
        

    }
}