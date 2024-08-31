namespace AN.Ticket.Domain.EntityValidations;
public class EntityValidationException : Exception
{
    public EntityValidationException(string message)
        : base(message)
    { }

    public EntityValidationException(
        string message, Exception innerException
    )
        : base(message, innerException)
    { }

    public EntityValidationException()
    { }
}
