using System;
using System.Linq.Expressions;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Maps
{
    public class PropertyMap
    {
        public readonly Type ModelType;
        public readonly string PropertyName;

        public PropertyMap(Type modelType, string propertyName)
        {
            PropertyName = propertyName;
            ModelType = modelType;
        }

        public static PropertyMap<TDestination> FromProperty<TDestination>(
            Expression<Func<TDestination, object>> propertyExpression) =>
            new PropertyMap<TDestination>(propertyExpression);
    }

    public class PropertyMap<TDestination> : PropertyMap
    {
        public PropertyMap(Expression<Func<TDestination, object>> propertyExpression) : base(typeof(TDestination),
            propertyExpression.GetPropertyInfo().Name)
        {
        }
    }
}