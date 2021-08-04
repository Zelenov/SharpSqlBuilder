using System;
using SharpSqlBuilder.Attributes;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder
{
    public class SqlOptions : ICloneable
    {
        public static SqlOptions Default = new SqlOptions();
        public SqlDatabaseType DatabaseType => Dialect.GetAttribute<SqlDatabaseAttribute>().SqlDatabaseType;
        public SqlDialect Dialect { get; set; } = SqlDialect.Postgres95;
        public bool UpperCaseCommands { get; set; } = true;
        public bool OneLine { get; set; } = false;
        public string IndentSymbol { get; set; } = "\t";
        public string NewLineSymbol { get; set; } = Environment.NewLine;
        public int Tabs { get; set; }
        public bool IncludeTableNames { get; set; } = true;
        public bool IncludeSchemaName { get; set; } = true;
        public SqlOptions Inlined()
        {
            OneLine = true;
            return this;
        }
        public SqlOptions IncTabs()
        {
            Tabs+=1;
            return this;
        }

        public object Clone() =>
            new SqlOptions
            {
                Dialect = this.Dialect,
                UpperCaseCommands = this.UpperCaseCommands,
                OneLine = this.OneLine,
                IndentSymbol = this.IndentSymbol,
                NewLineSymbol = this.NewLineSymbol,
                IncludeTableNames = this.IncludeTableNames,
                IncludeSchemaName = this.IncludeSchemaName,
            };

        public SqlOptions WithoutTableNames()
        {
            IncludeTableNames = false;
            return this;
        }
        public SqlOptions WithoutSchemaName()
        {
            IncludeSchemaName = true;
            return this;
        }
    }
}