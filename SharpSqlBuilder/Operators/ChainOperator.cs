using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    ///     Operator with multiple operands
    /// </summary>
    public abstract class ChainOperator : Operator
    {
        public readonly IList<Operator> Entities = new List<Operator>();

        protected ChainOperator(params Operator[] operators) : this((IEnumerable<Operator>) operators)
        {
        }

        protected ChainOperator(IEnumerable<Operator> operators)
        {
            AddRange(operators);
        }

        protected ChainOperator()
        {
        }

        public override bool Present(SqlOptions sqlOptions)
        {
            return Entities.Any(e => e.Present(sqlOptions));
        }

        public void AddRange(IEnumerable<Operator> entities)
        {
            foreach (var entity in entities ?? throw new ArgumentException(nameof(entities)))
                Add(entity);
        }

        public void Add(Operator entity)
        {
            if (entity == null)
                throw new ArgumentException(nameof(entity));

            Entities.Add(entity);
        }
    }
}