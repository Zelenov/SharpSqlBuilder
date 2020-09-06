using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using SharpSqlBuilder.Attributes;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Maps
{
    public class PropertyDbMapItem : DbMapItem
    {
        public PropertyDbMapItem(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentException(nameof(propertyInfo));

            var columnAttr = propertyInfo.GetAttribute<ColumnAttribute>();
            var column = columnAttr?.Name;

            ColumnName = column ?? throw new ArgumentException($"Property has no {nameof(ColumnAttribute)}");
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
        }

        public PropertyInfo PropertyInfo { get; set; }
    }
}