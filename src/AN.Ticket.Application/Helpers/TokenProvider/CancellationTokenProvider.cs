namespace AN.Ticket.Application.Helpers.TokenProvider;
public class CancellationTokenProvider
{
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public CancellationToken GetToken() => _cancellationTokenSource.Token;

    public void Cancel() => _cancellationTokenSource.Cancel();
}
