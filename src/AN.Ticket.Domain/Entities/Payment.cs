using AN.Ticket.Domain.Entities.Base;

namespace AN.Ticket.Domain.Entities;

public class Payment : EntityBase
{
    public Guid ContactId { get; private set; }
    public Contact Contact { get; private set; }
    public double MonthlyFee { get; private set; }
    public DateTime DueDate { get; private set; }
    public bool Paid { get; private set; }
    public Guid PaymentPlanId { get; private set; }
    public PaymentPlan? PaymentPlan { get; set; }


    protected Payment() { }

    public Payment(
        Guid contactId,
        double monthlyFee,
        DateTime dueDate,
        Guid paymentPlanId
    )
    {
        if (contactId == Guid.Empty) throw new ArgumentException("ContactId cannot be empty.", nameof(contactId));
        if (monthlyFee <= 0) throw new ArgumentException("MonthlyFee must be greater than zero.", nameof(monthlyFee));
        if (dueDate == default) throw new ArgumentException("DueDate must be a valid date.", nameof(dueDate));
        if (paymentPlanId == Guid.Empty) throw new ArgumentException("PaymentPlanId cannot be empty.", nameof(paymentPlanId));

        ContactId = contactId;
        MonthlyFee = monthlyFee;
        DueDate = dueDate;
        PaymentPlanId = paymentPlanId;
        Paid = false;
    }

    public void ConfirmPayment()
    {
        Paid = true;
    }

    public void UpdateDueDate(DateTime newDueDate)
    {
        if (newDueDate == default) throw new ArgumentException("NewDueDate must be a valid date.", nameof(newDueDate));
        DueDate = newDueDate;
    }

    public void UpdateMonthlyFee(double newMonthlyFee)
    {
        if (newMonthlyFee <= 0) throw new ArgumentException("NewMonthlyFee must be greater than zero.", nameof(newMonthlyFee));
        MonthlyFee = newMonthlyFee;
    }
}

