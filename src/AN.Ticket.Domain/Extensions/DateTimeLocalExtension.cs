namespace AN.Ticket.Domain.Extensions;
public static class DateTimeLocalExtension
{
    public static DateTime ToLocal(this DateTime dateTime)
    {
        return dateTime.AddHours(-3);
    }
}
