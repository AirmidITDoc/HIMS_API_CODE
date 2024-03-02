using HIMS.Core.Domain.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace HIMS.Data.Extensions
{
    public enum OperatorComparer
    {
        Contains,
        StartsWith,
        EndsWith,
        Equals = ExpressionType.Equal,
        GreaterThan = ExpressionType.GreaterThan,
        GreaterThanOrEqual = ExpressionType.GreaterThanOrEqual,
        LessThan = ExpressionType.LessThan,
        LessThanOrEqual = ExpressionType.LessThanOrEqual,
        NotEqual = ExpressionType.NotEqual,
        InClause
    }
    public static class DynamicLinqExpressionBuilder
    {
        public static int ToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string ConvertToString(this object obj)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static bool ConvertToBool(this object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<IPagedList<T>> BuildPredicate<T>(this IQueryable<T> query, GridRequestModel objGrid)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), typeof(T).Name);
            //for change between to greater and less (one item to two item)
            List<SearchGrid> datefilters = objGrid.Filters.Where(x => x.OpType.ToUpper() == "DATERANGE").ToList();
            foreach (SearchGrid objFilter in datefilters)
            {
                if (!string.IsNullOrEmpty(objFilter.FieldValue))//check first string is null or empty
                {
                    // if first date null then we consider second date other wise we go with first date
                    string FirstDate = objFilter?.FieldValue?.Split('-')[0] ?? "";
                    string SecondDate = objFilter.FieldValue.Contains('-') ? objFilter?.FieldValue?.Split('-')[1] ?? "" : "";
                    objGrid.Filters.Add(new SearchGrid() { FieldName = objFilter.FieldName, FieldValue = !string.IsNullOrWhiteSpace(FirstDate) ? Convert.ToDateTime(FirstDate).ToString("yyyy-MM-dd HH:mm") : "", OpType = "GreaterThanOrEqual" });
                    objFilter.FieldValue = !string.IsNullOrWhiteSpace(SecondDate) ? SecondDate + " 23:59:59" : "";
                    objFilter.OpType = "LessThanOrEqual";
                }
            }
            foreach (SearchGrid objFilter in objGrid.Filters.Where(x => !string.IsNullOrWhiteSpace(x.FieldValue)))
            {
                if (objFilter.OpType == OperatorComparer.InClause.ToString())
                {
                    Expression<Func<T, bool>> predicate = SelectListContainsPredicate<T>(objFilter.FieldName, objFilter.FieldValue.Replace('|', ','));
                    query = query.Where(predicate);
                }
                else
                {
                    Expression<Func<T, bool>> predicate = (Expression<Func<T, bool>>)BuildNavigationExpression(parameterExpression, (OperatorComparer)Enum.Parse(typeof(OperatorComparer), objFilter.OpType), objFilter.FieldValue, objFilter.FieldName);
                    query = query.Where(predicate);
                }
            }
            #region :: check delete column exist or not ::
            if (!string.IsNullOrEmpty(parameterExpression.Type.GetProperty("IsDeleted", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.Name ?? ""))
            {
                Expression<Func<T, bool>> r = (Expression<Func<T, bool>>)BuildCondition(parameterExpression, "IsDeleted", OperatorComparer.Equals, "false");
                query = query.Where(r);
            }
            #endregion
            if (!string.IsNullOrWhiteSpace(objGrid.SortField))
            {
                query = query.OrderBy(objGrid.SortField, objGrid.SortOrder == -1);
            }
            return await query.ToPagedListAsync(objGrid.First, objGrid.Rows, objGrid.ExportType != 0 || objGrid.Rows == -1);
        }
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            Type type = typeof(TEntity);
            PropertyInfo property = type.GetProperty(orderByProperty, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            ParameterExpression parameter = Expression.Parameter(type, "p");
            MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
        public static IQueryable<T> ThenByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        private static IQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
        private static Expression BuildOrExpression(Expression existingExpression, Expression expressionToAdd)
        {
            if (existingExpression == null)
            {
                return expressionToAdd;
            }

            //Build 'OR' expression for each property
            return Expression.OrElse(existingExpression, expressionToAdd);
        }
        private static Expression BuildNavigationExpression(Expression parameter, OperatorComparer comparer, object value, params string[] properties)
        {
            Expression childParameter, predicate;
            Type childType = null;

            Expression resultExpression;
            if (properties.Length > 1)
            {
                //build path
                parameter = Expression.Property(parameter, properties[0]);
                bool isCollection = typeof(IEnumerable).IsAssignableFrom(parameter.Type);
                //if it´s a collection we later need to use the predicate in the methodexpressioncall
                if (isCollection)
                {
                    childType = parameter.Type.GetGenericArguments()[0];
                    childParameter = Expression.Parameter(childType, childType.Name);
                }
                else
                {
                    childParameter = parameter;
                }
                //skip current property and get navigation property expression recursivly
                string[] innerProperties = properties.Skip(1).ToArray();
                predicate = BuildNavigationExpression(childParameter, comparer, value, innerProperties);
                if (isCollection)
                {
                    //build subquery
                    resultExpression = BuildSubQuery(parameter, childType, predicate);
                }
                else
                {
                    resultExpression = predicate;
                }
            }
            else
            {
                //build final predicate
                resultExpression = BuildCondition(parameter, properties[0], comparer, value);
            }
            return resultExpression;
        }

        private static Expression BuildSubQuery(Expression parameter, Type childType, Expression predicate)
        {
            MethodInfo anyMethod = typeof(Enumerable).GetMethods().Single(m => m.Name == "Any" && m.GetParameters().Length == 2);
            anyMethod = anyMethod.MakeGenericMethod(childType);
            predicate = Expression.Call(anyMethod, parameter, predicate);
            return MakeLambda(parameter, predicate);
        }

        private static Expression BuildCondition(Expression parameter, string property, OperatorComparer comparer, object value)
        {
            if (property.Contains('|'))
            {
                Expression final = null;
                foreach (string pro in property.Split('|'))
                {
                    PropertyInfo childProperty = parameter.Type.GetProperty(pro, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    MemberExpression left = Expression.Property(parameter, childProperty);
                    ConstantExpression right = Expression.Constant(value);
                    Expression predicate = BuildComparsion(left, comparer, right);
                    //return MakeLambda(parameter, predicate);
                    final = BuildOrExpression(final, predicate);
                }
                return MakeLambda(parameter, final);
            }
            else
            {
                PropertyInfo childProperty = parameter.Type.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                MemberExpression left = Expression.Property(parameter, childProperty);
                ConstantExpression right = Expression.Constant(value);
                Expression predicate = BuildComparsion(left, comparer, right);
                return MakeLambda(parameter, predicate);
            }
        }

        public static Expression<Func<T, bool>> SelectListContainsPredicate<T>(string columnName, string searchValues, char separator = ',')
        {
            Type type = typeof(T);
            ParameterExpression parameter = Expression.Parameter(type, "x");
            ConstantExpression constant = Expression.Constant(true);
            PropertyInfo property = type.GetProperties().FirstOrDefault(p => p.Name == columnName);
            string[] values = searchValues.Split(separator);
            if (property == null || values.Length == 0)
            {
                return Expression.Lambda<Func<T, bool>>(constant, parameter);
            }
            MemberExpression member = Expression.Property(parameter, property);
            bool isInt = property.PropertyType.FullName.Contains("Int", StringComparison.CurrentCulture) || property.PropertyType.BaseType.Name.Contains("Enum", StringComparison.CurrentCulture);
            MethodInfo method = isInt ? typeof(List<int>).GetMethod("Contains") : typeof(List<string>).GetMethod("Contains");
            constant = Expression.Constant(isInt ? values.Select(int.Parse).ToList() : values.ToList());
            //Expression memberAsInt = null;
            //if (property.PropertyType.BaseType.Name.ToLower() == "int")
            //{
            //    memberAsInt = Expression.Convert(member, typeof(Int32));
            //}
            //else if (property.PropertyType.BaseType.Name.ToLower() == "enum" || property.PropertyType.BaseType.Name == "Nullable`1")
            //{
            //    if (property.PropertyType.BaseType == typeof(Nullable<DateTime>))
            //    {
            //        memberAsInt = Expression.Convert(member, typeof(DateTime?));
            //    }
            //    else
            //        memberAsInt = Expression.Convert(member, typeof(int));
            //}
            //else
            //{
            //    memberAsInt = Expression.Convert(member, typeof(string));
            //}
            Expression memberAsInt = isInt ? Expression.Convert(member, typeof(int)) : Expression.Convert(member, typeof(string));
            Expression expression = Expression.Call(constant, method, memberAsInt);
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        private static Expression BuildComparsion(Expression left, OperatorComparer comparer, Expression right)
        {
            List<OperatorComparer> mask = new()
            {
                OperatorComparer.Contains,
                OperatorComparer.StartsWith,
                OperatorComparer.EndsWith
            };
            if (mask.Contains(comparer) && left.Type != typeof(string))
            {
                comparer = OperatorComparer.Equals;
            }
            if (!mask.Contains(comparer))
            {
                if (left.Type.Name.ToLower() == "string")
                {
                    return Expression.MakeBinary((ExpressionType)comparer, left, Expression.Convert(right, left.Type));
                }
                else if (left.Type == typeof(DateTime))
                {
                    Expression propertyValueDate = Expression.Constant(Convert.ToDateTime(right.ToString().Replace("\"", "")));
                    return Expression.MakeBinary((ExpressionType)comparer, left, Expression.Convert(propertyValueDate, typeof(DateTime)));
                }
                else if (left.Type.BaseType.Name.ToLower() == "enum" || left.Type.Name == "Nullable`1")
                {
                    if (left.Type == typeof(Nullable<DateTime>))
                    {
                        Expression propertyValueDate = Expression.Constant(Convert.ToDateTime(right.ToString().Replace("\"", "")));
                        return Expression.MakeBinary((ExpressionType)comparer, left, Expression.Convert(propertyValueDate, typeof(DateTime?)));
                    }
                    else
                    {
                        return Expression.MakeBinary((ExpressionType)comparer, Expression.Call(Expression.Convert(left, typeof(int)), typeof(object).GetMethod("ToString")), right);
                    }
                }
                else
                {
                    return Expression.MakeBinary((ExpressionType)comparer, left, Expression.Call(left.Type, "Parse", null, right));
                }
            }
            return BuildStringCondition(left, comparer, right);
        }


        private static Expression BuildStringCondition(Expression left, OperatorComparer comparer, Expression right)
        {
            MethodInfo compareMethod = typeof(string).GetMethods().FirstOrDefault(m => m.Name.Equals(Enum.GetName(typeof(OperatorComparer), comparer)) && m.GetParameters().Length == 1);
            //we assume ignoreCase, so call ToLower on paramter and memberexpression
            MethodInfo toLowerMethod = typeof(string).GetMethods().FirstOrDefault(m => m.Name.Equals("ToLower") && m.GetParameters().Length == 0);
            left = Expression.Call(left, toLowerMethod);
            right = Expression.Call(right, toLowerMethod);
            return Expression.Call(left, compareMethod, right);
        }

        private static Expression MakeLambda(Expression parameter, Expression predicate)
        {
            ParameterVisitor resultParameterVisitor = new();
            resultParameterVisitor.Visit(parameter);
            Expression resultParameter = resultParameterVisitor.Parameter;
            return Expression.Lambda(predicate, (ParameterExpression)resultParameter);
        }

        private class ParameterVisitor : ExpressionVisitor
        {
            public Expression Parameter
            {
                get;
                private set;
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                Parameter = node;
                return node;
            }
        }
    }
}
