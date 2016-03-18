app.controller("DocumentoCtrl", function ($scope, $location, $timeout, AppFctry, AppService) {
    //variables
    $scope.asisType = AppService.asisType();
    $scope.listDocumento = [];
    $scope.documento = {};
    $scope.listEmpleado = [];
    //metodos
    $scope.save = function (documento) {
        var data = new FormData();
        data.append("ruta", jQuery("#txt_archivo").get(0).files[0]);
        data.append("nombre", documento.nombre);
        data.append("id_documento", documento.id_documento);

        $.ajax({type: "POST",url: baseurl + '/Documento/save',
            contentType: false,processData: false,data: data,
            success: function (result) {
                if (documento.id_documento == undefined)
                    $scope.listDocumento.push(result);
                else
                    getDocumento();
                AppService.showMessage("OK! ", "Documento grabado correctamente.");
                $scope.documento = {};
            },
            error: function (error) {
                AppService.showMessage("ERROR! ", error);
            }
        });
    }
    
    var getDocumento = function () {
        AppFctry.getDocumento(function (res) {
            $scope.listDocumento = res;
        });
    }

    //load
    getDocumento();

    $scope.edit = function (documento) {
        $scope.documento = documento;
    }
    $scope.viewUser = function (documento) {
        console.log(documento);
        AppFctry.getUsersByDocumento({id:null}, documento).$promise.then(function (res) {
            $scope.listEmpleado = res;
            $("#modal_usuario").modal("show");
        });
    }
});