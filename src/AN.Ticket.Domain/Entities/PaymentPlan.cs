using AN.Ticket.Domain.Entities.Base;
using AN.Ticket.Domain.EntityValidations;

namespace AN.Ticket.Domain.Entities;
public class PaymentPlan : EntityBase
{
    public string Description { get; private set; }
    public double Value { get; private set; }

    protected PaymentPlan() { }

    public PaymentPlan(
        string description,
        double value
    )
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be empty.", nameof(description));
        if (value <= 0) throw new EntityValidationException("Value must be greater than zero.");

        Description = description;
        Value = value;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription)) throw new ArgumentException("NewDescription cannot be empty.", nameof(newDescription));
        Description = newDescription;
    }

    public void UpdateValue(double newValue)
    {
        if (newValue <= 0) throw new EntityValidationException("NewValue must be greater than zero.");
        Value = newValue;
    }
}
