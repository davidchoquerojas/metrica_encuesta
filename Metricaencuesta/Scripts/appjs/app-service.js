app.service("AppService", function () {
    this.states = function () {
        return [{ code: "A", text: "Activo" }, { code: "I", text: "Inactivo" }];
    },
    this.statesE = function () {
        return [{ code: "C", text: "Creado" }, { code: "F", text: "Finalizado" }];
    },
    this.userType = function () {
        return [{ code: "Col", text: "Colaborador (a)" }, { code: "Adm", text: "Administrador (a)" }];
    },
    this.asisType = function () {
        return [{ code: "Ing", text: "Inicio de Servicio" }, { code: "Sal", text: "Fin de Servicio" }];
    },
    this.getEvaluadorId = function () {
        return document.getElementById("id_evaluador").value;
    },
    this.getUserId = function () {
        return document.getElementById("id_empleado").value;
    },
    this.getEncuestaId = function () {
        return document.getElementById("id_encuesta").value;
    },
    this.isMobile = function () {
        var device = navigator.userAgent;
        if (device.match(/Iphone/i) || device.match(/Ipod/i) || device.match(/Android/i) || device.match(/J2ME/i) || device.match(/BlackBerry/i) || device.match(/iPhone|iPad|iPod/i) || device.match(/Opera Mini/i) || device.match(/IEMobile/i) || device.match(/Mobile/i) || device.match(/Windows Phone/i) || device.match(/windows mobile/i) || device.match(/windows ce/i) || device.match(/webOS/i) || device.match(/palm/i) || device.match(/bada/i) || device.match(/series60/i) || device.match(/nokia/i) || device.match(/symbian/i) || device.match(/HTC/i))
            return true;
        else 
            return false;
    },
    this.showMessage =function(title,message){
        var object = document.createElement("div");
        object.className = "alert alert-success respuesta";
        object.setAttribute("role","alert");
        object.innerHTML = "<p><strong>"+title+" </strong>"+message+"</p>";
        document.body.insertBefore(object,null);
        window.setTimeout(function(){
            object.style.display = "none";
        }, 5000);
    }
});
app.filter('totime', function () {
    return function (jsonDate) {
        var parser = jsonDate.replace(/\/Date\((-?\d+)\)\//, '$1');
        return new Date(parseInt(parser));
    };

});
try {
    var tipo_usuario = document.getElementById("tipo_usuario");
    if (tipo_usuario.value == "Col")
        document.getElementById("home_direct").style.display = "none";
}catch(e){
    console.log("OK");
}
