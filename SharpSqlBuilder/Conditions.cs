using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder
{
    public static class Conditions
    {
        public static AndOperator And(params Operator[] operators) => new AndOperator(operators);

        public static OrOperator Or(params Operator[] operators) => new OrOperator(operators);
    }
}