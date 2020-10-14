using System.Linq;

namespace SharpSqlBuilder.Extensions
{
    public static class SqlOptionsExtensions
    {
        public static string Command(this SqlOptions sqlOptions, string cmd)
        {
            if (sqlOptions.UpperCaseCommands)
                return cmd.ToUpperInvariant();

            return cmd.ToLowerInvariant();
        }

        public static string TableName(this SqlOptions sqlOptions, string tableName) => tableName;

        public static string ColumnName(this SqlOptions sqlOptions, string columnName) => columnName;

        public static string SchemaName(this SqlOptions sqlOptions, string schemaName) => schemaName;

        public static string NewLine(this SqlOptions sqlOptions)
        {
            if (sqlOptions.OneLine)
                return " ";

            return sqlOptions.NewLineSymbol;
        }

        public static string Indent(this SqlOptions sqlOptions, string additionalIndent = null)
        {
            if (sqlOptions.OneLine)
                return "";

            return string.Join("",  Enumerable.Repeat(sqlOptions.IndentSymbol, sqlOptions.Tabs + 1)) + additionalIndent;
        }
    }
}