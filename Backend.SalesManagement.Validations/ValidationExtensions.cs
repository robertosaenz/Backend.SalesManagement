using System.Collections.Generic;
using FluentValidation.Results;
using Backend.SalesManagement.Models;


namespace Backend.SalesManagement.Validations
{
    public static class ValidationExtensions
    {
        public static bool IsValid(this Sales sales, out IEnumerable<string> errors)
        {
            var validator = new SalesValidator();

            var validationResult = validator.Validate(sales);

            errors = AggregateErrors(validationResult);

            return validationResult.IsValid;
        }

        private static List<string> AggregateErrors(ValidationResult validationResult)
        {
            var errors = new List<string>();

            if (!validationResult.IsValid)
                foreach (var error in validationResult.Errors)
                    errors.Add(error.ErrorMessage);

            return errors;
        }
    }
}
