# SharpSqlBuilder
Multi-dialect sql builder with advanced mapping and custom extensions for Database-First applications.

SharpSqlBuilder takes your models and maps them to SQL text queries.

# Getting started
## Setting up models
Populate your database entitiy classes
```csharp
[Table("table1", Schema = "foo")]
public class Class1
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("auto")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Guid Auto { get; set; }

    [ForeignKeyType(typeof(Class3))]
    [Column("value1")]
    public Guid Value1 { get; set; }
    [Column("do_not_change")]
    [IgnoreUpdate]
    public DateTime DoNotChange { get; set; }
}
```

Following attributes are supported
### TableAttribute
*System.ComponentModel.DataAnnotations.Schema.TableAttribute*
Sets the table name for your class. If not set, name of the class would be used as-is. 
```csharp
[Table("table1", Schema = "foo")]
public class Class1
{ ...
```
### ColumnAttribute
*System.ComponentModel.DataAnnotations.Schema.ColumnAttribute*
Sets the table's column name for property. If not set, property's name would be used as-is. 
```csharp
[Column("id")]
public int Id { get; set; }
```
### DatabaseGenerated
*System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated*
* **DatabaseGeneratedOption.Identity** — Sets the property as auto-generated key. Such properties would be ignored on INSERT AND UPDATE, but would be using for auto-joining and ON CONFLICT blocks
```csharp
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int Id { get; set; }
```
* **DatabaseGeneratedOption.Computed** — Sets the property as auto-generated static property. Such properties would be ignored on INSERT only.
```csharp
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int Id { get; set; }
```

## Set up SqlTables

The main part of **SharpSqlBuilder** is **SqlTable**. **SqlTable** stores all the mapping information for all the columns of the table.

Use the following methods for the entity classes:

```csharp
 var table = SqlTable.ForType<Class1>();
```
This method is thread-safe and caches **SqlTable** for all the types. If you need just a plain class without any caching use
```csharp
 var table = new SqlTable<Class1>();
```

For custom setup use manual constructor and populate columns by hand
```csharp
var table = new  SqlTable(schema: "public", tableName: "table1");
table.Add(new SqlColumn{
	IsKey = true,
	IsUpdatable = true,
	IsAutoGenerated = true,
	ColumnName = "id",
	PropertyName = "Id",
});
```

## Construct SqlOptions
```csharp
var options = SqlOptions
{
    Dialect = SqlDialect.Postgres95,
    UpperCaseCommands = true,
    OneLine = false,
    IndentSymbol = "\t",
    NewLineSymbol = "\r\n"
}
```
## Start building queries

Consider the config
```csharp
var sqlOptions = new SqlOptions {Dialect = SqlDialect.Postgres95};
```
and these classes configured
```csharp
var sqlFilter = SqlFilter.Construct(
	new {Ids = new[] {1, 2}, Value1 = "foo", Auto = (int?) null}
);

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
    [Column("value2")] public string Value2 { get; set; }
    [Column("do_not_change")]
    [IgnoreUpdate]
    public DateTime DoNotChange { get; set; }
}

[Table("class2", Schema = "foo")]
public class Class2
{
    [Key] [Column("key")] public Guid Key { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("key_db_generated")]
    public int DbGeneratedKey { get; set; }
    [Column("db_generated")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public Guid DbGenerated { get; set; }
    [Column("value1")] 
    public int Value1 { get; set; }
    [Column("value2")] public string Value2 { get; set; }
    [Column("code_generated")] public DateTime CodeGenerated { get; set; }
    [Column("do_not_change")]
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

```

### SELECT
```csharp
var whereConditions = new []{
	sqlFilter[f => f.Ids].IsNull()		.Or(table1[m => m.Id].EqualsAny(sqlFilter[f => f.Ids])),
	sqlFilter[f => f.Value1].IsNull()	.Or(table1[m => m.Value1].EqualsOne(sqlFilter[f => f.Value1])),
	sqlFilter[f => f.Key].IsNull()		.Or(table2[m => m.Key].EqualsOne(sqlFilter[f => f.Key])),
};

var sqlBuilder = SqlBuilder.Select
   .Values(table1, table2)
   .From(table1)
   .InnerJoin(table2, table2[m => m.Key].EqualsOne(table1[m => m.Id]))
   .Where(whereConditions)
   .OrderBy(table1[m=>m.Id], OrderDirection.Asc)
   .Offset(5)
   .LimitBy(10);

Console.WriteLine(sqlBuilder.BuildSql(sqlOptions));
```
Builds the following SQL
```sql
SELECT
    class1.id AS Id,
    class1.auto AS Auto,
    class1.value1 AS Value1,
    class1.value2 AS Value2,
    class1.do_not_change AS DoNotChange,
    class2.key AS Key,
    class2.key_db_generated AS DbGeneratedKey,
    class2.db_generated AS DbGenerated,
    class2.value1 AS Value1,
    class2.value2 AS Value2,
    class2.code_generated AS CodeGenerated,
    class2.do_not_change AS DoNotChange
FROM foo.class1
INNER JOIN foo.class2 ON class2.key = class1.id
WHERE
         (@Ids IS NULL OR class1.id = ANY(@Ids))
     AND (@Value1 IS NULL OR class1.value1 = @Value1)
     AND (@Key IS NULL OR class2.key = @Key)
ORDER BY
    class1.id ASC
OFFSET 5
LIMIT 10
```
## INSERT
```csharp
var sqlBuilder = SqlBuilder.Insert.Into(SqlTable.FromType<Class2>())
   .AllStaticValues()
   .ReturnsAutoGeneratedColumns();

Console.WriteLine(sqlBuilder.BuildSql(sqlOptions));
```
Builds the following SQL
```sql
INSERT INTO foo.class2
(key, value1, value2, code_generated, do_not_change)
VALUES (
    @Key,
    @Value1,
    @Value2,
    @CodeGenerated,
    @DoNotChange
)
WHERE
    (EXCLUDED.code_generated < class2.code_generated)
RETURNING key_db_generated, db_generated
```
## UPSERT
```csharp
var sqlBuilder = SqlBuilder.Insert.Into(SqlTable.FromType<Class2>())
   .AllStaticValues()
   .OnKeysConflict()
   .DoUpdateAllStaticColumns()
   .Where(table[m => m.CodeGenerated].Excluded().LessThan(table[m => m.CodeGenerated].Column()))
   .ReturnsAutoGeneratedColumns();

Console.WriteLine(sqlBuilder.BuildSql(sqlOptions));
```
Builds the following SQL
```sql
INSERT INTO foo.class2
(key, value1, value2, code_generated, do_not_change)
VALUES (
    @Key,
    @Value1,
    @Value2,
    @CodeGenerated,
    @DoNotChange
)
ON CONFLICT (key, key_db_generated)
DO UPDATE SET
    db_generated = EXCLUDED.db_generated,
    value1 = EXCLUDED.value1,
    value2 = EXCLUDED.value2,
    code_generated = EXCLUDED.code_generated,
    do_not_change = EXCLUDED.do_not_change
WHERE
    (EXCLUDED.code_generated < class2.code_generated)
RETURNING key_db_generated, db_generated
```
## UPDATE
```csharp 
var sqlBuilder = SqlBuilder.Update(SqlTable.FromType<Class1>())
   .AllUpdatableValues()
   .WhereKeysEquals()
   .ReturnsAutoGeneratedKeys();

Console.WriteLine(sqlBuilder.BuildSql(sqlOptions));
```
Builds the following SQL
```sql
UPDATE foo.class1
SET
  value1 = @Value1,
  value2 = @Value2
WHERE
  (@Id = class1.id)
RETURNING id, auto
```

## DELETE
```csharp  
var sqlBuilder = SqlBuilder.Delete.From(SqlTable.FromType<Class1>())
   .WhereKeysEquals();

Console.WriteLine(sqlBuilder.BuildSql(sqlOptions));
```
Builds the following SQL
```sql
DELETE FROM foo.class1
WHERE
  (@Id = class1.id)
RETURNING id, auto, value1, value2, do_not_change
```