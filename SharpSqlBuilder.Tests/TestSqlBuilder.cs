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
    public class TestSqlBuilder
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

            [ForeignKeyType(typeof(Class3))]
            [Column("value1")] public Guid Value1 { get; set; }
            [ForeignKeyType(typeof(Class4))]
            [SqlType("json")]
            [Column("value2")] public string Value2 { get; set; }

            [Column("do_not_change")]
            [IgnoreUpdate]
            public DateTime DoNotChange { get; set; }
        }

        [Table("class2", Schema = "foo")]
        [NamingConvention(NamingConvention.SnakeCase)]
        public class Class2
        {
            [Key] public Guid Key { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int DbGeneratedKey { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public Guid DbGenerated { get; set; }
            public int Value1 { get; set; }
            public string Value2 { get; set; }
            public DateTime CodeGenerated { get; set; }

            [IgnoreUpdate]
            public DateTime DoNotChange { get; set; }
        }

        [Table("class3", Schema = "foo")]
        public class Class3
        {
            [Key] [Column("key")] public Guid Key { get; set; }

           
            [Column("value1")] 
            public int Value1 { get; set; }
        }
        [Table("class4", Schema = "foo")]
        public class Class4
        {
            [Key] [Column("key")] public string Key { get; set; }

           
            [Column("value1")] 
            public int Value1 { get; set; }
        }
        
        [Test]
        public void Delete_EqualsExpected()
        {
            var table = new SqlTable<Class1>();
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};
            var sqlBuilder = SqlBuilder.Delete.From(table)
               .CustomSql("/* DELETE FROM */")
               .WhereKeysEquals()
               .CustomSql("/* WHERE */").ReturnsAllValues()
               .CustomSql("/* RETURNING */" )
               .CustomSql("/* START */", SqlDeletePosition.Start);
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            DELETE FROM foo.class1
            /* DELETE FROM */
            WHERE
	            @Id = class1.id
            /* WHERE */
            RETURNING
	            id AS Id,
	            auto AS Auto,
	            value1 AS Value1,
	            value2 AS Value2,
	            do_not_change AS DoNotChange
            /* RETURNING */
            ";
            Check(expected, actual);
        }
        [Test]
        public void Delete_Generic_EqualsExpected()
        {
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};
            var sqlBuilder = SqlBuilder<Class1>.Delete.From()
               .CustomSql("/* DELETE FROM */")
               .WhereKeysEquals()
               .CustomSql("/* WHERE */")
               .ReturnsAllValues()
               .CustomSql("/* RETURNING */" )
               .CustomSql("/* START */", SqlDeletePosition.Start);
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            DELETE FROM foo.class1
            /* DELETE FROM */
            WHERE
	            @Id = class1.id
            /* WHERE */
            RETURNING
	            id AS Id,
	            auto AS Auto,
	            value1 AS Value1,
	            value2 AS Value2,
	            do_not_change AS DoNotChange
            /* RETURNING */
            ";
            Check(expected, actual);
        }

        [Test]
        public void Insert_EqualsExpected()
        {
            var table = new SqlTable<Class2>();
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};
            var sqlBuilder = SqlBuilder.Insert.Into(table)
               .CustomSql("/* INSERT INTO */")
               .AllStaticValues()
               .CustomSql("/* VALUES */")
               .OnKeysConflict()
               .CustomSql("/* ON CONFLICT */")
               .DoUpdateAllStaticColumns()
               .CustomSql("/* DO UPDATE SET */")
               .Where(table[m => m.CodeGenerated].Excluded().LessThan(table[m => m.CodeGenerated].Column()))
               .CustomSql("/* WHERE */")
               .ReturnsAutoGeneratedColumns()
               .CustomSql("/* RETURNING */")
               .CustomSql("/* START */", SqlInsertPosition.Start);
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            INSERT INTO foo.class2
            /* INSERT INTO */
            (key, value1, value2, code_generated, do_not_change)
            VALUES (
                @Key,
                @Value1,
                @Value2,
                @CodeGenerated,
                @DoNotChange
            )
            /* VALUES */
            ON CONFLICT (key, db_generated_key)
            /* ON CONFLICT */
            DO UPDATE SET
                db_generated = EXCLUDED.db_generated,
                value1 = EXCLUDED.value1,
                value2 = EXCLUDED.value2,
                code_generated = EXCLUDED.code_generated,
                do_not_change = EXCLUDED.do_not_change
            /* DO UPDATE SET */
            WHERE
                EXCLUDED.code_generated < class2.code_generated
            /* WHERE */
            RETURNING
	            db_generated_key AS DbGeneratedKey,
	            db_generated AS DbGenerated
            /* RETURNING */
            ";
            Check(expected, actual);
        }
        [Test]
        public void Insert_Generic_EqualsExpected()
        {
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};
            var sqlBuilder = SqlBuilder<Class2>.Insert.Into()
               .CustomSql("/* INSERT INTO */")
               .AllStaticValues()
               .CustomSql("/* VALUES */")
               .OnKeysConflict()
               .CustomSql("/* ON CONFLICT */")
               .DoUpdateAllStaticColumns()
               .CustomSql("/* DO UPDATE SET */")
               .Where(table=>table[m => m.CodeGenerated].Excluded().LessThan(table[m => m.CodeGenerated].Column()))
               .CustomSql("/* WHERE */")
               .ReturnsAutoGeneratedColumns()
               .CustomSql("/* RETURNING */")
               .CustomSql("/* START */", SqlInsertPosition.Start);
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            INSERT INTO foo.class2
            /* INSERT INTO */
            (key, value1, value2, code_generated, do_not_change)
            VALUES (
                @Key,
                @Value1,
                @Value2,
                @CodeGenerated,
                @DoNotChange
            )
            /* VALUES */
            ON CONFLICT (key, db_generated_key)
            /* ON CONFLICT */
            DO UPDATE SET
                db_generated = EXCLUDED.db_generated,
                value1 = EXCLUDED.value1,
                value2 = EXCLUDED.value2,
                code_generated = EXCLUDED.code_generated,
                do_not_change = EXCLUDED.do_not_change
            /* DO UPDATE SET */
            WHERE
                EXCLUDED.code_generated < class2.code_generated
            /* WHERE */
            RETURNING
	            db_generated_key AS DbGeneratedKey,
	            db_generated AS DbGenerated
            /* RETURNING */
            ";
            Check(expected, actual);
        }
        [Test]
        public void Insert_DoNothing_EqualsExpected()
        {
            var table = new SqlTable<Class2>();
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var sqlBuilder = SqlBuilder.Insert.Into(table)
               .AllStaticValues()
               .OnKeysConflict()
               .DoNothing();
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            INSERT INTO foo.class2
            (key, value1, value2, code_generated, do_not_change)
            VALUES (
                @Key,
                @Value1,
                @Value2,
                @CodeGenerated,
                @DoNotChange
            )
            ON CONFLICT (key, db_generated_key)
            DO NOTHING
            ";
            Check(expected, actual);
        } 
        [Test]
        public void Select_EqualsExpected()
        {
            var table = new SqlTable<Class1>();

            var order = new
            {
                Id = new OrderItem {Direction = OrderDirection.Asc, Index = 2},
                Value1 = new OrderItem {Direction = OrderDirection.Desc, Index = 1},
                DoNotChange = (OrderItem) null
            };
            var orderMap = OrderMap.FromOrder(table, order);
            var sqlFilter = SqlFilter.Construct(
                new {Ids = new[] {1, 2}, Value1 = "foo", Auto = (int?) null}
                );

            var whereSql = new[]
            {
                Conditions.Or(sqlFilter[f => f.Ids].IsNull(), table[m => m.Id].EqualsAny(sqlFilter[f => f.Ids])),
                Conditions.Or(sqlFilter[f => f.Value1].IsNull(),
                    table[m => m.Value1].EqualsOne(sqlFilter[f => f.Value1]))
            };
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};
            var sqlBuilder = SqlBuilder.Select
               .Values(table)
               .From(table)
               .Where(whereSql)
               .Order(orderMap)
               .Offset(5)
               .LimitBy(10);


            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
              class1.id AS Id,
              class1.auto AS Auto,
              class1.value1 AS Value1,
              class1.value2 AS Value2,
              class1.do_not_change AS DoNotChange
            FROM foo.class1
            WHERE
                   (@Ids IS NULL OR class1.id = ANY(@Ids))
               AND (@Value1 IS NULL OR class1.value1 = @Value1)
            ORDER BY
              class1.value1 DESC,
              class1.id ASC
            OFFSET 5
            LIMIT 10
            ";
            Check(expected, actual);
            Assert.AreEqual("Id", sqlBuilder.SplitOn);
        }
        [Test]
        public void Select_Generic_EqualsExpected()
        {
            var table = new SqlTable<Class1>();

            var order = new
            {
                Id = new OrderItem {Direction = OrderDirection.Asc, Index = 2},
                Value1 = new OrderItem {Direction = OrderDirection.Desc, Index = 1},
                DoNotChange = (OrderItem) null
            };
            var orderMap = OrderMap.FromOrder(table, order);
            var sqlFilter = SqlFilter.Construct(
                new {Ids = new[] {1, 2}, Value1 = "foo", Auto = (int?) null}
                );

            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};
            var sqlBuilder = SqlBuilder<Class1>.Select.Where(sqlFilter,(filter,t) =>
                    Conditions.And(
                        Conditions.Or(filter[f => f.Ids].IsNull(),
                            table[m => m.Id].EqualsAny(filter[f => f.Ids])),
                        Conditions.Or(filter[f => f.Value1].IsNull(),
                            table[m => m.Value1].EqualsOne(filter[f => f.Value1]))))
               .Order(orderMap)
               .Offset(5)
               .LimitBy(10);


            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
              class1.id AS Id,
              class1.auto AS Auto,
              class1.value1 AS Value1,
              class1.value2 AS Value2,
              class1.do_not_change AS DoNotChange
            FROM foo.class1
            WHERE
                   (@Ids IS NULL OR class1.id = ANY(@Ids))
               AND (@Value1 IS NULL OR class1.value1 = @Value1)
            ORDER BY
              class1.value1 DESC,
              class1.id ASC
            OFFSET 5
            LIMIT 10
            ";
            Check(expected, actual);
            Assert.AreEqual("Id", sqlBuilder.SplitOn);
        }

        [Test]
        public void SelectMultiple_EqualsExpected()
        {
            var table1 = new SqlTable<Class1>();
            var table2 = new SqlTable<Class2>();
            var sqlFilter = SqlFilter.Construct(new
            {
                Ids = new[] {1, 2},
                Value1 = "foo",
                Auto = (int?) null,
                Key = Guid.Empty,
                DbGeneratedKey = 2
            });
            var whereSql = new Operator[]
            {
                Conditions.Or(sqlFilter[f => f.Ids].IsNull(), table1[m => m.Id].EqualsAny(sqlFilter[f => f.Ids])),
                Conditions.Or(sqlFilter[f => f.Value1].IsNull(),
                    table1[m => m.Value1].EqualsOne(sqlFilter[f => f.Value1])),
                Conditions.Or(sqlFilter[f => f.Key].IsNull(), table2[m => m.Key].EqualsOne(sqlFilter[f => f.Key])),
                sqlFilter[f => f.DbGeneratedKey]
                   .IsNull()
                   .Or(table2[m => m.DbGeneratedKey].EqualsOne(sqlFilter[f => f.DbGeneratedKey])),

                table2[m => m.Key].ILike(sqlFilter[f => f.Key]),
                table2[m => m.Key].Like(sqlFilter[f => f.Key]),
                table2[m => m.Key].Lower().Like(sqlFilter[f => f.Key].Lower()),
            };

            var sqlBuilder = SqlBuilder.Select.Values(table1, table2)
               .CustomSql("/* SELECT */")
               .From(table1)
               .CustomSql("/* FROM */")
               .InnerJoin(table2, table2[m => m.Key].EqualsOne(table1[m => m.Id]))
               .CustomSql("/* JOIN */")
               .Where(whereSql)
               .CustomSql("/* WHERE */")
               .OrderBy(table1[m=>m.Id], OrderDirection.Asc)
               .CustomSql("/* ORDER */")
               .Offset(5)
               .CustomSql("/* OFFSET */")
               .LimitBy(10)
               .CustomSql("/* LIMIT */")
               .CustomSql("/* START */", SqlSelectPosition.Start);
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            SELECT
	            class1.id AS Id,
	            class1.auto AS Auto,
	            class1.value1 AS Value1,
	            class1.value2 AS Value2,
	            class1.do_not_change AS DoNotChange,
	            class2.key AS Key,
	            class2.db_generated_key AS DbGeneratedKey,
	            class2.db_generated AS DbGenerated,
	            class2.value1 AS Value1,
	            class2.value2 AS Value2,
	            class2.code_generated AS CodeGenerated,
	            class2.do_not_change AS DoNotChange
            /* SELECT */
            FROM foo.class1
            /* FROM */
            INNER JOIN foo.class2 ON class2.key = class1.id
            /* JOIN */
            WHERE
	                (@Ids IS NULL OR class1.id = ANY(@Ids))
	            AND (@Value1 IS NULL OR class1.value1 = @Value1)
	            AND (@Key IS NULL OR class2.key = @Key)
	            AND (@DbGeneratedKey IS NULL OR class2.db_generated_key = @DbGeneratedKey)
	            AND class2.key ILIKE @Key
	            AND class2.key LIKE @Key
	            AND LOWER(class2.key) LIKE LOWER(@Key)
            /* WHERE */
            ORDER BY
	            class1.id ASC
            /* ORDER */
            OFFSET 5
            /* OFFSET */
            LIMIT 10
            /* LIMIT */
            ";
            Check(expected, actual);
            Assert.AreEqual("Id,Key", sqlBuilder.SplitOn);
        }
        [Test]
        public void SelectMultiple_Generic_EqualsExpected()
        {
          
            var sqlFilter = SqlFilter.Construct(new
            {
                Ids = new[] {1, 2},
                Value1 = "foo",
                Auto = (int?) null,
                Key = Guid.Empty,
                DbGeneratedKey = 2
            });
            var sqlBuilder = SqlBuilder<Class1, Class2>.Select()
               .CustomSql("/* SELECT */")
               .From((table1, table2) => table1)
               .CustomSql("/* FROM */")
               .InnerJoin((table1, table2) => table2,
                    (table1, table2) => table2[m => m.Key].EqualsOne(table1[m => m.Id]))
               .CustomSql("/* JOIN */")
               .Where((table1, table2) => Conditions.And(
                    Conditions.Or(sqlFilter[f => f.Ids].IsNull(), table1[m => m.Id].EqualsAny(sqlFilter[f => f.Ids])),
                    Conditions.Or(sqlFilter[f => f.Value1].IsNull(),
                        table1[m => m.Value1].EqualsOne(sqlFilter[f => f.Value1])),
                    Conditions.Or(sqlFilter[f => f.Key].IsNull(), table2[m => m.Key].EqualsOne(sqlFilter[f => f.Key])),
                    sqlFilter[f => f.DbGeneratedKey]
                       .IsNull()
                       .Or(table2[m => m.DbGeneratedKey].EqualsOne(sqlFilter[f => f.DbGeneratedKey])),
                    table2[m => m.Key].ILike(sqlFilter[f => f.Key]), table2[m => m.Key].Like(sqlFilter[f => f.Key]),
                    table2[m => m.Key].Lower().Like(sqlFilter[f => f.Key].Lower())))
               .CustomSql("/* WHERE */")
               .OrderBy((table1, table2) => table1[m => m.Id], OrderDirection.Asc)
               .CustomSql("/* ORDER */")
               .Offset(5)
               .CustomSql("/* OFFSET */")
               .LimitBy(10)
               .CustomSql("/* LIMIT */")
               .CustomSql("/* START */", SqlSelectPosition.Start);
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            SELECT
	            class1.id AS Id,
	            class1.auto AS Auto,
	            class1.value1 AS Value1,
	            class1.value2 AS Value2,
	            class1.do_not_change AS DoNotChange,
	            class2.key AS Key,
	            class2.db_generated_key AS DbGeneratedKey,
	            class2.db_generated AS DbGenerated,
	            class2.value1 AS Value1,
	            class2.value2 AS Value2,
	            class2.code_generated AS CodeGenerated,
	            class2.do_not_change AS DoNotChange
            /* SELECT */
            FROM foo.class1
            /* FROM */
            INNER JOIN foo.class2 ON class2.key = class1.id
            /* JOIN */
            WHERE
	                (@Ids IS NULL OR class1.id = ANY(@Ids))
	            AND (@Value1 IS NULL OR class1.value1 = @Value1)
	            AND (@Key IS NULL OR class2.key = @Key)
	            AND (@DbGeneratedKey IS NULL OR class2.db_generated_key = @DbGeneratedKey)
	            AND class2.key ILIKE @Key
	            AND class2.key LIKE @Key
	            AND LOWER(class2.key) LIKE LOWER(@Key)
            /* WHERE */
            ORDER BY
	            class1.id ASC
            /* ORDER */
            OFFSET 5
            /* OFFSET */
            LIMIT 10
            /* LIMIT */
            ";
            Check(expected, actual);
            Assert.AreEqual("Id,Key", sqlBuilder.SplitOn);
        }
        [Test]
        public void Select_AutoJoin_EqualsExpected()
        {
            var table1 = new SqlTable<Class1>();
            var table3 = new SqlTable<Class3>();
            var table4 = new SqlTable<Class4>();
          
            var sqlBuilder = SqlBuilder.Select
               .Values(table1, table3, table4)
               .From(table1)
               .InnerJoin(table3)
               .LeftJoin(table4)
                ;
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
	            class1.id AS Id,
	            class1.auto AS Auto,
	            class1.value1 AS Value1,
	            class1.value2 AS Value2,
	            class1.do_not_change AS DoNotChange,
	            class3.key AS Key,
	            class3.value1 AS Value1,
	            class4.key AS Key,
	            class4.value1 AS Value1
            FROM foo.class1
            INNER JOIN foo.class3 ON class3.key = class1.value1
            LEFT JOIN foo.class4 ON class4.key = class1.value2
            ";
            Check(expected, actual);
        }

        [Test]
        public void Select_Exists_EqualsExpected()
        {
            var table1 = new SqlTable<Class1>();
            var table3 = new SqlTable<Class3>();

            var sqlBuilder = SqlBuilder.Select.Values(table1)
               .From(table1)
               .Where(SqlBuilder.Select.Star()
                   .From(table3)
                   .Where(table3[t => t.Key].EqualsOne(table1[t => t.Value1]))
                   .Exists());
            var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
	            class1.id AS Id,
	            class1.auto AS Auto,
	            class1.value1 AS Value1,
	            class1.value2 AS Value2,
	            class1.do_not_change AS DoNotChange
            FROM foo.class1
            WHERE
                EXISTS(SELECT * FROM foo.class3 WHERE class3.key = class1.value1)
            ";
            Check(expected, actual);
        }

        [Test]
        public void Update_Class1_EqualsExpected()
        {
            var table = new SqlTable<Class1>();
            var sqlBuilder = SqlBuilder.Update(table)
               .CustomSql("/* UPDATE */")
               .AllUpdatableValues()
               .CustomSql("/* SET */")
               .WhereKeysEquals()
               .CustomSql("/* WHERE */")
               .ReturnsAutoGeneratedKeys()
               .CustomSql("/* RETURNING */")
               .CustomSql("/* START */", SqlUpdatePosition.Start);
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            UPDATE foo.class1
            /* UPDATE */
            SET
	            value1 = @Value1,
	            value2 = CAST(@Value2 AS json)
            /* SET */
            WHERE
	            @Id = class1.id
            /* WHERE */
            RETURNING
	            id AS Id,
	            auto AS Auto
            /* RETURNING */
            ";
            Check(expected, actual);
        }
        [Test]
        public void Update_Generic_EqualsExpected()
        {
            var sqlBuilder = SqlBuilder<Class1>.Update()
               .CustomSql("/* UPDATE */")
               .AllUpdatableValues()
               .CustomSql("/* SET */")
               .WhereKeysEquals()
               .CustomSql("/* WHERE */")
               .ReturnsAutoGeneratedKeys()
               .CustomSql("/* RETURNING */")
               .CustomSql("/* START */", SqlUpdatePosition.Start);
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            /* START */
            UPDATE foo.class1
            /* UPDATE */
            SET
	            value1 = @Value1,
	            value2 = CAST(@Value2 AS json)
            /* SET */
            WHERE
	            @Id = class1.id
            /* WHERE */
            RETURNING
	            id AS Id,
	            auto AS Auto
            /* RETURNING */
            ";
            Check(expected, actual);
        }

        [Test]
        public void Priority1_EqualsExpected()
        {
            var a = Operand.From("A");
            var b = Operand.From("B");
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95, OneLine = true };
            var sqlBuilder = a.EqualsOne(b).Or(a.EqualsOne(b).Not().And(a.LessThan(b)));

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"A = B OR NOT A = B AND A < B";
            Check(expected, actual);
        }
        [Test]
        public void Priority2_EqualsExpected()
        {
            var a = Operand.From("A");
            var b = Operand.From("B");
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95, OneLine = true };
            var sqlBuilder = a.NotEquals(a.Or(b).And(a.EqualsOne(b)).Not());

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"A <> (NOT ((A OR B) AND A = B))";
            Check(expected, actual);
        }

        [Test]
        public void Eq1_EqualsExpected()
        {
            var a = Operand.From("A");
            var b = Operand.From("B");
            var c = Operand.From("C");
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95, OneLine = true };
            var sqlBuilder = a.IsTrue().Or(b.IsTrue()).EqualsOne(c);

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"(A = TRUE OR B = TRUE) = C";
            Check(expected, actual);
        }
        [Test]
        public void Eq2_EqualsExpected()
        {
            var a = Operand.From("A");
            var b = Operand.From("B");
            var c = Operand.From("C");
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95, OneLine = true };
            var sqlBuilder = c.EqualsOne( a.IsTrue().Or(b.IsTrue()));

            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"C = (A = TRUE OR B = TRUE)";
            Check(expected, actual);
        }
        [Test]
        public void SelectStar_EqualsExpected()
        {
            var table = new SqlTable<Class1>();
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var sqlBuilder = SqlBuilder.Select
               .Star()
               .From(table);


            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
              *
            FROM foo.class1
            ";
            Check(expected, actual);
            Assert.AreEqual("", sqlBuilder.SplitOn);
        }
        [Test]
        public void SelectCountStar_EqualsExpected()
        {
            var table = new SqlTable<Class1>();
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var sqlBuilder = SqlBuilder.Select
               .CountStar()
               .From(table);


            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
              COUNT(*)
            FROM foo.class1
            ";
            Check(expected, actual);
            Assert.AreEqual("", sqlBuilder.SplitOn);
        }
        [Test]
        public void SelectSum_EqualsExpected()
        {
            var table = new SqlTable<Class1>();
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var sqlBuilder = SqlBuilder.Select
               .Value(SqlBuilder.Select.Value(Operand.From(table[t=>t.Value2])).From(table).Sum())
               .From(table);


            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
	            SUM(SELECT
	                    class1.value2
                    FROM foo.class1)
            FROM foo.class1
            ";
            Check(expected, actual);
            Assert.AreEqual("", sqlBuilder.SplitOn);
        }
        [Test]
        public void OrderByEnumerable_EqualsExpected()
        {
            var table = new SqlTable<Class1>();

            var order = new[]
            {
                new NamedOrderItem{PropertyName = "Value1", Direction = OrderDirection.Desc},
                new NamedOrderItem{PropertyName = "Id", Direction = OrderDirection.Asc},
                null
            };
            var orderMap = OrderMap.Build(table, order, true);
            var sqlOptions = new SqlOptions { Dialect = SqlDialect.Postgres95 };
            var sqlBuilder = SqlBuilder.Select
               .Star()
               .From(table)
               .Order(orderMap);


            var actual = sqlBuilder.BuildSql(sqlOptions);
            var expected = @"
            SELECT
                *
            FROM foo.class1
            ORDER BY
              class1.value1 DESC,
              class1.id ASC
            ";
            Check(expected, actual);
        }


        private void Check(string expected, string actual)
        {
            Diff.Text(expected, actual, Sql.Normalize);
            Console.WriteLine("___________\nACTUAL\n");
            Console.WriteLine(actual);
            Console.WriteLine("___________\nEXPECTED\n");
            Console.WriteLine(expected);
            Assert.AreEqual(Sql.Normalize(expected), Sql.Normalize(actual));
        }
    }

}