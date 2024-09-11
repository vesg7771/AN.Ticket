$("#tab2").on("click", function () {
    $("#resolucao-content").show();
    $("#conversa-content, #anexo-content, #atividade-content, #aprovacao-content").hide();
});

$("#tab1").on("click", function () {
    $("#conversa-content").show();
    $("#resolucao-content, #anexo-content, #atividade-content, #aprovacao-content").hide();
});

$("#tab3").on("click", function () {
    $("#anexo-content").show();
    $("#conversa-content, #resolucao-content, #atividade-content, #aprovacao-content").hide();
});

$("#tab4").on("click", function () {
    $("#atividade-content").show();
    $("#anexo-content, #conversa-content, #resolucao-content, #aprovacao-content").hide();
});

$("#tab5").on("click", function () {
    $("#aprovacao-content").show();
    $("#atividade-content, #anexo-content, #conversa-content, #resolucao-content").hide();
});

$(".nav-link").on("click", function () {
    $(".nav-link").removeClass("active");
    $(this).addClass("active");
});
