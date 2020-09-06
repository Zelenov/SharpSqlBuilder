using System;
using SberPrint.Tests.Api.Common;

namespace SharpSqlBuilder.Tests.Common
{
    public static class Diff
    {
        public static void Text(string expected, string actual, Func<string, string> normalize)
        {
            var c1 = expected ?? "-";
            var c2 = actual ?? "-";
            var c1Normalized = normalize?.Invoke(c1) ?? c1;
            var c2Normalized = normalize?.Invoke(c2) ?? c2;
            var c0 = LineByLineOutput.DiffTexts(c1Normalized, c2Normalized);
            var columns = new[] {c0, c1, c2};
            var s = LineByLineOutput.CombineLineByLine(" ", columns);
            if (string.IsNullOrEmpty(s))
                s = "-";
            Console.WriteLine(s);
        }
    }
}