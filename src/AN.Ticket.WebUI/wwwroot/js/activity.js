$(document).ready(function () {
    $('#ticketDropdown').on('change', function () {
        var selectedTicketId = $(this).val();

        if (selectedTicketId) {
            loadTicketDetails(selectedTicketId);
        } else {
            loadTicketDetails('00000000-0000-0000-0000-000000000000');
        }
    });

    function loadTicketDetails(ticketId) {
        $.ajax({
            url: '/Activity/GetTicketDetails',
            type: 'GET',
            data: { ticketId: ticketId },
            success: function (data) {
                $('#ticketDetailsContainer').html(data);
            },
            error: function (xhr, status, error) {
                console.error('Erro ao carregar os detalhes do ticket:', error);
            }
        });
    }
});