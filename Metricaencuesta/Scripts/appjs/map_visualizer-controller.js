var position = {
    lat: null,
    lng: null
};
try {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (location) {
            position.lat = location.coords.latitude;
            position.lng = location.coords.longitude
            initMap(null,position,1);
        }, function () {
            console.log(error);
        });
    } else {
        console.log("no support");
    }
} catch (e) {
    console.log(e);
}
app.controller("mapCtrl", function ($scope, AppFctry, AppService) {
    //variables
    $scope.asistencia = {};
    $scope.asisType = AppService.asisType();
    $scope.listEmploye = []
    $scope.toggle = 1;
    //eventos
    AppFctry.getEmpleado({ id: 0, estado: "A" }, function (res) {
        $scope.listEmploye = res;
    });
    $scope.viewMarker = function (asistencia) {
        AppFctry.getAsistenciaForMaps({ asistencia: asistencia }).$promise.then(function (res) {
            initMap(res, position, 2);
        });
    }
    $scope.toggleIn = function () {
        $("#box-control").animate({ "top": "-34px" }, "slow");
        $scope.toggle = 2;
    }
    $scope.toggleOn = function () {
        $("#box-control").animate({ "top": "0px" }, "slow");
        $scope.toggle = 1;
    }
    //funciones
});
function initMap(listAsistencia, position, steep) {
    var myLatlng = new google.maps.LatLng(position.lat, position.lng);
    var mapOptions = {
        zoom: 16,
        center: myLatlng
    }
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
    var contentString = "Yo estoy aqui.";
    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });
    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: "Administrador",
        draggable: true,
        zIndex:1
    });
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
    if (listAsistencia != null)
        setMarker(map, listAsistencia);
        
}
function setMarker(map,listAsistencia) {
    angular.forEach(listAsistencia, function (value, key) {
        var contentString = "<h5>" + value.empleado.apellidos + ", " + value.empleado.nombres + "</h5><label>Entrada:</label> " + value.empleado.hora_ingreso + " <br>";
        contentString += "<label>Salida:</label> " + value.empleado.hora_salida + "<hr> <label> Marcó : " + value.hora_asistencia+"</label>";
        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });
        var marker = new google.maps.Marker({
            position: { lat: parseFloat(value.latitud), lng: parseFloat(value.longitud) },
            map: map,
            title: value.empleado.apellidos + ", " + value.empleado.nombres,
            draggable: true,
            label:value.empleado.apellidos + ", " + value.empleado.nombres,
            zIndex: key+1
        });
        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });
    }, console.info("no items!"));
}