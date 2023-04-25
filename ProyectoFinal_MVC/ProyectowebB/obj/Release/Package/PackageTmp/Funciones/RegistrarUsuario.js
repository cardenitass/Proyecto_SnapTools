function BuscarCorreo() {

    $("#btnRegistrar").prop("disabled", true);
    let correo = $("#CorreoElectronico").val();

    $.ajax({
        url: "/Home/BuscarCorreo",
        type: "POST",
        data: {
            "correo": correo
        },
        dataType: 'json',
        success: function (res) {

            if (res != "ERROR") {
                if (res == "") {
                    $("#btnRegistrar").prop("disabled", false);
                }
            }

        }
    });

}


