using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void ToolBarDelegate1(object sender, ImageClickEventArgs e);
public delegate void ActionsDelegate1(object sender, EventArgs e);

public partial class Site1 : System.Web.UI.MasterPage
{
    public event ToolBarDelegate1 eventoGuardar;
    ToolBarDelegate1 guardarToolbar;

    public event ToolBarDelegate1 eventoConsultar;
    ToolBarDelegate1 consultarToolbar;

    public event ToolBarDelegate1 eventoNuevo;
    ToolBarDelegate1 nuevoToolbar;

    public event ToolBarDelegate1 eventoEliminar;
    ToolBarDelegate1 eliminarToolbar;

    public event ToolBarDelegate1 eventoEditar;
    ToolBarDelegate1 editarToolbar;

    public event ToolBarDelegate1 eventoLimpiar;
    ToolBarDelegate1 limpiarToolbar;

    public event ToolBarDelegate1 eventoCancelar;
    ToolBarDelegate1 cancelarToolbar;    

    public event ToolBarDelegate1 eventoAdelante;
    ToolBarDelegate1 adelanteToolbar;

    public event ToolBarDelegate1 eventoAdelante2;
    ToolBarDelegate1 adelante2Toolbar;

    public event ToolBarDelegate1 eventoAtras;
    ToolBarDelegate1 atrasToolbar;

    public event ActionsDelegate1 eventoAcciones;
    ActionsDelegate1 accionesToolbar;

    protected void Page_Load(object sender, EventArgs e)
    {
        guardarToolbar = eventoGuardar;
        consultarToolbar = eventoConsultar;
        nuevoToolbar = eventoNuevo;
        eliminarToolbar = eventoEliminar;
        editarToolbar = eventoEditar;
        limpiarToolbar = eventoLimpiar;
        cancelarToolbar = eventoCancelar;        
        adelanteToolbar = eventoAdelante;
        adelante2Toolbar = eventoAdelante2;
        atrasToolbar = eventoAtras;
        accionesToolbar = eventoAcciones;

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
        if (limpiarToolbar == null)
            btnLimpiar.Visible = false;
        if (cancelarToolbar == null)
            btnCancelar.Visible = false;
        if (adelanteToolbar == null)
            btnAdelante.Visible = false;
        if (adelante2Toolbar == null)
            btnAdelante2.Visible = false;
        if (atrasToolbar == null)
            btnAtras.Visible = false;
        if (accionesToolbar == null)
            ddlAcciones.Visible = false;

        

        if (Session["nombreModulo"] != null)
            lblModulo.Text = Session["nombreModulo"].ToString();
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

    protected void ddlAcciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoAcciones != null)
            eventoAcciones(sender, e);
    }
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoAtras != null)
            eventoAtras(sender, e);
    }
    
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoAdelante != null)
            eventoAdelante(sender, e);
    }

    protected void btnAdelante2_Click(object sender, ImageClickEventArgs e)
    {
        if (eventoAdelante2 != null)
            eventoAdelante2(sender, e);
    }
}
