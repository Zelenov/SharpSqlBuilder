using System;
using System.Linq;
using System.Text;

namespace SharpSqlBuilder.Tests.Common
{
    public static class LineByLineOutput
    {
        public static string DiffTexts(params string[] columns)
        {
            var splits = columns
               .Select(c => c.ToString().Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries))
               .ToArray();
            var mx = splits.Max(s => s.Length);
            var sb = new StringBuilder();

            for (var j = 0; j < mx; j++)
            {
                var diff = splits.Any(s => s.Length <= j);

                if (!diff)
                {
                    var a = splits.First()[j];
                    for (var i = splits.Length - 1; i >= 1; i--)
                    {
                        var b = splits[i][j];
                        if (b != a)
                        {
                            diff = true;
                            break;
                        }
                    }
                }

                sb.AppendLine(diff ? ">" : " ");
            }

            return sb.ToString();
        }

        public static string CombineLineByLine(string delimeter, params string[] columns)
        {
            if (columns == null || columns.Length == 0)
                return null;

            var o = new object[columns.Length * 2 - 1];
            for (var i = 0; i < o.Length; i++)
                if (i % 2 == 0)
                    o[i] = columns[i / 2];
                else
                    o[i] = delimeter;

            var splits = o.Select(c => c.ToString().Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries))
               .ToArray();
            var maxes = splits.Select(s => s.Max(str => str.Length)).ToArray();
            var mx = splits.Max(s => s.Length);
            var sb = new StringBuilder();
            for (var j = 0; j < mx; j++)
            {
                for (var i = 0; i < splits.Length; i++)
                    if (splits[i].Length <= j)
                        sb.Append(new string(Enumerable.Repeat(' ', maxes[i]).ToArray()));
                    else
                        sb.Append(splits[i][j].PadRight(maxes[i], ' '));
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}