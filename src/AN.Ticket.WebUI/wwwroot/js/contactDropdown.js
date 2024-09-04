$(document).ready(function () {
    $('#contactSearch').on('input', function () {
        var filter = $(this).val().toLowerCase();
        $('.dropdown-item').each(function () {
            var text = $(this).text().toLowerCase();
            $(this).toggle(text.includes(filter));
        });
    });

    $('.dropdown-item').on('click', function (e) {
        e.preventDefault();

        var selectedContactName = $(this).text().trim();
        var selectedContactId = $(this).data('contact-id');

        $('#contactInput').val(selectedContactName);
        $('#selectedContactId').val(selectedContactId);

        loadContactDetails(selectedContactId);
    });

    $('#contactInput').on('input', function () {
        if ($(this).val().trim() === '') {
            $('#selectedContactId').val('');
            loadContactDetails('00000000-0000-0000-0000-000000000000');
        }
    });

    function loadContactDetails(contactId) {
        $.ajax({
            url: '/Ticket/GetContactDetails',
            type: 'GET',
            data: { contactId: contactId },
            success: function (data) {
                $('#contactDetailsContainer').html(data);
            },
            error: function (xhr, status, error) {
                console.error('Erro ao carregar os detalhes do contato:', error);
            }
        });
    }
});
