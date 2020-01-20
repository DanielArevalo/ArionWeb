using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Services;
using Xpinn.Servicios.Entities;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;

/// <summary>
/// Funciones para uso transversal en capa de presentacion
/// </summary>
public class GlobalWeb : System.Web.UI.Page
{
    public ExcepcionBusiness BOexcepcion;
    private Acceso opcionActual;
    Xpinn.Seguridad.Services.UsuarioService service = new Xpinn.Seguridad.Services.UsuarioService();

    protected enum Pagina { Nuevo = 1, Lista = 2, Detalle = 3, Editar = 4, Modificar = 5 };

    public static string MOV_GRAL_CRED_PRODUC = "idMovGralCredito";
    protected ConnectionDataBase dbConnectionFactory;
    Configuracion global = new Configuracion();
    public static string gFormatoFecha = "dd/MM/yyyy";
    public static string gSeparadorDecimal = ",";
    public static string gSeparadorMiles = ".";
    public static Boolean bMostrarPDF = false;
    public static string gControlarCompCaja = "0";
    public static string gEdadMinima = "";
    public static string gEdadMaxima = "";
    public static string gMarcarRecogerDesembolso = "";
    public static string gValidarTasaReestructuracion = "";
    public static string guserID = "";
    public static string gclave = "";
    public static string gmenuRetractil = "";
    public static readonly string espacioBlancoHTML = "&nbsp;";
    protected Dictionary<ParametroCorreo, string> parametrosFormatoCorreo;
    
    protected string TextoLaberError
    {
        get
        {
            return ((Label)Master.FindControl("lblError")).Text;
        }
    }

    Usuario _usuario;
    public Usuario Usuario
    {
        get
        {
            if (_usuario == null)
            {
                _usuario = Session["Usuario"] as Usuario;
            }

            return _usuario;
        }
        private set
        {
            _usuario = value;
        }
    }

    public GlobalWeb()
    {
        try
        {
            BOexcepcion = new Xpinn.Util.ExcepcionBusiness();
            bMostrarPDF = Convert.ToBoolean(global.ObtenerValorConfig("MostrarPDF"));
            gFormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
            gSeparadorDecimal = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (gSeparadorDecimal == ".")
            {
                gSeparadorMiles = ",";
            }
            else
            {
                gSeparadorMiles = ".";
            }
            gControlarCompCaja = global.ObtenerValorConfig("ControlarCompCaja");
            gEdadMinima = global.ObtenerValorConfig("EdadMinima");
            gEdadMaxima = global.ObtenerValorConfig("EdadMaxima");
            gMarcarRecogerDesembolso = global.ObtenerValorConfig("MarcarRecogerDesembolso");
            gValidarTasaReestructuracion = global.ObtenerValorConfig("ValidarTasaReestructuracion");
            gmenuRetractil = global.ObtenerValorConfig("menuRetractil");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "GlobalWeb", ex);
        }
    }



    public bool LlenarListaDesplegablesConEnumSinDefault0(Type enumeration, params DropDownList[] dropDownList)
    {
        if (!enumeration.IsEnum) return false;

        var source = Enum.GetNames(enumeration)
                                    .Where(x => (int)(Enum.Parse(enumeration, x)) != 0)
                                    .Select(o => new { Text = o, Value = (int)(Enum.Parse(enumeration, o)) });

        foreach (var dropDown in dropDownList)
        {
            dropDown.DataTextField = "Text";
            dropDown.DataValueField = "Value";
            dropDown.DataSource = source;
            dropDown.DataBind();
        }

        return true;
    }


    protected void RegistrarPostBack()
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:__doPostBack('', '');", true);
    }


    protected void ImprimirReporteAsPDF(ReportViewer reportViewer, FormatoArchivo formato = FormatoArchivo.PDF)
    {
        //MOSTRAR REPORTE EN PANTALLA
        var bytes = reportViewer.LocalReport.Render(formato.ToString());
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "inline;attachment; filename=Reporte.pdf");
        Response.BinaryWrite(bytes);
        Response.Flush(); // send it to the client to download
        Response.Clear();
    }

    protected byte[] ObtenerBytesReporteAsPDF(ReportViewer reportViewer, FormatoArchivo formato = FormatoArchivo.PDF)
    {
        return reportViewer.LocalReport.Render(formato.ToString());
    }

    protected string ReemplazarParametrosEnElMensajeCorreo(string mensaje)
    {
        StringBuilder MyStringBuilder = new StringBuilder(mensaje);

        foreach (var parametro in parametrosFormatoCorreo.Keys)
        {
            MyStringBuilder.Replace(parametro.ToString(), parametrosFormatoCorreo[parametro]);
        }

        MyStringBuilder.Replace("&nbsp;", " ");
        return MyStringBuilder.ToString();
    }


    // Por ahora da problemas si el control es un "CommandField"
    protected GridViewRow GetRowOfControlInsideGridViewOneLevel(Control controlWhoFireEvent)
    {
        Control controlOneLevel = (controlWhoFireEvent).NamingContainer;

        // No esta testeado, ignorad esto
        //// Si esta dentro de un Item Template falla, por eso se verifica si lo esta, y si lo esta sube un nivel mas para hallar el GridViewRow
        //if (controlOneLevel is ContentPlaceHolder)
        //{
        //    controlOneLevel = controlOneLevel.NamingContainer;
        //}

        return (GridViewRow)controlOneLevel;
    }

    protected int GetRowIndexOfControlInsideGridViewOneLevel(Control controlWhoFireEvent)
    {
        return GetRowOfControlInsideGridViewOneLevel(controlWhoFireEvent).RowIndex;
    }

    protected string obtFiltro(string pCod_Linea_Credito, string ExcepcionCredito, Int64 pCod_Persona)
    {
        string pFiltro = string.Empty;
        pFiltro += " WHERE COD_DEUDOR = " + pCod_Persona + " and COD_LINEA_CREDITO = '" + pCod_Linea_Credito + "' and ESTADO Not In ('T', 'N', 'B', 'P')";

        if (!string.IsNullOrEmpty(ExcepcionCredito))
        {
            if (ExcepcionCredito.Contains(","))
                ExcepcionCredito = ExcepcionCredito.Substring(0, ((ExcepcionCredito.Length) - 1));
            pFiltro += " AND NUMERO_RADICACION NOT IN (" + ExcepcionCredito + ")";
        }
        return pFiltro;
    }
    protected string obtFiltroAvance(string pCod_Linea_Credito, string ExcepcionCredito, Int64 pCod_Persona)
    {
        string pFiltro = string.Empty;
        pFiltro += " WHERE   SALDO_CAPITAL>0 AND COD_DEUDOR = " + pCod_Persona + " and COD_LINEA_CREDITO = '" + pCod_Linea_Credito + "' and ESTADO Not In ('T', 'N', 'B', 'P')";

        if (!string.IsNullOrEmpty(ExcepcionCredito))
        {
            if (ExcepcionCredito.Contains(","))
                ExcepcionCredito = ExcepcionCredito.Substring(0, ((ExcepcionCredito.Length) - 1));
            pFiltro += " AND NUMERO_RADICACION NOT IN (" + ExcepcionCredito + ")";
        }
        return pFiltro;
    }

   

    /// <summary>
    /// Debes tener una carpeta llamada "Archivos" en el mismo directorio para renderizar el pdf ahi y asi poder verlo en el literal
    /// </summary>
    protected void MostrarArchivoEnLiteral(byte[] bytes, Usuario pUsuario, Literal literal, string nombreArchivo = "documentoSalida")
    {
        string pNomUsuario = !string.IsNullOrWhiteSpace(pUsuario.codusuario.ToString()) ? nombreArchivo + " " + pUsuario.codusuario.ToString() : nombreArchivo;

        // ELIMINANDO ARCHIVOS GENERADOS SI LOS ENCUENTRA
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pNomUsuario))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"700px\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        literal.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
    }


    public string ObtenerCadenaConsulta(ArrayList pCriterios)
    {
        String cadenaConsulta = "";
        int contar = 0;
        try
        {
            for (int j = 0; j < pCriterios.Count; j++)
            {
                String[] criterio;
                criterio = pCriterios[j].ToString().Split('|');

                if (criterio[1].ToString().Trim() != "" && criterio[1].ToString().Trim() != "01/01/0001")
                    contar++;
            }

            if (contar != 0)
                cadenaConsulta += " WHERE ";

            for (int c = 0; c < pCriterios.Count; c++)
            {
                String[] criterio;
                criterio = pCriterios[c].ToString().Split('|');

                if (criterio[1].ToString().Trim() != "" && criterio[1].ToString().Trim() != "01/01/0001")
                {
                    String[] valoresCriterio = criterio[1].ToString().Split(' ');
                    for (int j = 0; j < valoresCriterio.Length; j++)
                    {
                        if (EsFecha(valoresCriterio[j].ToString()))
                        {
                            cadenaConsulta += " YEAR( " + criterio[0].ToString() + ") = '" + valoresCriterio[j].ToString().Trim().Substring(6, 4).ToString() + "'" +
                                              " AND MONTH (" + criterio[0].ToString() + ") = '" + valoresCriterio[j].ToString().Trim().Substring(3, 2).ToString() + "'" +
                                              " AND DAY (" + criterio[0].ToString() + ") = '" + valoresCriterio[j].ToString().Trim().Substring(0, 2).ToString() + "'";
                        }
                        else
                            cadenaConsulta += "UPPER(" + criterio[0].ToString() + ") LIKE " + "UPPER('%" + valoresCriterio[j].ToString() + "%')";

                        if (j != valoresCriterio.Length - 1)
                            cadenaConsulta += " AND ";
                    }

                    if (contar - 1 != 0)
                    {
                        cadenaConsulta += " AND ";
                        contar--;
                    }
                }
            }
            return cadenaConsulta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ObtenerCadenaConsulta", ex);
            return null;
        }
    }

    private Boolean EsFecha(String pCadena)
    {
        try
        {
            DateTime.Parse(pCadena);
        }
        catch
        {
            return false;
        }
        return true;
    }

    protected void Navegar(Pagina page)
    {
        try
        {
            switch (page.ToString())
            {
                case "Nuevo":
                    Response.Redirect("Nuevo.aspx", false);
                    break;
                case "Detalle":
                    Response.Redirect("Detalle.aspx", false);
                    break;
                case "Editar":
                    Response.Redirect("Nuevo.aspx?o=E", false);
                    break;
                case "Modificar":
                    Response.Redirect("Lista.aspx?modificar=1", false);
                    break;
                default:
                    Response.Redirect("Lista.aspx", false);
                    break;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "Navegar", ex);
        }
    }

    protected void Navegar(String pPath)
    {
        Response.Redirect(pPath, false);
    }

    public void GuardarValoresConsulta(Panel plControles, string pPrograma)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (control is TextBox)
                {
                    if (((TextBox)(control)).Text.Trim() != "")
                    {
                        string txtTexto = ((TextBox)(control)).Text.Trim();
                        Session.Add(pPrograma + "." + control.ID, txtTexto);
                    }
                }
                else if (control is DropDownList)
                {
                    if (((DropDownList)(control)).SelectedIndex != 0)
                    {
                        string ddlSeleccion = ((DropDownList)(control)).SelectedValue;
                        Session.Add(pPrograma + "." + control.ID, ddlSeleccion);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "GuardarValoresConsulta", ex);
        }
    }

    public void CargarValoresConsulta(Panel plControles, string pPrograma)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (Session[pPrograma + "." + control.ID] != null)
                {
                    if (control is TextBox)
                        ((TextBox)(plControles.FindControl(control.ID))).Text = Session[pPrograma + "." + control.ID].ToString();
                    else if (control is DropDownList)
                        ((DropDownList)(plControles.FindControl(control.ID))).SelectedValue = Session[pPrograma + "." + control.ID].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "CargarValoresConsulta", ex);
        }
    }

    public void LimpiarValoresConsulta(Panel plControles, string pPrograma)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {

                if (control is TextBox)
                {
                    ((TextBox)(control)).Text = "";

                    if (Session[pPrograma + "." + control.ID] != null)
                        Session.Remove(pPrograma + "." + control.ID);
                }
                else if (control.TemplateControl.ToString().Contains("ASP.general_controles_fecha_ascx"))
                {
                    //((UserControl)(control)).Text = "";

                    if (Session[pPrograma + "." + control.ID] != null)
                        Session.Remove(pPrograma + "." + control.ID);
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)(control)).SelectedIndex = 0;

                    if (Session[pPrograma + "." + control.ID] != null)
                        Session.Remove(pPrograma + "." + control.ID);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "LimpiarValoresConsulta", ex);
        }
    }

    public void LimpiarFormulario()
    {
        try
        {
            ContentPlaceHolder contenedor = (ContentPlaceHolder)Master.FindControl("cphMain");
            foreach (Control control in contenedor.Controls)
            {
                if (control is TextBox)
                    ((TextBox)(control)).Text = "";
                else if (control is DropDownList)
                    ((DropDownList)(control)).SelectedIndex = 0;
                else if (control is RadioButton)
                    ((RadioButton)(control)).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)(control)).Checked = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("ACTRGenGeneral", "LimpiarFormulario", ex);
        }
    }

    public void LimpiarPanel(Panel plControles)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (control is TextBox)
                    ((TextBox)(control)).Text = "";
                else if (control is DropDownList)
                    ((DropDownList)(control)).SelectedIndex = 0;
                else if (control is RadioButton)
                    ((RadioButton)(control)).Checked = false;
                else if (control is CheckBox)
                    ((CheckBox)(control)).Checked = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "LimpiarPanel", ex);
        }
    }

    public void DeshabilitarObjetosPantalla(Panel plControles)
    {
        try
        {
            foreach (Control control in plControles.Controls)
            {
                if (control is TextBox)
                    ((TextBox)(control)).Enabled = false;
                else if (control is DropDownList)
                    ((DropDownList)(control)).Enabled = false;
                else if (control is RadioButton)
                    ((RadioButton)(control)).Enabled = false;
                else if (control is CheckBox)
                    ((CheckBox)(control)).Enabled = false;
                else if (control is Panel)
                    DeshabilitarObjetosPantalla((Panel)control);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "LimpiarPanel", ex);
        }
    }

    public void ConfirmarEventoBoton(ImageButton pBoton, String pMensaje)
    {
        try
        {
            pBoton.Attributes.Add("onClick", "return confirm('" + pMensaje + "');");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ConfirmarEventoBoton", ex);
        }
    }

    public void ConfirmarEliminarFila(GridViewRowEventArgs e, String pBoton)
    {
        if (e.Row == null)
            return;
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    ImageButton btnGrilla = (ImageButton)e.Row.FindControl(pBoton);
                    if (btnGrilla != null)
                        btnGrilla.Attributes.Add("onClick", "return confirm('Esta seguro que desea eliminar este registro?')");
                    break;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ConfirmarEliminarFila", ex);
        }
    }

    public void VerError(String pError)
    {
        try
        {
            if (pError.Trim() == "")
            {
                ((Label)Master.FindControl("lblError")).Visible = false;
                ((Label)Master.FindControl("lblError")).Text = pError;
                ((Panel)Master.FindControl("plError")).Visible = false;
            }
            else
            {
                if (pError.Contains("ORA-20101"))
                    if (pError.Length > 9)
                        pError = pError.Substring(9, pError.Length - 9);
                ((Label)Master.FindControl("lblError")).Visible = true;
                ((Label)Master.FindControl("lblError")).Text = pError;
                ((Panel)Master.FindControl("plError")).Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VerError", ex);
        }
    }

    public void OcultarError()
    {
        try
        {
            ((Panel)Master.FindControl("pError")).Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "OcultarError", ex);
        }
    }

    public void VerErrorControl(String pError)
    {
        try
        {
            ClientScript.RegisterStartupScript(this.GetType(), "jsError", "alert('" + pError + "')", true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VerError", ex);
        }
    }

    public void CerrarNuevo(string pPrograma)
    {
        try
        {
            ClientScriptManager close = Page.ClientScript;
            close.RegisterClientScriptBlock(this.GetType(), "ClientScript", "javascript:cambiarDisplayLista('divL" + pPrograma + "', 'divN" + pPrograma + "')", true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "CerrarNuevo", ex);
        }
    }

    public void VerAuditoria(string pUsuarioCrea, DateTime pFechaCrea, string pUsuarioEdita, DateTime pFechaEdita)
    {
        try
        {
            string infoAuditoria = "";

            infoAuditoria += "Creado '" + pUsuarioCrea.Trim() + "' - '" + pFechaCrea + "'";

            if (!string.IsNullOrEmpty(pUsuarioEdita))
                infoAuditoria += ", modificado '" + pUsuarioEdita.Trim() + "' - '" + pFechaEdita + "'";

            ((Label)Master.FindControl("lblAuditoria")).Text = infoAuditoria;
            ((Label)Master.FindControl("lblAuditoria")).Visible = true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VerAuditoria", ex);
        }
    }

    public string idObjeto
    {
        get
        {
            if (Master != null) return ((HiddenField) Master.FindControl("hfObj")).Value.Trim();
            return "";
        }

        set { ((HiddenField)Master.FindControl("hfObj")).Value = value; }
    }

    public int pageSize
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]); }
    }

    public String emptyQuery
    {
        get { return ConfigurationManager.AppSettings["EmptyQuery"]; }
    }

    public String FormatoDecimal(string str)
    {
        string formateado = "";

        string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (s == ".")
            str = str.Replace(",", "");
        else
        {
            str = str.Replace(".", "");
            str = str.Replace(",", ".");
        }

        try
        {
            if (str != "" && str.ToLower() != "null" && Convert.ToInt64(str) > 0)
            {

                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 9)
                { str = str.Substring(0, 9); }

                int longi = str.Length;
                string mill = "";
                string mil = "";
                string cen = "";


                if (longi > 0 && longi <= 3)
                {
                    cen = str.Substring(0, longi);
                    formateado = Convert.ToInt64(cen).ToString();
                }
                else if (longi > 3 && longi <= 6)
                {
                    mil = str.Substring(0, longi - 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mil) + "." + cen;
                }
                else if (longi > 6 && longi <= 9)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else
                { formateado = "0"; }
            }
            else { if (str.ToLower() != "null") formateado = "0"; else formateado = ""; }

            return formateado.ToString();
        }
        catch (Exception ex)
        {
            ex.ToString();
            return "";
        }
    }


    public void PoblarListaTipoCtaXPagar(DropDownList ddlControl)
    {
        ddlControl.Items.Clear();
        ddlControl.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlControl.Items.Insert(1, new ListItem("Facturas", "1"));
        ddlControl.Items.Insert(2, new ListItem("Orden de Pago", "2"));
        ddlControl.Items.Insert(3, new ListItem("Orden de Compra", "3"));
        ddlControl.Items.Insert(4, new ListItem("Orden de Servicio", "4"));
        ddlControl.Items.Insert(5, new ListItem("Contrato de Servicio", "5"));
        ddlControl.DataBind();
    }
    public void PoblarListaFromatosDIAN(DropDownList ddlControl)
    {
        ddlControl.Items.Clear();
        ddlControl.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlControl.Items.Insert(1, new ListItem("FORMATO 1019-Movimiento cuenta Ahorro", "1"));
        ddlControl.Items.Insert(2, new ListItem("FORMATO 1020-Inversiones en CDATS", "2"));
        ddlControl.Items.Insert(3, new ListItem("FORMATO 1010-Información socios accionistas, cooperados", "3"));
        ddlControl.Items.Insert(4, new ListItem("FORMATO 1008-Saldo de cuentas por Cobrar", "4"));
        ddlControl.Items.Insert(5, new ListItem("FORMATO 1001-Pago o Abonos en cuenta y Retenciones Practicadas", "5"));
        ddlControl.Items.Insert(6, new ListItem("FORMATO 1007-Ingresos Recibidos", "6"));
        ddlControl.Items.Insert(7, new ListItem("FORMATO 1009-Saldo de cuentas por pagar", "7"));
        ddlControl.Items.Insert(8, new ListItem("FORMATO 1026-Cartera neta colocada", "8"));
        ddlControl.DataBind();
    }


    public string CalcularDigitoVerificacion(string Nit)
    {
        string Temp;
        int Contador;
        int Residuo;
        int Acumulador;
        int[] Vector = new int[15];

        Vector[0] = 3;
        Vector[1] = 7;
        Vector[2] = 13;
        Vector[3] = 17;
        Vector[4] = 19;
        Vector[5] = 23;
        Vector[6] = 29;
        Vector[7] = 37;
        Vector[8] = 41;
        Vector[9] = 43;
        Vector[10] = 47;
        Vector[11] = 53;
        Vector[12] = 59;
        Vector[13] = 67;
        Vector[14] = 71;

        Acumulador = 0;

        Residuo = 0;

        for (Contador = 0; Contador < Nit.Length; Contador++)
        {
            Temp = Nit.Substring((Nit.Length - 1) - Contador, 1);
            Acumulador = Acumulador + (Convert.ToInt32(Temp) * Vector[Contador]);
        }

        Residuo = Acumulador % 11;

        return Residuo > 1 ? Convert.ToString(11 - Residuo) : Residuo.ToString();
    }

    public DateTime ConvertirStringToDate(String pCadena)
    {
        try
        {
            return DateTime.ParseExact(pCadena, gFormatoFecha, null);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    public DateTime? ConvertirStringToDateN(String pCadena)
    {
        try
        {
            return DateTime.ParseExact(pCadena, gFormatoFecha, null);
        }
        catch
        {
            return null;
        }
    }

    public decimal ConvertirStringToDecimal(String pCadena)
    {
        if (pCadena == "")
            return 0;
        try
        {
            return Convert.ToDecimal(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public decimal? ConvertirStringToDecimalN(String pCadena)
    {
        if (pCadena == "")
            return null;
        try
        {
            return Convert.ToDecimal(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int64 ConvertirStringToInt(String pCadena)
    {
        if (pCadena == "")
            return 0;
        try
        {
            return Convert.ToInt64(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int64? ConvertirStringToIntN(String pCadena)
    {
        if (pCadena == "")
            return null;
        try
        {
            return Convert.ToInt64(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int32 ConvertirStringToInt32(String pCadena)
    {
        if (pCadena == "")
            return 0;
        try
        {
            return Convert.ToInt32(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public Int32? ConvertirStringToInt32N(String pCadena)
    {
        if (pCadena == "")
            return null;
        try
        {
            return Convert.ToInt32(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
        }
        catch
        {
            return 0;
        }
    }

    public void Deshabilitar(ContentPlaceHolder pContenedor)
    {
        if (pContenedor == null)
            return;
        try
        {
            foreach (Control control in pContenedor.Controls)
            {
                DeshabilitarControl(control);
            }
        }
        catch
        {
            return;
        }
    }

    public void DeshabilitarControl(Control pControl)
    {
        if (pControl == null)
            return;
        try
        {
            if (pControl is TextBox)
                ((TextBox)pControl).Enabled = false;
            else if (pControl is DropDownList)
                ((DropDownList)(pControl)).Enabled = false;
            else if (pControl is Image)
                ((Image)(pControl)).Enabled = false;
            else if (pControl is Button)
                ((Button)(pControl)).Enabled = false;
            else if (pControl is GridView)
                ((GridView)(pControl)).Enabled = false;
            else if (pControl is GridView)
                ((GridView)(pControl)).Enabled = false;
            else if (pControl is RadioButton)
                ((RadioButton)(pControl)).Enabled = false;
            else if (pControl is RadioButtonList)
                ((RadioButtonList)(pControl)).Enabled = false;
            else if (pControl is CheckBox)
                ((CheckBox)(pControl)).Enabled = false;
            // Deshabilitar los hijos
            if (pControl.Controls != null)
            {
                foreach (Control pHijo in pControl.Controls)
                {
                    DeshabilitarControl(pHijo);
                }
            }
        }
        catch
        {
            return;
        }
    }

    public GridView CopiarGridViewParaExportar(GridView gvLista, string pDatos)
    {
        GridView gvExportar = new GridView();
        gvExportar = gvLista;
        gvExportar.AllowPaging = false;
        foreach (DataControlField eColumna in gvExportar.Columns)
        {
            try
            {
                if (eColumna.GetType().ToString() == "System.Web.UI.WebControls.BoundField")
                {
                    BoundField eCol = (BoundField)eColumna;
                    if (eCol.DataFormatString != "{0:d}" && eCol.DataFormatString != "{0:c}")
                        eCol.DataFormatString = "";
                }
            }
            catch { }
        }

        gvExportar.DataSource = Session[pDatos];
        gvExportar.DataBind();
        gvExportar.EnableViewState = false;
        return gvExportar;
    }
    public StringBuilder ExportarGridCSV(GridView gvDataSinPaginar, char pSeparadorCampo = ';')
    {
        StringBuilder sb = new StringBuilder();
        if (gvDataSinPaginar.Rows.Count > 0)
        {
            string pItem;
            for (int k = 0; k < gvDataSinPaginar.Columns.Count; k++)
            {
                string s = gvDataSinPaginar.Columns[k].HeaderStyle.CssClass;
                if (s != "gridIco")
                {
                    pItem = HttpUtility.HtmlDecode(gvDataSinPaginar.Columns[k].HeaderText);
                    sb.Append(pItem);
                    sb.Append(pSeparadorCampo);
                }
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("\r\n");

            for (int i = 0; i < gvDataSinPaginar.Rows.Count; i++)
            {
                for (int k = 0; k < gvDataSinPaginar.Columns.Count; k++)
                {
                    string s = gvDataSinPaginar.Columns[k].HeaderStyle.CssClass;
                    if (s != "gridIco")
                    {
                        if (gvDataSinPaginar.Rows[i].Cells[k].Text != "&nbsp;" || gvDataSinPaginar.Rows[i].Cells[k].Text != "")
                        {
                            pItem = HttpUtility.HtmlDecode(gvDataSinPaginar.Rows[i].Cells[k].Text);
                            sb.Append(pItem);
                        }
                        else
                            sb.Append(" ");
                        sb.Append(pSeparadorCampo);
                    }
                }
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append("\r\n");
            }
        }
        return sb;
    }

    public void ExportarGridCSVDirecto(GridView gvDataSinPaginar, string nombreArchivo = "Archivo", char pSeparadorCampo = ';')
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".csv");
        Response.Charset = "UTF-8";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;

        StringBuilder sb = ExportarGridCSV(gvDataSinPaginar, pSeparadorCampo);
        Response.Output.Write(sb.ToString());
        Response.Flush();

        Response.End();
    }

    public void ExportarGridViewEnExcel(GridView gvDataSinPaginar, string nombreArchivo = "Archivo")
    {
        if (gvDataSinPaginar.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            using (Page pagina = new Page())
            using (HtmlForm form = new HtmlForm())
            {
                gvDataSinPaginar.EnableViewState = false;
                pagina.EnableEventValidation = false;

                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvDataSinPaginar);

                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreArchivo + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
        }
    }
    public void VisualizarOpciones(string pPrograma, string pTipoPagina)
    {
        try
        {
            if (Session["usuario"] != null)
            {
                opcionActual = ObtenerOpcionActual(pPrograma.Trim());
                ((Usuario)Session["usuario"]).codOpcionActual = opcionActual.cod_opcion;

                if (opcionActual.generalog == 1)
                    ((Usuario)Session["usuario"]).programaGeneraLog = true;
                else
                    ((Usuario)Session["usuario"]).programaGeneraLog = false;

                if (opcionActual.nombreopcion != null)
                {
                    ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion;

                    switch (pTipoPagina)
                    {
                        case "A": // Agregar
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Nuevo";
                            ((LinkButton)Master.FindControl("btnGuardar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        case "D": // Detalle
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Detalle";
                            break;
                        case "E": // Editar
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Edicion";
                            break;
                        case "L": // Lista
                            ((Label)Master.FindControl("lblOpcion")).Text = opcionActual.nombreopcion + " - Consulta";
                            ((LinkButton)Master.FindControl("btnConsultar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        case "G":
                            ((LinkButton)Master.FindControl("btnGuardar")).Attributes.Add("onClick", "LoadingList()");
                            break;
                        default:
                            break;
                    }

                    // PERMISO EN OPCION A BOTONES TRANSACCIONALES
                    if (opcionActual.insertar != 1) Master.FindControl("btnNuevo").Visible = false;
                    if (opcionActual.modificar != 1) Master.FindControl("btnEditar").Visible = false;
                    if (opcionActual.borrar != 1) Master.FindControl("btnEliminar").Visible = false;
                    if (opcionActual.consultar != 1) Master.FindControl("btnConsultar").Visible = false;
                }
                else
                {
                    Response.Redirect("~/General/Global/noAcceso.htm", false);
                }
            }
            else
            {
                if (Session["COD_INGRESO"] != null)
                {
                    Ingresos pIngresos = new Ingresos();
                    pIngresos.cod_ingreso = Convert.ToInt32(Session["COD_INGRESO"].ToString());
                    pIngresos.fecha_horasalida = DateTime.Now;
                    service.ModificarUsuarioIngreso(pIngresos, (Usuario)Session["usuario"]);
                }
                Response.Redirect("~/General/Global/FinSesion.htm", false);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "VisualizarOpciones", ex);
        }
    }
    private Acceso ObtenerOpcionActual(string pIdOpcion)
    {
        try
        {
            Acceso accesos = new Acceso();

            if (Session["accesos"] != null)
            {
                List<Acceso> lstAccesos = new List<Acceso>();
                lstAccesos = (List<Acceso>)Session["accesos"];

                foreach (Acceso ent in lstAccesos)
                    if (ent.cod_opcion == Convert.ToInt64(pIdOpcion))
                        accesos = ent;
            }

            return accesos;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("GlobalWeb", "ObtenerOpcionActual", ex);
            return null;
        }
    }
    public readonly string _tipoOperacionRetiroEnpacto = "1";
    public readonly string _tipoOperacionDepositoEnpacto = "3";    

    public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
    {
        return Math.Abs((fechaHasta.Month - fechaDesde.Month) + 12 * (fechaHasta.Year - fechaDesde.Year));
    }

    #region OCULTAR FOOTER DE GRIDVIEW

    public void OcultarGridFooter(GridView pGridInfo, bool IsVisible)
    {
        if (pGridInfo == null)
            return;
        try
        {
            ControlCollection pControls = pGridInfo.FooterRow.Controls;
            if (pControls != null)
            {
                foreach (Control pControl in pControls)
                {
                    OcultarControl(pControl, IsVisible);
                }
            }
        }
        catch
        {
            return;
        }
    }

    public void OcultarControl(Control pControl, bool IsVisible)
    {
        if (pControl == null)
            return;
        try
        {
            if (pControl is Label)
                ((Label)pControl).Visible = IsVisible;
            if (pControl is TextBox)
                ((TextBox)pControl).Visible = IsVisible;
            else if (pControl is DropDownList)
                ((DropDownList)(pControl)).Visible = IsVisible;
            else if (pControl is Button)
                ((Button)(pControl)).Visible = IsVisible;
            else if (pControl is ImageButton)
                ((ImageButton)(pControl)).Visible = IsVisible;
            else if (pControl is DataControlFieldCell)
            {
                foreach (Control pHijo in pControl.Controls)
                {
                    OcultarControl(pHijo, IsVisible);
                }
            }
        }
        catch
        {
            return;
        }
    }

    #endregion



}