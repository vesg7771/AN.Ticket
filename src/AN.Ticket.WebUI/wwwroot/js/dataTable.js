$(document).ready( function () {
    getDataTableId('#table-contactDto');
} );

function getDataTableId(id) {
    $(id).DataTable({
        
        language: {
            url: '//cdn.datatables.net/plug-ins/2.1.6/i18n/pt-BR.json',
        },
    });
}
