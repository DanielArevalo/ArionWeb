
        function buscarProcuraduria() {
            var frame = document.getElementById('cphMain_frmPrint');
            alert(frame);
            var form = window.frames["cphMain_frmPrint"].document.forms[0];
            alert(form);
        }
        function buscarRegistraduria() {
            var frame = document.getElementById('cphMain_frmPrint');
            alert(frame);
            var form = window.frames["cphMain_frmPrint"].document.forms[0];
            alert(form);
        }
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                var hoy = new Date();
                alert("Eliga una fecha inferior a la Actual! " + hoy.toDateString());
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            }
        }
        function PanelClick(sender, e) {
        }

        function ActiveTabChanged(sender, e) {
        }

        var HighlightAnimations = {};

        function Highlight(el) {
            if (HighlightAnimations[el.uniqueID] == null) {
                HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                    AnimationName: "color",
                    duration: 0.5,
                    property: "style",
                    propertyKey: "backgroundColor",
                    startValue: "#FFFF90",
                    endValue: "#FFFFFF"
                }, el);
            }
            HighlightAnimations[el.uniqueID].stop();
            HighlightAnimations[el.uniqueID].play();
        }

        function ToggleHidden(value) {
        }

        function mpeSeleccionOnOk() {
        }

        function mpeSeleccionOnCancel() {
        }

        function KeyBackspace(keyStroke) {
            isNetscape = (document.layers);
            eventChooser = (isNetscape) ? keyStroke.which : event.keyCode;
            if (eventChooser == 13) {
                return false;
            }
        }
        document.onkeypress = KeyBackspace;

        document.onkeydown = function () {
            if (window.event && window.event.keyCode == 8) {
                window.event.keyCode = 505;
            }
            if (window.event && window.event.keyCode == 505) {
                return false;
            }
        }
        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            return ((tecla > 47 && tecla < 58) || tecla == 46);
        }
$('ul.tabs li a:first').addClass('active');
	$('.secciones article').hide();
	$('.secciones article:first').show();

	$('ul.tabs li a').click(function(){
		$('ul.tabs li a').removeClass('active');
		$(this).addClass('active');
		$('.secciones article').hide();

		var activeTab = $(this).attr('href');
		$(activeTab).show();
		return false;
	});
        function blur7(textbox) {
            var str = textbox.value;
            //var str = int.value;
            var formateado = "";
            str = str.replace(/\./g, "");
            if (str > 0) {
                str = parseInt(str);
                str = str.toString();

                if (str.length > 12)
                { str = str.substring(0, 12); }

                var long = str.length;
                var cen = str.substring(long - 3, long);
                var mil = str.substring(long - 6, long - 3);
                var mill = str.substring(long - 9, long - 6);
                var milmill = str.substring(0, long - 9);

                if (long > 0 && long <= 3)
                { formateado = parseInt(cen); }
                else if (long > 3 && long <= 6)
                { formateado = parseInt(mil) + "." + cen; }
                else if (long > 6 && long <= 9)
                { formateado = parseInt(mill) + "." + mil + "." + cen; }
                else if (long > 9 && long <= 12)
                { formateado = parseInt(milmill) + "." + mill + "." + mil + "." + cen; }
                else
                { formateado = "0"; }
            }
            else { formateado = "0"; }
            return formateado;
        }

        function TotalizarIngresosSoli(textbox) {

            var txtsueldo_solijq = document.getElementById('<%= txtsueldo_soli.ClientID %>');
            var txthonorario_solijq = document.getElementById('<%= txthonorario_soli.ClientID %>');
            var txtarrenda_solijq = document.getElementById('<%= txtarrenda_soli.ClientID %>');
            var txtotrosIng_solijq = document.getElementById('<%= txtotrosIng_soli.ClientID %>');

            var txttotalING_solijq = document.getElementById('<%= txttotalING_soli.ClientID %>');

            var A = txtsueldo_solijq.value == "" || txtsueldo_solijq.value == null ? "0" : replaceAll(".", "", txtsueldo_solijq.value);
            var E = txthonorario_solijq.value == "" || txthonorario_solijq.value == null ? "0" : replaceAll(".", "", txthonorario_solijq.value);
            var I = txtarrenda_solijq.value == "" || txtarrenda_solijq.value == null ? "0" : replaceAll(".", "", txtarrenda_solijq.value);
            var O = txtotrosIng_solijq.value == "" || txtotrosIng_solijq.value == null ? "0" : replaceAll(".", "", txtotrosIng_solijq.value);

            var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O);

            txttotalING_solijq.value = totalGeneral;
            var hdtotalING_soli = document.getElementById('<%= hdtotalING_soli.ClientID %>');
            hdtotalING_soli.value = totalGeneral;
            blur7(textbox);
            blur7(document.getElementById('<%= txttotalING_soli.ClientID %>'));

        }

        function TotalizarIngresosCony(textbox) {

            var txtsueldo_cony = document.getElementById('<%= txtsueldo_cony.ClientID %>');
            var txthonorario_cony = document.getElementById('<%= txthonorario_cony.ClientID %>');
            var txtarrenda_cony = document.getElementById('<%= txtarrenda_cony.ClientID %>');
            var txtotrosIng_cony = document.getElementById('<%= txtotrosIng_cony.ClientID %>');

            var txttotalING_cony = document.getElementById('<%= txttotalING_cony.ClientID %>');

            var A = txtsueldo_cony.value == "" || txtsueldo_cony.value == null ? "0" : replaceAll(".", "", txtsueldo_cony.value);
            var E = txthonorario_cony.value == "" || txthonorario_cony.value == null ? "0" : replaceAll(".", "", txthonorario_cony.value);
            var I = txtarrenda_cony.value == "" || txtarrenda_cony.value == null ? "0" : replaceAll(".", "", txtarrenda_cony.value);
            var O = txtotrosIng_cony.value == "" || txtotrosIng_cony.value == null ? "0" : replaceAll(".", "", txtotrosIng_cony.value);

            var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O);

            txttotalING_cony.value = totalGeneral;
            var hdtotalING_cony = document.getElementById('<%= hdtotalING_cony.ClientID %>');
            hdtotalING_cony.value = totalGeneral;
            blur7(textbox);
            blur7(document.getElementById('<%= txttotalING_cony.ClientID %>'));
        }

        function TotalizarEgresosSoli(textbox) {

            var txthipoteca_soli = document.getElementById('<%= txthipoteca_soli.ClientID %>');
            var txttarjeta_soli = document.getElementById('<%= txttarjeta_soli.ClientID %>');
            var txtotrosPres_soli = document.getElementById('<%= txtotrosPres_soli.ClientID %>');
            var txtgastosFam_soli = document.getElementById('<%= txtgastosFam_soli.ClientID %>');
            var txtnomina_soli = document.getElementById('<%= txtnomina_soli.ClientID %>');

            var txttotalEGR_soli = document.getElementById('<%= txttotalEGR_soli.ClientID %>');

            var A = txthipoteca_soli.value == "" || txthipoteca_soli.value == null ? "0" : replaceAll(".", "", txthipoteca_soli.value);
            var E = txttarjeta_soli.value == "" || txttarjeta_soli.value == null ? "0" : replaceAll(".", "", txttarjeta_soli.value);
            var I = txtotrosPres_soli.value == "" || txtotrosPres_soli.value == null ? "0" : replaceAll(".", "", txtotrosPres_soli.value);
            var O = txtgastosFam_soli.value == "" || txtgastosFam_soli.value == null ? "0" : replaceAll(".", "", txtgastosFam_soli.value);
            var U = txtnomina_soli.value == "" || txtnomina_soli.value == null ? "0" : replaceAll(".", "", txtnomina_soli.value);

            var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O) + +parseFloat(U);

            txttotalEGR_soli.value = totalGeneral;
            var hdtotalEGR_soli = document.getElementById('<%= hdtotalEGR_soli.ClientID %>');
            hdtotalEGR_soli.value = totalGeneral;
            blur7(textbox);
            blur7(document.getElementById('<%= txttotalEGR_soli.ClientID %>'));
        }

        function TotalizarEgresosCony(textbox) {

            var txthipoteca_cony = document.getElementById('<%= txthipoteca_cony.ClientID %>');
            var txttarjeta_cony = document.getElementById('<%= txttarjeta_cony.ClientID %>');
            var txtotrosPres_cony = document.getElementById('<%= txtotrosPres_cony.ClientID %>');
            var txtgastosFam_cony = document.getElementById('<%= txtgastosFam_cony.ClientID %>');
            var txtnomina_cony = document.getElementById('<%= txtnomina_cony.ClientID %>');

            var txttotalEGR_cony = document.getElementById('<%= txttotalEGR_cony.ClientID %>');

            var A = txthipoteca_cony.value == "" || txthipoteca_cony.value == null ? "0" : replaceAll(".", "", txthipoteca_cony.value);
            var E = txttarjeta_cony.value == "" || txttarjeta_cony.value == null ? "0" : replaceAll(".", "", txttarjeta_cony.value);
            var I = txtotrosPres_cony.value == "" || txtotrosPres_cony.value == null ? "0" : replaceAll(".", "", txtotrosPres_cony.value);
            var O = txtgastosFam_cony.value == "" || txtgastosFam_cony.value == null ? "0" : replaceAll(".", "", txtgastosFam_cony.value);
            var U = txtnomina_cony.value == "" || txtnomina_cony.value == null ? "0" : replaceAll(".", "", txtnomina_cony.value);

            var totalGeneral = parseFloat(A) + parseFloat(E) + parseFloat(I) + parseFloat(O) + +parseFloat(U);

            txttotalEGR_cony.value = totalGeneral;
            var hdtotalEGR_cony = document.getElementById('<%= hdtotalEGR_cony.ClientID %>');
            hdtotalEGR_cony.value = totalGeneral;
            blur7(textbox);
            blur7(document.getElementById('<%= txttotalEGR_cony.ClientID %>'));
        }


        function replaceAll(find, replace, str) {
            while (str.indexOf(find) > -1) {
                str = str.replace(find, replace);
            }
            return str;
        }

        function MostrarCIIUPrincipal(pDescripcion) {
            document.getElementById('<%= txtActividadCIIU.ClientID %>').value = pDescripcion;
        }

        function MostrarCIIUPrincipalEmp(pCodigo,pDescripcion) {
            document.getElementById('<%= txtCIIUEmpresa.ClientID %>').value = pDescripcion;
            document.getElementById('<%=hfActEmpresa.ClientID%>').value = pCodigo;
        }
        
        function InfoCorrespondencia() {
            var chInfoResidencia = document.getElementById('<%= chInfoResidencia.ClientID %>');

            var ddlTipoUbic = document.getElementById('<%= ddlTipoUbic.ClientID %>');
            var txtDireccionE = document.getElementById('<%= txtDireccionE.ClientID %>');
            var ddlCiudadResidencia = document.getElementById('<%= ddlCiudadResidencia.ClientID %>');
            var ddlBarrioResid = document.getElementById('<%= ddlBarrioResid.ClientID %>');
            var txtTelefonoE = document.getElementById('<%= txtTelefonoE.ClientID %>');
            
            var ddlTipoUbicCorr = document.getElementById('<%= ddlTipoUbicCorr.ClientID %>');
            var txtDirCorrespondencia = document.getElementById('<%= txtDirCorrespondencia.ClientID %>'); 
            var ddlCiuCorrespondencia = document.getElementById('<%= ddlCiuCorrespondencia.ClientID %>');
            var ddlBarrioCorrespondencia = document.getElementById('<%= ddlBarrioCorrespondencia.ClientID %>');
            var txtTelCorrespondencia = document.getElementById('<%= txtTelCorrespondencia.ClientID %>');

            if (chInfoResidencia == null || chInfoResidencia == null)
                alert('Checkbox no encontrado');
            else {
                if (chInfoResidencia.checked)
                {
                    ddlTipoUbicCorr.value = ddlTipoUbic.value;
                    txtDirCorrespondencia.value = txtDireccionE.value;
                    ddlCiuCorrespondencia.value = ddlCiudadResidencia.value;
                    ddlBarrioCorrespondencia.value = ddlBarrioResid.value;
                    txtTelCorrespondencia.value = txtTelefonoE.value;
                }
            }
        }
              $(document).ready(function(){
	$('ul.tabs li a:first').addClass('active');
	$('.secciones article').hide();
	$('.secciones article:first').show();

	$('ul.tabs li a').click(function(){
		$('ul.tabs li a').removeClass('active');
		$(this).addClass('active');
		$('.secciones article').hide();

		var activeTab = $(this).attr('href');
		$(activeTab).show();
		return false;
	});
}); 
        //        function obtener_localizacion() {
        //            if (navigator.geolocation) {
        //                navigator.geolocation.getCurrentPosition(mostrar_mapa, gestiona_errores);
        //            } else {
        //                alert('Tu navegador no soporta la API de geolocalizacion');
        //            }
        //        }
        //        function mostrar_mapa(position) { 
        //            var latitud = position.coords.latitude;
        //            var longitud = position.coords.longitude;
        //            var TextBox1 = document.getElementById('%=TextBox1.ClientID%>');
        //            TextBox1.value = latitud;
        //            var TextBox2 = document.getElementById('%=TextBox2.ClientID%>');
        //            TextBox2.value = longitud;
        //            TextBox1.disabled = true;
        //            TextBox2.disabled = true;
        //            var boton = document.getElementById('btnAceptar');

        //            return true;
        //        }

        //        function gestiona_errores(err) {
        //            if (err.code == 0) {
        //                alert("error desconocido");
        //            }
        //            if (err.code == 1) {
        //                alert("El usuario no ha compartido su posicion");
        //            }
        //            if (err.code == 2) {
        //                alert("no se puede obtener la posicion actual");
        //            }
        //            if (err.code == 3) {
        //                alert("timeout recibiendo la posicion");
        //            }
        //        }
        //        function z_metjsClick() {
        //            navigator.geolocation.getCurrentPosition(mostrar_mapa, gestiona_errores);
        //        }