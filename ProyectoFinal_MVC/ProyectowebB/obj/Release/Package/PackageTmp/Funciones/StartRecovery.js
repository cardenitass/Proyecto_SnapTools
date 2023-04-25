function BuscarCorreo() {

    $("#BtnEnviarContrasenna").prop("disabled", true);
    let correo = $("#Email").val();

    $.ajax({
        url: "/Access/BuscarCorreo",
        type: "POST",
        data: {
            "correo": correo
        },
        dataType: 'json',
        success: function (res) {

            if (res != "ERROR") {

                if (res == "") {
                    $("#BtnEnviarContrasenna").prop("disabled", false);
                }
                else {
                    alert(res);
                }
            }
        }
    });

}