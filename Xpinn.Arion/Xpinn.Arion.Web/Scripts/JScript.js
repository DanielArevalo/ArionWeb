/// Visualiza la imagen de carga para registro o edicion de informacion
function Loading() {
    if (Page_ClientValidate()) {
        var imgLoading = document.getElementById("imgLoading");
        imgLoading.style.visibility = "visible";
       

    }
}

/// Visualiza la imagen de carga para consulta de informacion
function LoadingList() {
    var imgLoading = document.getElementById("imgLoading");
    imgLoading.style.visibility = "visible";
}

/// Visualiza la imagen de carga para consulta de informacion
function OcultaLoading() {
    var imgLoading = document.getElementById("imgLoading");
    imgLoading.Visible = "false";
}

//Convierte los numero y los devuelve en miles

var formatNumber = {
    separador: ".", // separador para los miles
    sepDecimal: ",", // separador para los decimales
    formatear: function (num) {
        num += '';
        var splitStr = num.split('.');
        var splitLeft = splitStr[0];
        var splitRight = splitStr.length > 1 ? this.sepDecimal + splitStr[1] : '';
        var regx = /(\d+)(\d{3})/;
        while (regx.test(splitLeft)) {
            splitLeft = splitLeft.replace(regx, '$1' + this.separador + '$2');
        }
        return this.simbol + splitLeft + splitRight;
    },
    new: function (num, simbol) {
        this.simbol = simbol || '';
        return this.formatear(num);
    }
}
//Mira que el resultano no sea nulo
function isStringNullOrEmpty(val) {
    switch (val) {
        case "":
        case 0:
        case "0":
        case null:
        case false:
        case undefined:
        case typeof this === 'undefined':
            return true;
        default:
            return false;
    }
}
//function disable(Mask) {
//    var behavior = $find(Mask); // "MaskedEditExtenderEx" - BehaviorID
//    // to prevent the base dispose() method call - it removes the behavior from components list
//    //------------------------------------------------------------------------
////    var savedDispose = AjaxControlToolkit.MaskedEditBehavior.callBaseMethod;
////    AjaxControlToolkit.MaskedEditBehavior.callBaseMethod = function (instance, name) {
////    };
//    //------------------------------------------------------------------------
//    behavior.dispose();
//    // restore the base dispose() method
////    AjaxControlToolkit.MaskedEditBehavior.callBaseMethod = savedDispose;
//}
//function enable() { // enable it again
//    var behavior = $find("MaskedEditExtenderEx"); // "MaskedEditExtenderEx" - BehaviorID
//    behavior.initialize();
//}

//function clearTextBox(textBoxID) {
//    document.getElementById(textBoxID).value = "ss";
//}

function blur7(textbox) {

    var str2 = textbox[0].textContent;
    var formateado = "";
    var str1 = parseInt(str2.replace(/\./g, ""));
    if (str1 > 0) {
        var str = str1.toString();
        var long = str.length;
        var cen = str.substring(long - 3, long);
        var mil = str.substring(long - 6, long - 3);
        mill = str.substring(0, long - 6);

        if (long > 0 && long <= 3) { formateado = parseInt(cen); }
        else if (long > 3 && long <= 6) { formateado = parseInt(mil) + "." + cen; }
        else if (long > 6 && long <= 9) { formateado = parseInt(mill) + "." + mil + "." + cen; }
        else { formateado = "0"; }
    }
    else {
        var formateado = "0";
    }

    var s = document.getElementById(textbox.id);
    document.getElementById(textbox[0].id).value = formateado;
    //document.getElementById("<%= TxtValor3.ClientID %>").value = formateado;  

}


function FormatearNumerosMientrasEscribes(e) {

    e = String(e);
    var valida = "";
    if (e[0] == "-")
        valida = e[0];

    var valor = String(e);
    valor = parseInt(valor.replace(/[.,$}/\-/]/g, "")).toString();
    valor = valor.replace(/\D/g, "")
        .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");

    return valida + valor;
}

function autoResize(id) {
    var newheight;
    // var newwidth;

    if (document.getElementById) {
        newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
        //  newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
    }

    document.getElementById(id).height = (newheight) + "px";
    //           document.getElementById(id).width = (newwidth) + "px";
}


//*********************************************************************************
//********************INFORMACION PARA ANALISIS DE CREDITOS --NO QUITAR-- *********
//*********************************************************************************

var contador = 3;
var estado = true;
$(document).ready(function () {
    formatear();
    debugger;
    if (!$("#btnGuardar").is(":visible"))
        permiteEditar();
});

// ID Primer Concepto "INGRESOS"
var ingresos = ["cphMain_TxtIngresos1", "cphMain_TxtIngresos2", "cphMain_TxtIngresos3", "cphMain_TxtIngresos4"];
var otrosIngresos = ["cphMain_TxtOtrosIngresos1", "cphMain_TxtOtrosIngresos2", "cphMain_TxtOtrosIngresos3", "cphMain_TxtOtrosIngresos4"];
var arrendamientos = ["cphMain_txtArrendamiento1", "cphMain_txtArrendamiento2", "cphMain_txtArrendamiento3", "cphMain_txtArrendamiento4"];
var honorarios = ["cphMain_txtHonorario1", "cphMain_txtHonorario2", "cphMain_txtHonorario3", "cphMain_txtHonorario4"];
var ingresosBrutos = ["cphMain_TxtIngresosBrutos1", "cphMain_TxtIngresosBrutos2", "cphMain_TxtIngresosBrutos3", "cphMain_TxtIngresosBrutos4"];

// ID Segundo Concepto "EGRESOS"
// deduccionesSociales  => Salud / Pension
var deduccionesSociales = ["cphMain_TxtDeduccionesSocial1", "cphMain_TxtDeduccionesSocial2", "cphMain_TxtDeduccionesSocial3", "cphMain_TxtDeduccionesSocial4"];
var cuotasFinanPrin = ["cphMain_TxtCuotasFinanPrincipal1", "cphMain_TxtCuotasFinanPrincipal2", "cphMain_TxtCuotasFinanPrincipal3", "cphMain_TxtCuotasFinanPrincipal4"];
var cuotasFinanDeudor = ["cphMain_TxtCuotasFinanDeudor1", "cphMain_TxtCuotasFinanDeudor2", "cphMain_TxtCuotasFinanDeudor3", "cphMain_TxtCuotasFinanDeudor4"];
var gastosFamiliares = ["cphMain_TxtGastosFamiliares1", "cphMain_TxtGastosFamiliares2", "cphMain_TxtGastosFamiliares3", "cphMain_TxtGastosFamiliares4"];
var aportes = ["cphMain_txtAporte1", "cphMain_txtAporte2", "cphMain_txtAporte3", "cphMain_txtAporte4"];
var otrosDsctos = ["cphMain_txtOtrosDsctos1", "cphMain_txtOtrosDsctos2", "cphMain_txtOtrosDsctos3", "cphMain_txtOtrosDsctos4"];
var creditos = ["cphMain_txtCreditoVig1", "cphMain_txtCreditoVig2", "cphMain_txtCreditoVig3", "cphMain_txtCreditoVig4"];
var servicios = ["cphMain_txtServicio1", "cphMain_txtServicio2", "cphMain_txtServicio3", "cphMain_txtServicio4"];
var deudasTerceros = ["cphMain_txtDeudasTer1", "cphMain_txtDeudasTer2", "cphMain_txtDeudasTer3", "cphMain_txtDeudasTer4"];
var proteccion = ["cphMain_txtProtSalarial1", "cphMain_txtProtSalarial2", "cphMain_txtProtSalarial3", "cphMain_txtProtSalarial4"];
var totalEgresos = ["cphMain_TxtB1", "cphMain_TxtB2", "cphMain_TxtB3", "cphMain_TxtB4"];

// ID Tercer Concepto "INGRESO NETO MENSUAL"
var ingresosMensuales = ["cphMain_TxtIngresosMensual1", "cphMain_TxtIngresosMensual2", "cphMain_TxtIngresosMensual3", "cphMain_TxtIngresosMensual4"];

// ID Cuarto Concepto "INGRESO NETO TRIMESTRAL"
var ingresosTrimestrales = ["cphMain_TxtIngresoTrimestral1", "cphMain_TxtIngresoTrimestral2", "cphMain_TxtIngresoTrimestral3", "cphMain_TxtIngresoTrimestral4"];

var capacidaDesc = ["cphMain_txtCapDesc1", "cphMain_txtCapDesc2", "cphMain_txtCapDesc3", "cphMain_txtCapDesc4"];
var capacidadPago = ["cphMain_txtCapPago1", "cphMain_txtCapPago2", "cphMain_txtCapPago3", "cphMain_txtCapPago4"];
var general;



function CalcularSumaTotalVariosControlesAControlUnico(arrayIDControlesCalcular, IDControlDestino) {

    var total = 0;
    var x = document.getElementById(IDControlDestino);

    for (var i = 0, max = arrayIDControlesCalcular.length; i < max; i++) {
        var temp = parseInt(total);

        temp += parseInt(document.getElementById(arrayIDControlesCalcular[i]).value.replace(/\./g, ""));

        if (isNaN(temp)) {
            continue;
        }
        total = parseInt(temp);
    }

    x.value = FormatearNumerosMientrasEscribes(total);
    formatear();

}

function calcular(concepto, columna, opcion, idControl) {
    if (concepto == "Ingreso") {
        CalcularSumaTotalVariosControlesAControlUnico(
            [ingresos[columna], otrosIngresos[columna], arrendamientos[columna], honorarios[columna]],
            ingresosBrutos[columna]);
        CalcularSumaTotalVariosControlesAControlUnico([
            deduccionesSociales[columna], cuotasFinanPrin[columna], cuotasFinanDeudor[columna],
            gastosFamiliares[columna],
            aportes[columna], otrosDsctos[columna], creditos[columna], servicios[columna], deudasTerceros[columna]

        ],
            totalEgresos[columna]);
    }
    if (concepto == "Egreso") {
        CalcularSumaTotalVariosControlesAControlUnico([
            deduccionesSociales[columna], cuotasFinanPrin[columna], cuotasFinanDeudor[columna],
            gastosFamiliares[columna],
            aportes[columna], otrosDsctos[columna], creditos[columna], servicios[columna], deudasTerceros[columna]
        ],
            totalEgresos[columna]);
    }
    if (opcion != null) {
        general = document.getElementById("cphMain_lblgeneral").textContent;
        if (general == "0" || general == "") {
            if (idControl == null)
                CalcularDeduccionSocial(columna);
            CalcularProteccionSalarial(columna);
            CalcularCapacidadPago(columna);
        }
    }

    CalcularDeduccionSocial(columna);
    CalcularProteccionSalarial(columna);
    CalcularCapacidadPago(columna);
    formatear();

}


function CalcularCapacidadPago(columna) {
    CalculoNetoMensual(columna);
    CalcularCuotaSobreIngreso();
    CalcularCapacidadDescuentoPago(columna);
}


function CalculoNetoMensual(columna) {
    var ingresos = parseInt(document.getElementById(ingresosBrutos[columna]).value.replace(/\./g, ""));
    var egresos = parseInt(document.getElementById(totalEgresos[columna]).value.replace(/\./g, ""));
    var total = document.getElementById(ingresosMensuales[columna]);

    if (isNaN(ingresos)) {
        ingresos = 0;
    }
    if (isNaN(egresos)) {
        egresos = 0;
    }

    total.value = FormatearNumerosMientrasEscribes(ingresos - egresos);
    formatear();
}


function CalculoNetoTrimestral(columna) {
    var total = parseInt(document.getElementById(ingresosMensuales[columna]).value.replace(/\./g, ""));
    var totalTrimestral = document.getElementById(ingresosTrimestrales[columna]);

    totalTrimestral.value = FormatearNumerosMientrasEscribes(total * 3);
    formatear();
}


function CalcularCuotaSobreIngreso() {
    var cuotaSobreIngreso = document.getElementById("cphMain_TxtCuotaIngresosNeto1");
    var cuotaCredito = parseInt(document.getElementById("cphMain_TxtCuotaCredito1").value.replace(/\./g, ""));
    var ingresosTrimestrales = parseInt(document.getElementById("cphMain_TxtIngresoTrimestral1").value.replace(/\./g, ""));
    var total = FormatearNumerosMientrasEscribes(cuotaCredito / ingresosTrimestrales);

    if (!isFinite(total)) {
        total = 0
    }

    var totalCalculo = total * 100;
    cuotaSobreIngreso.value = FormatearNumerosMientrasEscribes(totalCalculo.toFixed(2));
    formatear();
}


function CalcularDeduccionSocial(columna) {
    var ingreso;
    var saludPension;
    ingreso = document.getElementById(ingresos[columna]).value != "" ? parseFloat(document.getElementById(ingresos[columna]).value.replace(/\./g, "")) : 0;

    var calcABC = 0;
    if (ingreso > 0)
        calcABC = Math.round(ingreso * (parseInt(document.getElementById("cphMain_TxtPorcentaje").value) / 100));

    document.getElementById(deduccionesSociales[columna]).value = FormatearNumerosMientrasEscribes(calcABC);
    formatear();
}

function CalcularProteccionSalarial(columna) {
    var ingreso;
    var saludPension;
    ingreso = document.getElementById(ingresosBrutos[columna]).value.replace(/\./g, "") != "" ? parseFloat(document.getElementById(ingresosBrutos[columna]).value.replace(/\./g, "")) : 0;
    saludPension = document.getElementById(deduccionesSociales[columna]).value.replace(/\./g, "") != "" ? parseFloat(document.getElementById(deduccionesSociales[columna]).value.replace(/\./g, "")) : 0;

    var calcABC = Math.round((ingreso - saludPension) / 2);

    document.getElementById(proteccion[columna]).value = FormatearNumerosMientrasEscribes(calcABC);

}


function CalcularCapacidadDescuentoPago(columna) {
    // CAPTURA DE DATOS

    var salario = parseFloat(document.getElementById(ingresos[columna]).value.replace(/\./g, "")) || 0,
        otrosIng = parseFloat(document.getElementById(otrosIngresos[columna]).value.replace(/\./g, "")) || 0,
        aporte = parseFloat(document.getElementById(aportes[columna]).value.replace(/\./g, "")) || 0,
        otroDscto = parseFloat(document.getElementById(otrosDsctos[columna]).value.replace(/\./g, "")) || 0,
        credito = parseFloat(document.getElementById(creditos[columna]).value.replace(/\./g, "")) || 0,
        servicio = parseFloat(document.getElementById(servicios[columna]).value.replace(/\./g, "")) || 0,
        saludpension = parseFloat(document.getElementById(deduccionesSociales[columna]).value.replace(/\./g, "")) || 0,
        protecSalarial = parseFloat(document.getElementById(proteccion[columna]).value.replace(/\./g, "")) || 0,
        totalIngreso = parseFloat(document.getElementById(ingresosBrutos[columna]).value.replace(/\./g, "")) || 0,
        totalEgreso = parseFloat(document.getElementById(totalEgresos[columna]).value.replace(/\./g, "")) || 0;

    var totalDescuento = (salario + otrosIng) - (totalEgreso);
    var totalPago = totalIngreso - totalEgreso;

    document.getElementById(capacidaDesc[columna]).value = FormatearNumerosMientrasEscribes(totalDescuento);
    // RECALCULANDO DESCUBIERTO POR CAJA
    var cuotaCredito = parseFloat(document.getElementById("cphMain_TxtCuotaCredito1").value.replace(/\./g, "")) || 0;
    var valCalc = cuotaCredito - totalDescuento;
    var newValCalc = 0;
    if (valCalc > 0)
        // PENDIENTE AJUSTAR FORMATO
        newValCalc = FormatearNumerosMientrasEscribes(cuotaCredito - totalDescuento);
    else
        newValCalc = 0;
    document.getElementById("cphMain_lblDescuCaja").innerHTML = newValCalc;
    blur7($("#cphMain_lblDescuCaja"));
    document.getElementById(capacidadPago[columna]).value = FormatearNumerosMientrasEscribes(totalPago
    );
    formatear();
}

function permiteEditar() {
    for (var i = 0; i <= contador; i++) {

        $("#" + ingresos[i]).attr("readonly", true);
        $("#" + otrosIngresos[i]).attr("readonly", true);
        $("#" + arrendamientos[i]).attr("readonly", true);
        $("#" + honorarios[i]).attr("readonly", true);
        $("#" + ingresosBrutos[i]).attr("readonly", true);
        $("#" + deduccionesSociales[i]).attr("readonly", true);
        $("#" + cuotasFinanPrin[i]).attr("readonly", true);
        $("#" + cuotasFinanDeudor[i]).attr("readonly", true);
        $("#" + gastosFamiliares[i]).attr("readonly", true);
        $("#" + aportes[i]).attr("readonly", true);
        $("#" + otrosDsctos[i]).attr("readonly", true);
        $("#" + creditos[i]).attr("readonly", true);
        $("#" + servicios[i]).attr("readonly", true);
        $("#" + deudasTerceros[i]).attr("readonly", true);
        $("#" + proteccion[i]).attr("readonly", true);
        $("#" + totalEgresos[i]).attr("readonly", true);
        $("#" + ingresosMensuales[i]).attr("readonly", true);
        $("#" + ingresosTrimestrales[i]).attr("readonly", true);
        $("#" + capacidaDesc[i]).attr("readonly", true);
        $("#" + capacidadPago[i]).attr("readonly", true);
    }
}

function Toogle() {
    $("#divCuotasExtras").slideToggle();
    if (estado == true) {
        estado = false;
    } else {
        estado = true;
    }
    event.preventDefault();
}

function Toogleco() {
    $("#divCodeudores").slideToggle();
    if (estado == true) {
        estado = false;
    } else {
        estado = true;
    }
    event.preventDefault();
}
function ToogleDoc() {

    $("#divDocAnexos").slideToggle();
    if (estado == true) {
        estado = false;
    } else {
        estado = true;
    }
    event.preventDefault();
}

	function formatear() {
		var cantidad = 4;
		for (var i = 0; i < cantidad; i++) {

			$("#" + ingresos[i]).val(FormatearNumerosMientrasEscribes($("#" + ingresos[i]).val()));
			$("#" + otrosIngresos[i]).val(FormatearNumerosMientrasEscribes($("#" + otrosIngresos[i]).val()));
			$("#" + arrendamientos[i]).val(FormatearNumerosMientrasEscribes($("#" + arrendamientos[i]).val()));
			$("#" + honorarios[i]).val(FormatearNumerosMientrasEscribes($("#" + honorarios[i]).val()));
			$("#" + ingresosBrutos[i]).val(FormatearNumerosMientrasEscribes($("#" + ingresosBrutos[i]).val()));
			$("#" + deduccionesSociales[i]).val(FormatearNumerosMientrasEscribes($("#" + deduccionesSociales[i]).val()));
			$("#" + cuotasFinanPrin[i]).val(FormatearNumerosMientrasEscribes($("#" + cuotasFinanPrin[i]).val()));
			$("#" + cuotasFinanDeudor[i]).val(FormatearNumerosMientrasEscribes($("#" + cuotasFinanDeudor[i]).val()));
			$("#" + gastosFamiliares[i]).val(FormatearNumerosMientrasEscribes($("#" + gastosFamiliares[i]).val()));
			$("#" + aportes[i]).val(FormatearNumerosMientrasEscribes($("#" + aportes[i]).val()));
			$("#" + otrosDsctos[i]).val(FormatearNumerosMientrasEscribes($("#" + otrosDsctos[i]).val()));
			$("#" + creditos[i]).val(FormatearNumerosMientrasEscribes($("#" + creditos[i]).val()));
			$("#" + servicios[i]).val(FormatearNumerosMientrasEscribes($("#" + servicios[i]).val()));
			$("#" + proteccion[i]).val(FormatearNumerosMientrasEscribes($("#" + proteccion[i]).val()));
			$("#" + totalEgresos[i]).val(FormatearNumerosMientrasEscribes($("#" + totalEgresos[i]).val()));
			$("#" + ingresosMensuales[i]).val(FormatearNumerosMientrasEscribes($("#" + ingresosMensuales[i]).val()));
			$("#" + ingresosTrimestrales[i]).val(FormatearNumerosMientrasEscribes($("#" + ingresosTrimestrales[i]).val()));
			$("#" + capacidaDesc[i]).val(FormatearNumerosMientrasEscribes($("#" + capacidaDesc[i]).val()));
			$("#" + capacidadPago[i]).val(FormatearNumerosMientrasEscribes($("#" + capacidadPago[i]).val()));
		}
		$("#cphMain_TxtCuotaCredito1").val(FormatearNumerosMientrasEscribes($("#cphMain_TxtCuotaCredito1").val()));

	}

