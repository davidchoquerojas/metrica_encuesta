﻿//P = padre
//H = hijo
app.controller("ResultadoCtrl", function ($scope, AppService, AppFctry) {
    var id_evaluador = AppService.getEvaluadorId();
    $scope.screen = 1;
    $scope.listEncuesta = [];
    $scope.listColaborador = [];
    var encuestaMarcador = [];
    var configEncuesta = function (encuesta) {
        $scope.listEncuesta = [];
        var index = 0;
        for (var i = 0; i < encuesta.length;i++) {
            var entity = {
                descripcion: encuesta[i].descripcion,
                id_gpregunta: encuesta[i].id_gpregunta,
                id_pregunta: encuesta[i].id_pregunta,
                puntaje:null,
                tipo: "P",
                index: -1
            }
            ++index;
            $scope.listEncuesta.push(entity);
            for (var c = 0; c < encuesta[i].subencuesta.length;c++) {
                var entitysub = {
                    descripcion: encuesta[i].subencuesta[c].descripcion,
                    id_gpregunta: encuesta[i].subencuesta[c].id_gpregunta,
                    id_pregunta: encuesta[i].subencuesta[c].id_pregunta,
                    puntaje:null,
                    tipo: "H",
                    index: index
                }
                $scope.listEncuesta.push(entitysub);
                index++;
            }
        }
    };
    var listEncuestaCuestioanario = function () {
        AppFctry.getEncuesta(function (res) {
            configEncuesta(res);
            encuestaMarcador = $scope.listEncuesta;
        });
    }
    //execute listEncuestaCuestioanario()
    listEncuestaCuestioanario();

    $scope.chekMe = function (value,index) {
        encuestaMarcador[index].puntaje = value;
    }
    //verificar si ya existe una evaluacion para un colaborador
    $scope.chekMask = function (id_empleado) {
        if (id_empleado != undefined)
            chekMaskEvaluacion(id_empleado);
    }
    AppFctry.getByEvaluador({ id: id_evaluador }, function (res) {
        $scope.listColaborador = res;
    });
    var clear = function () {
        $scope.id_empleado = undefined;
        $scope.comentario = "";
        $("html, body").animate({ scrollTop: "0px" });
        listEncuestaCuestioanario();
    }
    //save resultado
    var resultado = function (i) {
        this.id_encuesta = AppService.getEncuestaId();
        this.id_evaluador = id_evaluador,
        this.id_empleado = $scope.id_empleado,
        this.id_pregunta = encuestaMarcador[i].id_pregunta,
        this.puntaje = encuestaMarcador[i].puntaje,
        this.comentario = $scope.comentario
    }

    var encuesta = function (i) {
        this.id_gpregunta = encuestaMarcador[i].id_gpregunta,
        this.id_pregunta = encuestaMarcador[i].id_pregunta,
        this.descripcion = encuestaMarcador[i].descripcion,
        this.puntaje = encuestaMarcador[i].puntaje
    }

    function chekMaskEvaluacion(id_empleado) {
        AppFctry.getEncuestaByColaborador({ id: id_empleado }, function (res) {
            if (parseInt(res[0])) {
                $scope.id_empleado = undefined;
                AppService.showMessage("Aviso! ", "Ya existe una evalución para el colaborador con el id: " + id_empleado);
                clear();
            }
        });
    }

    $scope.save = function () {
        var formValidator = true;
        var saveEncuesta = [];
        var saveResultado = [];
        for (var i = 0; i < encuestaMarcador.length; i++) {
            if (encuestaMarcador[i].tipo == "P")
                saveEncuesta.push(new encuesta(i));
            if (encuestaMarcador[i].tipo == "H" && encuestaMarcador[i].puntaje > 0) {
                saveResultado.push(new resultado(i));
                saveEncuesta.push(new encuesta(i));
            } else {
                if (encuestaMarcador[i].tipo != "P") {
                    formValidator = false;
                    alert("Por favor, revise y marque todas las casillas");
                    break;
                }
            }
           
        }
        if (formValidator == true) {
            $("#alert").modal("show");
            AppFctry.saveEncuesta({ o: saveResultado, e: saveEncuesta }).$promise.then(function (res) {
                if (res[0] == "OK") {
                    $("#alert").modal("hide");
                    AppService.showMessage("OK! ", "Encuesta realizado correctamente, para el colaborador con id: " + $scope.id_empleado);
                    clear();
                }
            });
        }
    }
    $scope.new = function () {
        clear();
    }
});