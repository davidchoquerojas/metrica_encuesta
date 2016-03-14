﻿app.controller("AppCtrl", function($scope, AppService, AppFctry) {
    document.querySelector("#fileImageEmpleado").addEventListener("change", function(e) {
        var file = e.target.files[0];
        var reader = new FileReader();
        reader.onload = function () {
            document.getElementById("foto_profile").src = reader.result;
            var empleado = {
                id_empleado : document.getElementById("id_empleado").value,
                foto : reader.result
            };
            AppFctry.updateEmpleadoFoto({ id: empleado.id_empleado }, empleado).$promise.then(function (res) {
                console.log(res);
                if (res.length > 0)
                    AppService.showMessage("OK! ", "Usuario " + empleado.nombres + " Se actualizo la foto correctamente.");
            });
            console.log(reader.result);
        }
        reader.readAsDataURL(file);
    });
    
});