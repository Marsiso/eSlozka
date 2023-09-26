namespace eSlozka.Domain.Exceptions;

public class EntityValidationException : Exception
{
    public EntityValidationException(Dictionary<string, string[]> validationErrors)
    {
        ValidationErrors = validationErrors;
    }

    public Dictionary<string, string[]> ValidationErrors { get; }
}