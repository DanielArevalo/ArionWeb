using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Comun.Services;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Comun.Services.GDocumentalService GDocumentalService = new Xpinn.Comun.Services.GDocumentalService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[GDocumentalService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(GDocumentalService.CodigoPrograma, "E");
            else
                VisualizarOpciones(GDocumentalService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[GDocumentalService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[GDocumentalService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(GDocumentalService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);


                }
                else
                {
                    //txtCodigo.Text = Convert.ToString(TipoArticuloservices.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]));
                }

                Xpinn.Comun.Entities.GestionDocumental vGestionDocumental = new Xpinn.Comun.Entities.GestionDocumental();
                List<Xpinn.Comun.Entities.GestionDocumental> lstGd = new List<Xpinn.Comun.Entities.GestionDocumental>();                
                lstGd = GDocumentalService.ListarGDocumental(vGestionDocumental, null);

                int padre = 0;
                foreach (Xpinn.Comun.Entities.GestionDocumental  row in lstGd)
                {
                    TreeNode nuevoNodo = new TreeNode();
                    nuevoNodo.Text = row.Descripcion.ToString().Trim();
                    TreeNode nodePadre = null;
                    
                    // si el parámetro nodoPadre es nulo es porque es la primera llamada, son los Nodos
                    // del primer nivel que no dependen de otro nodo.
                    if (nodePadre == null)
                    {
                        TreeView1.Nodes.Add(nuevoNodo);

                        foreach (Xpinn.Comun.Entities.GestionDocumental row23 in lstGd)
                        {

                            TreeNode nodo = new TreeNode();
                            nodo.Text = row23.Descripcion + padre;
                            TreeView1.Nodes[padre].ChildNodes.Add(nodo);
                        }
                        padre++;
                    }
                   
                }


            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.TipoArticulo vTipoArticulo = new Xpinn.ActivosFijos.Entities.TipoArticulo();

            if (idObjeto != "")
                //vTipoArticulo = GDocumentalService.ConsultarTipoArticulo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            //vTipoArticulo.IdTipo_Articulo   = Convert.ToInt32(txtCodigo.Text.Trim());
            //vTipoArticulo.Descripcion   = Convert.ToString(txtDescripcion.Text.Trim());
            //vTipoArticulo.Dias_Periodicidad   = Convert.ToInt32(txtDias.Text.Trim());

            if (idObjeto != "")
            {
                vTipoArticulo.IdTipo_Articulo   = Convert.ToInt32(idObjeto);
                //GDocumentalService.ModificarTipoArticulo(vTipoArticulo, (Usuario)Session["usuario"]);
            }
            else
            {
                //vTipoArticulo = GDocumentalService.CrearTipoArticulo (vTipoArticulo, (Usuario)Session["usuario"]);
                idObjeto = vTipoArticulo.IdTipo_Articulo  .ToString();
            }

            Session[GDocumentalService.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.ActivosFijos.Entities.TipoArticulo vTipoArticulo = new Xpinn.ActivosFijos.Entities.TipoArticulo();
            //vTipoArticulo = GDocumentalService.ConsultarTipoArticulo  (Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            //if (!string.IsNullOrEmpty(vTipoArticulo.IdTipo_Articulo  .ToString()))
            //    txtCodigo.Text = HttpUtility.HtmlDecode(vTipoArticulo.IdTipo_Articulo.ToString().Trim());
            //if (!string.IsNullOrEmpty(vTipoArticulo.Descripcion ))
            //    txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoArticulo.Descripcion.ToString().Trim());
            //if (!string.IsNullOrEmpty(vTipoArticulo.Dias_Periodicidad .ToString()))
            //    txtDias .Text = HttpUtility.HtmlDecode(vTipoArticulo.Dias_Periodicidad.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GDocumentalService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}