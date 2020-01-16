using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Xpinn.Util;

public partial class ctrl_header : System.Web.UI.UserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["usuario"] != null)
            {
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                usuario = (Xpinn.Util.Usuario)Session["usuario"];
                if (usuario !=null)
                {

                    lblUser.Text = usuario.nombre.Length < 40 ? usuario.nombre : usuario.nombre.Substring(0, 40);
                    lblRol.Text = usuario.nombreperfil.Length < 40 ? usuario.nombreperfil : usuario.nombreperfil.Substring(0, 40);
                    lblOficina.Text = usuario.nombre_oficina.Length < 40 ? usuario.nombre_oficina : usuario.nombre_oficina.Substring(0, 40);

                }
                int cod = 0;
                if (usuario.idEmpresa != 0)
                    cod = Convert.ToInt32(usuario.idEmpresa);
                
                Xpinn.Util.Usuario newUsu = new Xpinn.Util.Usuario();
            }
        }
    }

    protected void Buscar(object sender, EventArgs e)
    {
        LinkButton ctrl = sender as LinkButton;
        string Id = ctrl.ID;
        String Name = ctrl.CommandName;
        Session["temp"] = Name;
        Thread.CurrentThread.CurrentCulture =   new CultureInfo(Name);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(Name);
        Server.Transfer(Request.Path);

    }
    protected void hlkCerrar_Click(object sender, EventArgs e)
    {
        if (Session["COD_INGRESO"] != null)
        {
            
        }
        Session.Abandon();
        Session.RemoveAll();
        Response.Redirect("~/Default.aspx");
    }
}