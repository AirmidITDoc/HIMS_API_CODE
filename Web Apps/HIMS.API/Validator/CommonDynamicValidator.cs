
using FluentValidation;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HIMS.API.Validator
{
public abstract class CommonDynamicValidator<T> : AbstractValidator<T>
{
    protected CommonDynamicValidator()
    {
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            // String validation
            if (property.PropertyType == typeof(string))
            {
                AddStringValidation(property);
            }

            // Integer validation
            else if (property.PropertyType == typeof(int))
            {
                AddIntValidation(property);
            }

            // Nullable Integer validation
            else if (property.PropertyType == typeof(int?))
            {
                AddNullableIntValidation(property);
            }
        }
    }

    private void AddStringValidation(PropertyInfo property)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var member = Expression.Property(parameter, property);
        var lambda = Expression.Lambda<Func<T, string>>(member, parameter);

        RuleFor(lambda)
            .NotEmpty()
            .WithMessage($"{property.Name} is required");
    }

    private void AddIntValidation(PropertyInfo property)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var member = Expression.Property(parameter, property);
        var lambda = Expression.Lambda<Func<T, int>>(member, parameter);

        RuleFor(lambda)
            .GreaterThan(0)
            .WithMessage($"{property.Name} is required");
    }

    private void AddNullableIntValidation(PropertyInfo property)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var member = Expression.Property(parameter, property);
        var lambda = Expression.Lambda<Func<T, int?>>(member, parameter);

        RuleFor(lambda)
            .NotNull()
            .GreaterThan(0)
            .WithMessage($"{property.Name} is required");
    }
}
}
