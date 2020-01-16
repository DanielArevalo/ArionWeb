using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public delegate void ToolBarDelegate(object sender, ImageClickEventArgs e);
public delegate void ActionsDelegate(object sender, EventArgs e);

public partial class Site : System.Web.UI.MasterPage
{
    public event ToolBarDelegate eventoGuardar;
    ToolBarDelegate guardarToolbar;

    public event ToolBarDelegate eventoConsultar;
    ToolBarDelegate consultarToolbar;

    public event ToolBarDelegate eventoNuevo;
    ToolBarDelegate nuevoToolbar;

    public event ToolBarDelegate eventoEliminar;
    ToolBarDelegate eliminarToolbar;

    public event ToolBarDelegate eventoEditar;
    ToolBarDelegate editarToolbar;

    public event ToolBarDelegate eventoRegresar;
    ToolBarDelegate regresarToolbar;

    public event ToolBarDelegate eventoLimpiar;
    ToolBarDelegate limpiarToolbar;

    public event ToolBarDelegate eventoCancelar;
    ToolBarDelegate cancelarToolbar;

    public event ToolBarDelegate eventoImprimir;
    ToolBarDelegate imprimirToolbar;

    public event ToolBarDelegate eventoExportar;
    ToolBarDelegate exportarToolbar;

    public event ActionsDelegate eventoAcciones;
    ActionsDelegate accionesToolbar;

    public event ToolBarDelegate eventoCopiar;
    ToolBarDelegate copiarToolbar;

    public event ToolBarDelegate eventoCargar;
    ToolBarDelegate cargarToolbar;

    public event ToolBarDelegate eventoImportar;
    ToolBarDelegate importarToolbar;


    protected void Application_BeginRequest(object sender, EventArgs e)
    {
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    

        guardarToolbar = eventoGuardar;
        consultarToolbar = eventoConsultar;
        nuevoToolbar = eventoNuevo;
        eliminarToolbar = eventoEliminar;
        editarToolbar = eventoEditar;
        limpiarToolbar = eventoLimpiar;
        regresarToolbar = eventoRegresar;
        cancelarToolbar = eventoCancelar;
        imprimirToolbar = eventoImprimir;
        exportarToolbar = eventoExportar;
        accionesToolbar = eventoAcciones;
        copiarToolbar = eventoCopiar;
        cargarToolbar = eventoCargar;
        importarToolbar = eventoImportar;

        if (guardarToolbar == null)
            btnGuardar.Visible = false;
        if (consultarToolbar == null)
            btnConsultar.Visible = false;
        if (nuevoToolbar == null)
            btnNuevo.Visible = false;
        if (eliminarToolbar == null)
            btnEliminar.Visible = false;
        if (editarToolbar == null)
            btnEditar.Visible = false;
        if (regresarToolbar == null)
            btnRegresar.Visible = false;
        if (limpiarToolbar == null)
            btnLimpiar.Visible = false;
        if (cancelarToolbar == null)
            btnCancelar.Visible = false;
        if (imprimirToolbar == null)
            btnImprimir.Visible = false;
        if (exportarToolbar == null)
            btnExportar.Visible = false;
        if (accionesToolbar == null)
            ddlAcciones.Visible = false;
        if (copiarToolbar == null)
            btnCopiar.Visible = false;
        if (cargarToolbar == null)
            btnCargar.Visible = false;
        if (importarToolbar == null)
            btnImportar.Visible = false;

        if (Session["nombreModulo"] != null)
            lblModulo.Text = Session["nombreModulo"].ToString();

        if (Session["ocultarMenu"] != null)
        {
            header1.Visible = false;
            menu1.Visible = false;
            home.Visible = false;
            lblModulo.Visible = false;
            Session.Remove("ocultarMenu");
        }
        Configuracion conf = new Configuracion();
        if (conf.ObtenerValorConfig("Modulo") != null)
        {
            if (conf.ObtenerValorConfig("Modulo").ToString().Trim() != "")
            {
                home.Visible = false;
            }
        }

    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (guardarToolbar != null)
            guardarToolbar(sender, e);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        if (nuevoToolbar != null)
            nuevoToolbar(sender, e);
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        if (editarToolbar != null)
            editarToolbar(sender, e);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        if (eliminarToolbar != null)
            eliminarToolbar(sender, e);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoConsultar != null)
            eventoConsultar(sender, e);
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoRegresar != null)
            eventoRegresar(sender, e);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoLimpiar != null)
            eventoLimpiar(sender, e);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoCancelar != null)
            eventoCancelar(sender, e);
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoImprimir != null)
            eventoImprimir(sender, e);
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoExportar != null)
            eventoExportar(sender, e);
    }

    protected void ddlAcciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoAcciones != null)
            eventoAcciones(sender, e);
    }

    protected void btnCopiar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoCopiar != null)
            eventoCopiar(sender, e);
    }

    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoCargar != null)
            eventoCargar(sender, e);
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoImportar != null)
            eventoImportar(sender, e);
    }

    public void MostrarGuardar(Boolean pestado)
    {
        btnGuardar.Visible = pestado;
    }

    public void MostrarConsultar(Boolean pestado)
    {
        btnConsultar.Visible = pestado;        
    }

    public void MostrarEliminar(Boolean pestado)
    {
        btnEliminar.Visible = pestado;
    }

    public void MostrarRegresar(Boolean pestado)
    {
        btnRegresar.Visible = pestado;
    }

    public void MostrarCancelar(Boolean pestado)
    {
        btnCancelar.Visible = pestado;
    }

    public void MostrarLimpiar(Boolean pestado)
    {
        btnLimpiar.Visible = pestado;
    }

    public void BloquearConsultar(Boolean pestado)
    {
        if (pestado == true)
            btnConsultar.OnClientClick = "this.disabled=true;this.value='Enviando...'";
        else
            btnConsultar.OnClientClick = "";
    }

    public void MostrarImprimir(Boolean pestado)
    {
        btnImprimir.Visible = pestado;
    }

    public void MostrarExportar(Boolean pestado)
    {
        btnExportar.Visible = pestado;
    }

    public void MostrarNuevo(Boolean pestado)
    {
        btnNuevo.Visible = pestado;
    }

    public void MostrarCopiar(Boolean pestado)
    {
        btnCopiar.Visible = pestado;
    }

    public void MostrarCargar(Boolean pestado)
    {
        btnCargar.Visible = pestado;
    }

    public void MostrarImportar(Boolean pestado)
    {
        btnImportar.Visible = pestado;
    }

    public void MostrarImagenLoading(Boolean pestado)
    {       
        
    }
}
