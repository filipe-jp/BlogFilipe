$(document).ready(function () {
    var Tags = new Array();

    $("#adicionar").on('click', function () {
        var textoTag = $("#Tag").val().trim();
        if (textoTag) {


            Tags.push(new Object({ Tag: textoTag }));
            montaListaPeloArray();

            $('#Tag').val('');
        }

        $('#Tag').focus();
    });

    function montaListaPeloArray() {
        var form = $("form");

        $("input[Name='Tags']").remove();
        $("#resultado").empty();

        $(Tags).each(function () {
            $("#resultado").append("<li><span>" + this.Tag + '</span><a tag= "' + this.Tag + '" class="remover-tag icone-excluir"></a></li>');
            form.append("<input type='hidden' name='Tags' value='" + this.Tag + "'/>")
        });
    }

    $('body').on('click', '.remover-tag', function () {
        var tag = $(this).attr('tag');

        Tags = $.grep(Tags, function (e) {
            return e.Tag !== tag;
        });

        montaListaPeloArray();
    });

    $('input[Name="Tags"]').map(function () {
        var tag = $(this).val();
        Tags.push(new Object({ Tag: tag }));
    }).get();
});