using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Application.Common.Extensions
{
    public static class SortingExtension
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string sortQuery)
        {
            if (string.IsNullOrWhiteSpace(sortQuery)) return queryable;

            var ordered = queryable.OrderBy(x => 0);

            return sortQuery.Split(',').Aggregate(ordered, (current, sortBy) =>
            {
                var command = sortBy.EndsWith(" desc") ? "ThenByDescending" : "ThenBy";
                var propertyName = sortBy.Split(null)[0];

                var type = typeof(T);
                var property = type.GetProperties().FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

                if (property == null) return current;
                
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(
                    typeof(Queryable),
                    command, new Type[] { type, property.PropertyType },
                    current.Expression, Expression.Quote(orderByExpression));

                return (IOrderedQueryable<T>)current.Provider.CreateQuery<T>(resultExpression);
            });
        }
    }
}
