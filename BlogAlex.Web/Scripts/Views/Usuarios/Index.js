$(document).ready(function () {
    $(".excluir-usuario").on("click", function (e) {
        if (!confirm("Deseja realmente excluir este usuário?")) {
            e.preventDefault();
        }
    });
});