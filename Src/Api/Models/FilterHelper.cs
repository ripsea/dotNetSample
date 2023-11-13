using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Api.Models
{
    public static class FilterHelper
    {

        public static Expression<Func<T, bool>> CreateMultiEqualWithPropertyInfoExpression<T>(
            this IQueryable<T> iqueryables,
            T filters)
        {
            var param = Expression.Parameter(typeof(T), "p");

            Expression? body = null;
            Type t = filters.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo property in properties)
            {

                Type propType = property.PropertyType;
                if (property == null)
                {
                    continue;
                }

                var propValue = property.GetValue(filters);
                var propKey = property.Name;

                var member = Expression.Property(param, propKey);

                var propertyType = ((PropertyInfo)member.Member).PropertyType;
                var converter = TypeDescriptor.GetConverter(propertyType); // 1
                if (!converter.CanConvertFrom(property.PropertyType)) // 2
                    throw new NotSupportedException();

                var propertyValue = converter.ConvertFromInvariantString((string)propValue); // 3
                var constant = Expression.Constant(propertyValue);
                var valueExpression = Expression.Convert(constant, propertyType); // 4

                var expression = Expression.Equal(member, valueExpression);
                body = body == null ? expression : Expression.AndAlso(body, expression);
            }
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        //wait to do...
        //https://www.c-sharpcorner.com/article/handling-complex-api-filter-queries-in-asp-net-core/
        public static Expression<Func<T, bool>> CreateMultiEqualExpression<T>(
            this IQueryable<T> iqueryables,
            IDictionary<string, object> filters)
        {
            var param = Expression.Parameter(typeof(T), "p");
            Expression? body = null;
            foreach (var pair in filters)
            {
                var member = Expression.Property(param, pair.Key);

                var propertyType = ((PropertyInfo)member.Member).PropertyType;
                var converter = TypeDescriptor.GetConverter(propertyType); // 1
                if (!converter.CanConvertFrom(typeof(string))) // 2
                    throw new NotSupportedException();

                var propertyValue = converter.ConvertFromInvariantString((string)pair.Value); // 3
                var constant = Expression.Constant(propertyValue);
                var valueExpression = Expression.Convert(constant, propertyType); // 4

                var expression = Expression.Equal(member, valueExpression);
                body = body == null ? expression : Expression.AndAlso(body, expression);
            }
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> CreateEqualExpression<T>(
            this IQueryable<T> iqueryables, 
            string propertyName,
            object value)
        {
            var param = Expression.Parameter(typeof(T), "p");
            Expression? body = null;
            var member = Expression.Property(param, propertyName);
            var constant = Expression.Constant(value);
            var expression = Expression.Equal(member, constant);
            body = body == null ? expression : Expression.AndAlso(body, expression);

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> CreateNestedExpression<T>(
            this IQueryable<T> iqueryables,
            string propertyName, 
            object value)
        {
            var param = Expression.Parameter(typeof(T), "p");
            Expression member = param;
            foreach (var namePart in propertyName.Split('.'))
            {
                member = Expression.Property(member, namePart);
            }
            var constant = Expression.Constant(value);
            var body = Expression.Equal(member, constant);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> CreateBetweenExpression<T>(
            this IQueryable<T> iqueryables,
            string propertyName,
            object lowerValue,
            object upperValue
        )
        {
            var param = Expression.Parameter(typeof(T), "p");
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var body = Expression.AndAlso(
                Expression.GreaterThanOrEqual(member, Expression.Constant(lowerValue)),
                Expression.LessThanOrEqual(member, Expression.Constant(upperValue))
            );
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> CreateInExpression<T>(
            this IQueryable<T> iqueryables,
            string propertyName, 
            object value)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var constant = Expression.Constant(value);
            var body = Expression.Call(typeof(Enumerable), 
                "Contains", 
                new[] { propertyType }, constant, member);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public static Expression<Func<T, bool>> CreateContainsExpression<T>(
            this IQueryable<T> iqueryables,
            string propertyName,
            string value)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var member = Expression.Property(param, propertyName);
            var constant = Expression.Constant(value);
            var body = Expression.Call(member, "Contains", Type.EmptyTypes, constant);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}
