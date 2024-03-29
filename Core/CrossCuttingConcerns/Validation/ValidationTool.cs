﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate<T>(IValidator<T> validator, T entity)
        {
            var result = validator.Validate(new ValidationContext<T>(entity));
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}