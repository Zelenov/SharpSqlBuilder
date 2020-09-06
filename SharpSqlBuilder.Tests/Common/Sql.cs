using System.Text.RegularExpressions;

namespace SharpSqlBuilder.Tests.Common
{
    public static class Sql
    {
        private static readonly Regex SqlNormalizeRegex = new Regex(@"^\s+", RegexOptions.Multiline);

        public static string Normalize(string sql) => SqlNormalizeRegex.Replace(sql.Trim(), "");
    }
}