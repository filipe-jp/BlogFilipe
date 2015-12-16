$(document).ready(function(){
    $('.excluir-post').on('click', function (e) {
        if (!confirm('Deseja realmente excluir esse post?')) {
            e.preventDefault();
        }
    });
});