$(function () {
    var qtdLogradouros = 0;

    $("#btn-add-logradouro").click(function (e) {
        e.preventDefault();

        var blocoLogradouro = '<div class="row">' +            
            '    <div class="col-md-11">' +
            '        <input type="text" name="Logradouros[' + qtdLogradouros + '].Endereco" placeholder="Endereço" class="form-control txt-endereco" />' +
            '    </div>' +            
            '    <div class="col-md-1">' +
            '        <button class="btn btn-danger btn-remover-logradouro">' +
            '            <span class="glyphicon glyphicon-trash"></span>' +
            '        </button>' +
            '    </div>' +
            '</div>';

        $("#div-logradouros").append(blocoLogradouro);

        qtdLogradouros++;
    });

    $("#div-logradouros").on("click", ".btn-remover-logradouro", function (e) {
        e.preventDefault();

        $(this).parent().parent().remove();

        qtdLogradouros--;

        $("#div-logradouros .row").each(function (indice, elemento) {
            $(elemento).find(".txt-endereco").attr("name", "Logradouros[" + indice + "].Endereco");
        });
    });
});