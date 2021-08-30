using System;
using System.Collections.Generic;
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
    public class TestUpdate : BaseTest
    {
        [Table("class1", Schema = "foo")]
        public class Class1
        {
            [Column("id")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid Id { get; set; }

            [Column("auto")]
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public Guid Auto { get; set; }
            [Column("do_not_change")]
            [IgnoreUpdate]
            public DateTime DoNotChange { get; set; }
        }
        [Test]
        public void Update_ImplicitSets_EqualsExpected()
        {
            var filter = SqlFilter.Construct(new
            {
                Id = Guid.Parse("b07d1437-597c-47f5-8d13-453a85f0b681"),
                Auto = Guid.Parse("b07d1437-597c-47f5-8d13-453a85f0b681"),
                DoNotChange = (DateTime?) null
            });
            var table = new SqlTable<Class1>();
            var sqlBuilder = SqlBuilder.Update(table)
               .Values(new Dictionary<SqlColumn, IOperable>
                {
                    {table[t => t.Auto], filter[t => t.Auto].Coalesce(table[f => f.Auto])},
                    {table[t => t.DoNotChange], filter[t => t.DoNotChange].Coalesce(table[f => f.DoNotChange])}
                })
               .Where(table[t => t.Id].EqualsOne(filter[f => f.Id]));
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            UPDATE foo.class1
            SET
                auto = COALESCE(@Auto, class1.auto),
                do_not_change = COALESCE(@DoNotChange, class1.do_not_change)
            WHERE
                class1.id = @Id
            ";
            Check(expected, actual);
        }
    }

}