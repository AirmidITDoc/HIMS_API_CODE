
using FluentValidation;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HIMS.API.Validator
{
    public abstract class CommonDynamicValidator<T> : AbstractValidator<T>
    {
        protected CommonDynamicValidator(params string[] ignoreProperties)
        {
            var properties = typeof(T).GetProperties();
            var nullabilityContext = new NullabilityInfoContext();

            foreach (var property in properties)
            {
                if (ignoreProperties.Contains(property.Name))
                    continue;

                // String validation
                if (property.PropertyType == typeof(string))
                {
                    var isNullable = nullabilityContext.Create(property).WriteState
                                     == NullabilityState.Nullable;

                    AddStringValidation(property, isNullable);
                }
            }
        }

        private void AddStringValidation(PropertyInfo property, bool isNullable)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(parameter, property);
            var lambda = Expression.Lambda<Func<T, string>>(member, parameter);

            var rule = RuleFor(lambda);

            if (!isNullable)
            {
                rule.NotNull()
                    .WithMessage($"{property.Name} is required");
            }

            rule.Must(value =>
                    value == null ||
                    !Regex.IsMatch(value,
                        @"<|>|javascript\s*:|<script",
                        RegexOptions.IgnoreCase))
                .WithMessage($"{property.Name} contains invalid input");
        }

        protected void IgnoreValidation(params string[] propertyNames)
        {
            // Properties passed to the base constructor are ignored.
        }
    }
}
