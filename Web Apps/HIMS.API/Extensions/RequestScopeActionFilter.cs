using HIMS.API.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections;
using System.Reflection;

namespace HIMS.API.Extensions
{
    public sealed class RequestScopeActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Items["RequestContext"] is not RequestContext approved ||
                ScopeInputValidator.TryValidate(context.ActionArguments, approved, out _))
            {
                await next();
                return;
            }

            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }

    public static class ScopeInputValidator
    {
        private static readonly HashSet<Type> TerminalTypes = new()
        {
            typeof(string), typeof(DateTime), typeof(DateTimeOffset), typeof(Guid), typeof(decimal)
        };

        public static bool TryValidate(object? value, RequestContext approved, out string reason)
        {
            return TryValidate(value, approved, out reason, 0, new HashSet<object>(ReferenceEqualityComparer.Instance));
        }

        private static bool TryValidate(object? value, RequestContext approved, out string reason, int depth,
            HashSet<object> visited)
        {
            reason = string.Empty;
            if (value == null || depth > 5 || value is Type)
                return true;

            Type type = value.GetType();
            if (type.IsPrimitive || type.IsEnum || type.IsValueType || TerminalTypes.Contains(type))
                return true;
            if (!visited.Add(value))
                return true;

            if (value is IDictionary dictionary)
            {
                foreach (DictionaryEntry entry in dictionary)
                {
                    if (entry.Key is string argumentName &&
                        string.Equals(argumentName, "StoreId", StringComparison.OrdinalIgnoreCase) &&
                        TryReadPositiveId(entry.Value, out int storeId) && storeId != approved.StoreId)
                    {
                        reason = "Store scope conflicts with the approved request context.";
                        return false;
                    }
                    if (entry.Key is string unitArgumentName &&
                        string.Equals(unitArgumentName, "UnitId", StringComparison.OrdinalIgnoreCase) &&
                        TryReadPositiveId(entry.Value, out int unitId) && unitId != approved.UnitId)
                    {
                        reason = "Unit scope conflicts with the approved request context.";
                        return false;
                    }
                    if (!TryValidate(entry.Value, approved, out reason, depth + 1, visited))
                        return false;
                }
                return true;
            }

            if (value is IEnumerable enumerable && value is not string)
            {
                foreach (object? item in enumerable)
                {
                    if (!TryValidate(item, approved, out reason, depth + 1, visited))
                        return false;
                }
                return true;
            }

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                         .Where(property => property.CanRead && property.GetIndexParameters().Length == 0))
            {
                object? propertyValue;
                try { propertyValue = property.GetValue(value); }
                catch { continue; }

                if (string.Equals(property.Name, "StoreId", StringComparison.OrdinalIgnoreCase) &&
                    TryReadPositiveId(propertyValue, out int storeId) && storeId != approved.StoreId)
                {
                    reason = "Store scope conflicts with the approved request context.";
                    return false;
                }
                if (string.Equals(property.Name, "UnitId", StringComparison.OrdinalIgnoreCase) &&
                    TryReadPositiveId(propertyValue, out int unitId) && unitId != approved.UnitId)
                {
                    reason = "Unit scope conflicts with the approved request context.";
                    return false;
                }
                if (!TryValidate(propertyValue, approved, out reason, depth + 1, visited))
                    return false;
            }
            return true;
        }

        private static bool TryReadPositiveId(object? value, out int id)
        {
            id = 0;
            return value != null && int.TryParse(Convert.ToString(value), out id) && id > 0;
        }
    }
}
