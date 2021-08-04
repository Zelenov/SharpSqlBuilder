using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SharpSqlBuilder.Tests.Common;

namespace SharpSqlBuilder.Tests
{
    public class BaseTest
    {
        protected void Check(string expected, string actual)
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
