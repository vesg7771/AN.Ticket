using AN.Ticket.Application.DTOs.Ticket;
using static AN.Ticket.Application.Services.HomeService;

namespace AN.Ticket.Application.DTOs.Home;
public class HomeDto
{
    public int QtyOfTicketsOnhold { get; set; } = 0;
    public int QtyOfContactsAssociation { get; set; } = 0;
    public int QtyOfAvaliations { get; set; } = 0;
    public int QtyOfTicketsClosed { get; set; } = 0;
    public int QtyOfTicketsOpen { get; set; } = 0;
    public int QtyOfTicketsInProgress { get; set; } = 0;
    public bool HasOverdueTickets { get; set; }
    public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    public List<TicketsByDayDto> TicketsByDay { get; set; } = new List<TicketsByDayDto>();

    public string GetAnalysis()
    {
        if (HasOverdueTickets)
        {
            return $"Atenção! Há tickets vencidos e não fechados. Verifique imediatamente!";
        }
        else if (QtyOfTicketsInProgress > QtyOfTicketsClosed)
        {
            return $"Você tem mais tickets em andamento ({QtyOfTicketsInProgress}) do que fechados ({QtyOfTicketsClosed}). Avalie se há bloqueios e redistribua a carga, se necessário.";
        }
        else if (QtyOfTicketsClosed > QtyOfTicketsInProgress)
        {
            return $"Excelente progresso! Você tem mais tickets fechados ({QtyOfTicketsClosed}) do que em andamento ({QtyOfTicketsInProgress}). Continue assim!";
        }
        else if (QtyOfTicketsOpen > QtyOfTicketsClosed)
        {
            return $"Há muitos tickets abertos ({QtyOfTicketsOpen}) comparados aos fechados ({QtyOfTicketsClosed}). Verifique pontos de bloqueio e priorize a resolução.";
        }
        else
        {
            return $"Bom equilíbrio entre tickets abertos e fechados. Continue focando nos tickets em progresso para evitar atrasos.";
        }
    }

    public List<string> GetSuggestions()
    {
        var suggestions = new List<string>();

        if (HasOverdueTickets)
        {
            suggestions.Add("Priorize os tickets vencidos para evitar a insatisfação dos clientes.");
        }

        if (QtyOfTicketsInProgress > 0)
        {
            suggestions.Add("Verifique os tickets em progresso para garantir que nenhum está parado há muito tempo.");
        }

        if (QtyOfTicketsOnhold > 0)
        {
            suggestions.Add("Revise os tickets em espera para identificar possíveis ações rápidas.");
        }

        if (QtyOfTicketsClosed > 0)
        {
            suggestions.Add("Comemore os tickets fechados e identifique as práticas que levaram ao sucesso.");
        }

        return suggestions;
    }
}
