using System;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Entities
{
    /// <summary>
    ///     PropertyName for select and other blocks, without @ symbol in the beginning
    ///     <example>PropertyName</example>
    /// </summary>
    public class OutputValueEntity : SqlBuilderEntity
    {
        public readonly string PropertyName;

        public OutputValueEntity(SqlColumn sqlColumn)
        {
            PropertyName = sqlColumn?.PropertyName ?? throw new ArgumentException(nameof(sqlColumn));
        }

        public OutputValueEntity(string propertyName)
        {
            PropertyName = propertyName;
        }

        public override bool Present(SqlOptions sqlOptions) => !string.IsNullOrEmpty(PropertyName);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var propertyName = PropertyName;
            return propertyName;
        }
    }
}