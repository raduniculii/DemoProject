using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DemoProject.Data.Common
{
    public static class CloneHelper
    {

        private static Type[] additionalScalarTypes = new Type[]{
            typeof(string)
            , typeof(DateTime)
            , typeof(DateTimeOffset)
            , typeof(TimeSpan)
        };

        private static bool isScalar(Type t, bool allowNullable){
            if(allowNullable && t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                return isScalar(t.GetGenericArguments()[0], false);
            }

            return t.IsPrimitive || t.IsEnum || additionalScalarTypes.Contains(t);
        }

        public static Func<object, object> GetCloneMatchingScalarPropertiesFunction(Type mainType, Type cloneType){
            var parametrelessCtor = cloneType.GetConstructor(new Type[]{});
            if(parametrelessCtor == null) throw new InvalidProgramException($@"{mainType.FullName} cannot be cloned to {cloneType.FullName}, as teh latter doesn't have a parametreless constructor.");

            var from = Expression.Parameter(typeof(object), "from");
            var typedFrom = Expression.Parameter(mainType, "typedFrom");
            var to = Expression.Parameter(cloneType, "to");
            var expressionList = new List<Expression>(){
                Expression.Assign(to, Expression.New(parametrelessCtor))
                , Expression.Assign(typedFrom, Expression.Convert(from, mainType))
            };

            foreach (var prop in mainType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                            .Where(prop => prop.GetSetMethod() != null && isScalar(prop.PropertyType, allowNullable: true))
                    )
            {
                var propInClone = cloneType.GetProperty(prop.Name);

                if(propInClone != null) {
                    expressionList.Add(
                        Expression.Call(
                            to
                            , propInClone.GetSetMethod()!
                            , Expression.Call(typedFrom, prop.GetGetMethod()!)
                        )
                    );
                }
            }

            expressionList.Add(to); //this basically returns "to"

            return Expression.Lambda<Func<object, object>>(
                Expression.Block(
                    new ParameterExpression[]{ to, typedFrom }
                    , expressionList
                )
                , new ParameterExpression[]{ from }
            ).Compile()!;
        }
    }
}