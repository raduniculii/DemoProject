using System.Linq.Expressions;
using System.Reflection;

namespace DemoProject.AppCode
{
    public static class WebApiItemListExtensions{
        public static IQueryable<T> Filter<T>(this IQueryable<T> onQueriable, string? filterString){
            var localQueriable = onQueriable;

            if( !string.IsNullOrWhiteSpace(filterString)) {
                localQueriable = localQueriable.Where(
                    (new FilterExpressionMatcher<T>(filterString)).GetPredicateExpression()
                );
            }

            return localQueriable;
        }

        public static IQueryable<T> LastlySortBy<T, UqSortType>(this IQueryable<T> onQueriable, Expression<Func<T, UqSortType>> uniqueSortBy){
            var localQueriable = onQueriable;

            localQueriable = (localQueriable.Expression.Type != typeof(IOrderedQueryable<T>)
                    ? localQueriable.OrderBy(uniqueSortBy)
                    : ((IOrderedQueryable<T>)localQueriable).ThenBy(uniqueSortBy)
                );
            
            return localQueriable;
        }
        public static IQueryable<T> SortBy<T>(this IQueryable<T> onQueriable, string? sortString, string? thenBy = null){
            var localQueriable = onQueriable;
            var fullSortString = string.Join(",", (new[]{ sortString, thenBy }).Where(s => !string.IsNullOrWhiteSpace(s)));

            if( !string.IsNullOrWhiteSpace(fullSortString)) {
                var type = typeof(T);
                ParameterExpression x = Expression.Parameter(type, "x");

                foreach(var colSpec in fullSortString.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)){
                    var sortDesc = colSpec.StartsWith("-");
                    var propName = (sortDesc ? colSpec.Substring(1) : colSpec);

                    PropertyInfo pi = type.GetProperty(propName)!;

                    Type delegateType = typeof(Func<,>).MakeGenericType(type, pi.PropertyType);
                    LambdaExpression lambda = Expression.Lambda(delegateType, Expression.Property(x, pi), x);

                    localQueriable = (IOrderedQueryable<T>)(
                        typeof(Queryable).GetMethods().Single(
                            method => method.Name == ( localQueriable.Expression.Type != typeof(IOrderedQueryable<T> )
                                            ? ( sortDesc ? nameof(Enumerable.OrderByDescending) : nameof(Enumerable.OrderBy) )
                                            : ( sortDesc ? nameof(Enumerable.ThenByDescending) : nameof(Enumerable.ThenBy) )
                                        )
                                    && method.IsGenericMethodDefinition
                                    && method.GetGenericArguments().Length == 2
                                    && method.GetParameters().Length == 2
                        ).MakeGenericMethod(type, pi.PropertyType)
                        .Invoke(null, new object[] { localQueriable, lambda })
                    )!;
                }
            }

            return localQueriable;
        }

        public static WebApiItemList<T> GetWebApiItemList<T>(this IQueryable<T> onQueriable, int startAt = 0, int? maxItems = null){
            var localQueriable = onQueriable;

            var l = new WebApiItemList<T>();

            l.TotalCount = localQueriable.Count(); //runs the Count(*) query

            //paging; it starts from the requested startAt unless the total count was lower, it starts from 0 to prevent another query that would do the same thing.
            l.FirstItemIndex = (startAt < l.TotalCount ? startAt : 0);
            localQueriable = localQueriable.Skip(l.FirstItemIndex);
            if(maxItems > 0){
                localQueriable = localQueriable.Take(maxItems.Value);
            }

            l.Items = localQueriable.ToList(); //runs the "get list" query

            return l;
        }
    }
}