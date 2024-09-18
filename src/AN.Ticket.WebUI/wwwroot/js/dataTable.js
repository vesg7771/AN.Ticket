$(document).ready( function () {
    $('#table-contactDto').DataTable({
        // columns: [
        //      { data: 'Nome' },
        //      { data: 'Email' },
        //      { data: 'Telefone' },
        //      { data: 'Endereço' },
        //      { data: 'Button' },
        // ],

        // columns: [
        //     { data: 'FullName', title: "Nome" },
        //     { data: 'PrimaryEmail', title: "Email" },
        //     { data: 'Phone', title: "Telefone" },
        //     { data: 'Department', title: "Endereço" },
        //     { data: null, title: "#", orderable: false }
        // ],
        language: {
            url: '//cdn.datatables.net/plug-ins/2.1.6/i18n/pt-BR.json',
        },
    });
} );
