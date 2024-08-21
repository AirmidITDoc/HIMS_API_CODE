using HIMS.Core.Domain.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;


namespace HIMS.Data.Extensions
{
    public static class SearchFieldExtension
    {
        public static List<SearchModel> GetSearchFields(List<SearchFields> model)
        {
            List<SearchModel> searchModelList = new();
            foreach (SearchFields fields in model)
            {
                if (string.IsNullOrEmpty(fields.FieldName) || fields.FieldValue == null) throw new Exception("Invalid search field.");

                string fieldName = fields.FieldName;

                if (string.IsNullOrEmpty(fieldName)) continue;
                SearchModel searchModel = new();
                switch (Convert.ToInt32(fields.OpType))
                {
                    //(OperatorComparer)Enum.Parse(typeof(OperatorComparer), objFilter.OpType
                    case (int)OperatorComparer.Equals:
                        searchModel = new SearchModel()
                        {
                            FieldName = fieldName,
                            FieldValueString = $"{fields.FieldValue.ToLower()}",
                            FieldOperator = OperatorComparer.Equals.ToString()
                        };
                        break;
                    case (int)OperatorComparer.InClause:
                        List<string> values = new();
                        foreach (var field in fields.FieldValue)
                        {
                            values.Add($"'{field}'");
                        }
                        searchModel = new SearchModel()
                        {
                            FieldName = fieldName,
                            FieldOperator = OperatorComparer.InClause.ToString(),
                            FieldValueString = $"({string.Join(",", values)})"
                        };
                        break;
                    case (int)OperatorComparer.LessThan:
                        searchModel = new SearchModel()
                        {
                            FieldName = fieldName,
                            FieldValueString = $"{fields.FieldValue.ToLower()}",
                            FieldOperator = OperatorComparer.LessThan.ToString()
                        };
                        break;
                    case (int)OperatorComparer.GreaterThan:
                        searchModel = new SearchModel()
                        {
                            FieldName = fieldName,
                            FieldValueString = $"{fields.FieldValue.ToLower()}",
                            FieldOperator = OperatorComparer.GreaterThan.ToString()
                        };
                        break;
                    case (int)OperatorComparer.LessThanOrEqual:
                        searchModel = new SearchModel()
                        {
                            FieldName = fieldName,
                            FieldValueString = $"{fields.FieldValue.ToLower()}",
                            FieldOperator = OperatorComparer.LessThanOrEqual.ToString()
                        };
                        break;
                    case (int)OperatorComparer.GreaterThanOrEqual:
                        searchModel = new SearchModel()
                        {
                            FieldName = fieldName,
                            FieldValueString = $"{fields.FieldValue.ToLower()}",
                            FieldOperator = OperatorComparer.GreaterThanOrEqual.ToString()
                        };
                        break;
                }
                searchModelList.Add(searchModel);
            }
            return searchModelList;
        }

        public static string GetDescriptionValue<T>(string propertyName)
        {
            string fieldName = string.Empty;
            Type t = typeof(T);
            var _propertyNames = propertyName.Split('.');
            PropertyInfo prop = null;
            for (var i = 0; i < _propertyNames.Length; i++)
            {
                prop = t.GetProperty(_propertyNames[i], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop != null)
                {
                    var pt = prop.PropertyType;
                    if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(List<>))
                        t = pt.GetGenericArguments()[0];
                    else
                        t = prop.GetType();
                }
            }
            if (prop != null)
            {
                var customAttribute = prop.GetCustomAttributes(typeof(DescriptionAttribute), false);
                fieldName = customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : string.Empty;
            }

            return fieldName;
        }
    }
}

