using System;
using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    ///     Operator with multiple operands
    /// </summary>
    public abstract class ChainOperator : PriorityOperator
    {
        protected abstract string Command { get; }
        public bool NewLine { get; set; }
        public bool Indent { get; set; }

        public readonly IList<Operand> Entities = new List<Operand>();

        protected ChainOperator(params Operand[] operators) : this((IEnumerable<Operand>) operators)
        {
        }

        protected ChainOperator(IEnumerable<Operand> operators):this()
        {
            AddRange(operators);
        }

        protected ChainOperator()
        {
        }
        public virtual string GetSeparator(SqlOptions sqlOptions)
        {
            var r = $"{(NewLine ? sqlOptions.NewLine() : "")}{(Indent ? sqlOptions.Indent() : "")}";
            return string.IsNullOrEmpty(r) ? " " : r;
        }
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operators =
                Entities.Select(o => o.BuildSql(sqlOptions, FlowOptions.Construct(this)));
            var command = sqlOptions.Command(Command);
            var separator = GetSeparator(sqlOptions);
            var conditions = string.Join($"{separator}{command} ", operators);

            return $"{conditions}";
        }
        public override bool Present(SqlOptions sqlOptions)
        {
            return Entities.Any(e => e.Present(sqlOptions));
        }

        public void AddRange(IEnumerable<Operand> entities)
        {
            foreach (var entity in entities ?? throw new ArgumentException(nameof(entities)))
                Add(entity);
        }

        public void Add(Operand entity)
        {
            if (entity == null)
                throw new ArgumentException(nameof(entity));

            Entities.Add(entity);
        }
    }
}