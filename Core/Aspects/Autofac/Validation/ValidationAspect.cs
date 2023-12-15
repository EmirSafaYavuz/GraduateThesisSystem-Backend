using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var validatorGenericType = typeof(IValidator<>).MakeGenericType(entityType);

            if (!validatorGenericType.IsAssignableFrom(_validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                // Use ValidationTool.Validate with the generic method
                typeof(ValidationTool)
                    .GetMethod("Validate")
                    ?.MakeGenericMethod(entityType)
                    .Invoke(null, new[] { validator, entity });
            }
        }
    }
}