using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SharpSqlBuilder.Attributes;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Maps
{
    public class PropertySqlColumn : SqlColumn
    {
        public PropertySqlColumn(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentException(nameof(propertyInfo));

            var columnAttr = propertyInfo.GetAttribute<ColumnAttribute>();
            var column = columnAttr?.Name ?? propertyInfo.Name;

            ColumnName = column;
            PropertyName = propertyInfo.Name;
            PropertyInfo = propertyInfo;


            var keyAttr = propertyInfo.GetAttribute<KeyAttribute>();
            var databaseGeneratedAttributeAttr = propertyInfo.GetAttribute<DatabaseGeneratedAttribute>();

            var isKey = keyAttr != null;
            var isDbIdentity = databaseGeneratedAttributeAttr?.DatabaseGeneratedOption ==
                DatabaseGeneratedOption.Identity;


            if (isKey || isDbIdentity)
                Key();

            var isDbAuto = databaseGeneratedAttributeAttr != null;
            if (isDbAuto)
                Auto();

            var ignoreUpdateAttribute = propertyInfo.GetAttribute<IgnoreUpdateAttribute>();
            var ignoreUpdate = ignoreUpdateAttribute != null;
            if (ignoreUpdate)
                DoNotUpdate();

            var foreignKeyAttribute = propertyInfo.GetAttribute<ForeignKeyAttribute>();
            if (foreignKeyAttribute!=null)
                WithForeignKey(foreignKeyAttribute.Name);
        }


        public PropertyInfo PropertyInfo { get; set; }
    }
}