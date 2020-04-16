using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DK.Utility
{
    public static class ExpressionManager
    {
        public static Dictionary<int, List<long>> GetExpressionEntitie<T>(Dictionary<int, List<long>> xDictionary, int xLevel, List<T> xEntities, List<long> xIDs)
        {


            List<T> xChildEntities = new List<T>();
            IQueryable<T> queryableData = xEntities.AsQueryable<T>();
            ParameterExpression targetParameter = Expression.Parameter(typeof(T), "t");
            MemberExpression targetProperty = Expression.Property(targetParameter, "xParentJobPositionID");
            ConstantExpression constantExpression = Expression.Constant(null, typeof(long?));
            if (xIDs == null)
            {

                BinaryExpression equalityParentIDToNullExpression = Expression.Equal(targetProperty, constantExpression);
                Expression<Func<T, bool>> lambdaExpression = Expression.Lambda<Func<T, bool>>(equalityParentIDToNullExpression, targetParameter);
                MethodCallExpression whereCallForNullExpression = Expression.Call(
                   typeof(Queryable),
                    "Where",
                    new Type[] { queryableData.ElementType },
                      queryableData.Expression,
                     lambdaExpression
               );
                xChildEntities = queryableData.Provider.CreateQuery<T>(whereCallForNullExpression).ToList();
            }
            else
            {
                ConstantExpression idsParameter = Expression.Constant(xIDs, typeof(List<long>));
                IQueryable<long> queryableIDs = xIDs.AsQueryable<long>();
                MethodInfo methodContains = typeof(List<long>).GetMethod("Contains", new Type[] { typeof(long) });
                Expression convertExpr = Expression.Convert(targetProperty, typeof(long));
                MethodCallExpression containsMethodExp = Expression.Call(idsParameter, methodContains, convertExpr);
                Expression<Func<T, bool>> lambdaIDsExpression = Expression.Lambda<Func<T, bool>>(containsMethodExp, targetParameter);
                MethodCallExpression whereCallExpression = Expression.Call(
                  typeof(Queryable),
                   "Where",
                   new Type[] { queryableData.ElementType },
                    queryableData.Expression,
                  lambdaIDsExpression
              );

                xChildEntities = queryableData.Provider.CreateQuery<T>(whereCallExpression).ToList();

            }
            if (xChildEntities.Any())
            {
                List<long> xChildIDs = new List<long>();
                MemberExpression idProperty = Expression.Property(targetParameter, "xID");
                Expression<Func<T, long>> lambdaIDExpression = Expression.Lambda<Func<T, long>>(idProperty, targetParameter);
                xChildIDs = xChildEntities.AsQueryable().Select(lambdaIDExpression).ToList();
                foreach (var item in xChildEntities)
                {
                    xEntities.Remove(item);
                }
                xDictionary.Add(xLevel, xChildIDs);
                if (xEntities.Count > 0)
                {
                    GetExpressionEntitie(xDictionary, xLevel + 1, xEntities, xChildIDs);
                }
                else
                {
                    return xDictionary;
                }

            }
            return xDictionary;
        }
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }
   

    class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;

            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
