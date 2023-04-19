function incrementarCantidad(inputId) {
    let input = document.getElementById(inputId);
    let currentValue = parseInt(input.value);
    input.value = currentValue + 1;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode
    return !(charCode > 31 && (charCode < 48 || charCode > 57));
}

function ActualizarCarrito(IdProducto, Stock) {
    let CantidadProducto = $("#Cnt-" + IdProducto).val();

    if (CantidadProducto > Stock) {
        Swal.fire({
            position: 'top-center',
            icon: 'warning',
            title: 'Excede la cantidad de productos disponibles',
            showConfirmButton: false,
            timer: 2000
        })
    }
    else {

        $.ajax({
            url: "/Producto/ActualizarCarrito",
            type: "POST",
            data: {
                "IdProducto": IdProducto,
                "CantidadProducto": CantidadProducto
            },
            dataType: 'json',
            success: function (res) {

                Swal.fire({
                    position: 'top-center',
                    icon: 'success',
                    title: 'Producto actualizado en el carrito!',
                    showConfirmButton: false,
                    timer: 2000
                })

                setTimeout(function () {
                    window.location.href = "/Home/PantallaPrincipal";
                }, 2200);

            }
        });

    }
}