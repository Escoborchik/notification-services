using FluentValidation.Results;
using SharedKernel.ErrorsBase;

namespace Framework.Validation;

public static class ValidationExtensions
{
    public static Errors ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;
        var errors = new List<Error>();

        foreach (var validationError in validationErrors)
        {
            var error = Error.Deserialize(validationError.ErrorMessage);
            errors.Add(Error.Validation(error.Code, error.Message, validationError.PropertyName));
        }

        return errors;
    }
}
