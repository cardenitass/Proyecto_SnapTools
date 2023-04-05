function ConsultarNombreApi() {

    let Identificacion = $("#Identificacion").val();

    if (Identificacion.trim().length >= 8) {
        $.ajax({
            url: "https://apis.gometa.org/cedulas/" + Identificacion,
            type: "GET",
            dataType: 'json',
            success: function (res) {

                $("#Nombre").val(res.nombre);
            }
        });
    } else {
        $("#Nombre").val("");
    }
}