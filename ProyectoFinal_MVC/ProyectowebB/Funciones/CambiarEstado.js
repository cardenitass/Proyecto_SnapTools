$(document).on("click", ".openModal", function () {
    var id = $(this).data('id');
    $("#idModalHidden").val(id);
});

function CambiarEstado() {
    let id = $("#idModalHidden").val();

    $.ajax({
        url: "/Usuario/CambiarEstado",
        type: "POST",
        data: {
            "id": id
        },
        dataType: 'json',
        success: function () {
            window.location.href = "/Usuario/Index";
        }
    });
}