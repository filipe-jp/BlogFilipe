$(document).ready(function () {
    $('.excluir-usuario').on('click', function (e) {
        if (!confirm('Deseja realmente excluir esse usuário?')) {
            e.preventDefault();
        }
    });
});