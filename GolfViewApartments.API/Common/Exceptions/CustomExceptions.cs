namespace GolfViewApartments.API.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource is not found
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key)
            : base($"{name} with key '{key}' was not found")
        {
        }
    }

    /// <summary>
    /// Exception thrown when a business rule is violated
    /// </summary>
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {
        }

        public BusinessRuleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Exception thrown when validation fails
    /// </summary>
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException(List<string> errors)
            : base("One or more validation errors occurred")
        {
            Errors = errors;
        }

        public ValidationException(string error)
            : base(error)
        {
            Errors = new List<string> { error };
        }
    }
}