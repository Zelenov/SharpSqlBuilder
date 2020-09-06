using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    ///     Sql block with multiple entities
    /// </summary>
    /// <typeparam name="T">Sql entity</typeparam>
    public abstract class CollectionBlock<T> : SqlBuilderEntity where T : SqlBuilderEntity
    {
        public readonly IList<T> Entities = new List<T>();

        protected CollectionBlock(params T[] operators) : this((IEnumerable<T>) operators)
        {
        }

        protected CollectionBlock(IEnumerable<T> operators)
        {
            AddRange(operators);
        }

        protected CollectionBlock()
        {
        }

        public override bool Present(SqlOptions sqlOptions)
        {
            return Entities.Any(e => e.Present(sqlOptions));
        }

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities ?? throw new ArgumentException(nameof(entities)))
                Add(entity);
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentException(nameof(entity));

            Entities.Add(entity);
        }
    }
}