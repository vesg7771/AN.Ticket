namespace AN.Ticket.Application.Extensions;
public static class CpfValidator
{
    public static bool Validate(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) return false;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11) return false;

        if (cpf.Distinct().Count() == 1) return false;

        var numbers = cpf.Substring(0, 9);
        var digits = cpf.Substring(9, 2);

        var sum = 0;
        for (var i = 0; i < 9; i++)
            sum += int.Parse(numbers[i].ToString()) * (10 - i);

        var result = sum % 11;

        if (result == 0 || result == 1)
        {
            if (int.Parse(digits[0].ToString()) != 0) return false;
        }
        else if (int.Parse(digits[0].ToString()) != 11 - result) return false;

        sum = 0;
        for (var i = 0; i < 10; i++)
            sum += int.Parse(cpf[i].ToString()) * (11 - i);

        result = sum % 11;

        if (result == 0 || result == 1)
        {
            if (int.Parse(digits[1].ToString()) != 0) return false;
        }
        else if (int.Parse(digits[1].ToString()) != 11 - result) return false;

        return true;
    }
}
