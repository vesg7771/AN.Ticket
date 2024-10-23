// Função para carregar scripts externos dinamicamente
function loadScript(url, callback) {
    let script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = url;

    script.onload = function() {
        if (callback) callback();
    };

    document.head.appendChild(script);
}

// Carregar jQuery primeiro
loadScript('https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js', function() {
    // Carregar jQuery Mask após jQuery
    loadScript('https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js', function() {
        // Aplicar máscaras após carregar todas as bibliotecas
        $(document).ready(function() {
            $('#Cpf').mask('000.000.000-00');
            $('#Phone').mask('(00) 00000-0000');
            $('#Mobile').mask('(00) 00000-0000');
        });
    });
});
