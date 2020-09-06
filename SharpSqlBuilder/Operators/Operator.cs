using System.Linq;

namespace SharpSqlBuilder.Operators
{
    public abstract class Operator : SqlBuilderEntity
    {
        public OrOperator Or(params Operator[] operators)
        {
            return new OrOperator(new[] {this}.Concat(operators));
        }

        public AndOperator And(params Operator[] operators)
        {
            return new AndOperator(new[] {this}.Concat(operators));
        }
    }
}