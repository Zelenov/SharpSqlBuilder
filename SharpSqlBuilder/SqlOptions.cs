using SharpSqlBuilder.Attributes;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder
{
    public class SqlOptions
    {
        public static SqlOptions Default = new SqlOptions();
        public SqlDatabase Database => Dialect.GetAttribute<SqlDatabaseAttribute>().SqlDatabase;
        public SqlDialect Dialect { get; set; } = SqlDialect.Postgres95;
        public bool UpperCaseCommands { get; set; } = true;
        public bool OneLine { get; set; } = false;
        public string IndentSymbol { get; set; } = "\t";
        public string NewLineSymbol { get; set; } = "\r\n";
    }
}