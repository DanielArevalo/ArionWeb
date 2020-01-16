using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;

public delegate void btnImpresion_ActionsDelegate(object sender, EventArgs e);

public partial class ctlFormatoDocum : System.Web.UI.UserControl
{
    public event btnImpresion_ActionsDelegate eventoClick;
    

    Xpinn.Aportes.Services.FormatoDocumentoServices BOFormato = new Xpinn.Aportes.Services.FormatoDocumentoServices();
    //TIPOS DE FORMATOS
    //Tipo 1 = Afiliacion ,  2 = Aprobación 
    public void Inicializar(string pTipo)
    {
        List<Xpinn.Aportes.Entities.FormatoDocumento> lstFormatos = new List<Xpinn.Aportes.Entities.FormatoDocumento>();
        Xpinn.Aportes.Entities.FormatoDocumento pEntidad = new Xpinn.Aportes.Entities.FormatoDocumento();
        pEntidad.tipo = pTipo;
        lstFormatos = BOFormato.ListarFormatoDocumento(pEntidad, (Usuario)Session["usuario"]);
        if (lstFormatos.Count > 0)
        {
            ddlFormatosImp.DataTextField = "descripcion";
            ddlFormatosImp.DataValueField = "cod_documento";
            ddlFormatosImp.DataSource = lstFormatos;
            ddlFormatosImp.DataBind();
        }
        lblTipo.Text = pTipo.Trim();
    }

    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        if (eventoClick != null)
            eventoClick(sender, e);
    }

    public Boolean ImprimirFormato(string pVariable,string pRuta)
    {
        lblErrorFI.Visible = false;
        lblErrorFI.Text = "";
        if (ddlFormatosImp.SelectedItem == null)
        {
            lblErrorFI.Visible = true;
            lblErrorFI.Text = "No existen formatos creados para generar la Impresión, Adiciónelas por el módulo de Gestión Solidaria - Formato de Documentos";
            return false;
        }

        //RECUPERAR NOMBRE DE PL AL QUE EJECUTARA
        string cTipoDocumento = ddlFormatosImp.SelectedValue;
        string cNombreDocumento = ddlFormatosImp.SelectedItem.Text;
        string cDocumentoGenerado = Server.MapPath(pRuta + pVariable.Trim() + "_" + cTipoDocumento + '.' + 'p' + 'd' + 'f');
        Xpinn.Aportes.Entities.FormatoDocumento FormatoDOC = new Xpinn.Aportes.Entities.FormatoDocumento();
        FormatoDOC = BOFormato.ConsultarFormatoDocumento(Convert.ToInt64(cTipoDocumento), (Usuario)Session["Usuario"]);

        List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDocumentos = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();
        lstDocumentos = BOFormato.ListarDatosDeDocumento(Convert.ToInt64(pVariable),FormatoDOC.nombre_pl, (Usuario)Session["usuario"]);

        if (FormatoDOC != null)
        {
            if (FormatoDOC.texto != null)
            {
                if (FormatoDOC.texto.Trim().Length > 0)
                {
                    try { ReemplazarEnDocumentoDeWordYGuardarPDF(FormatoDOC.texto, lstDocumentos, cDocumentoGenerado); }
                    catch (Exception ex)
                    {
                        throw ex;
                        //lblErrorFI.Visible = true;
                        //lblErrorFI.Text = cNombreDocumento + " " + ex.Message;
                        //return false;
                    }
                }
            }
        }        
        return true;        
    }

    private void ReemplazarEnDocumentoDeWordYGuardarPDF(string pTexto, List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> plstReemplazos, string pDocumentoGenerado)
    {
        // Validar que exista el texto
        if (pTexto.Trim().Length <= 0)
            return;
        // Hacer los reemplazos de los campos
        foreach (Xpinn.FabricaCreditos.Entities.DatosDeDocumento dFila in plstReemplazos)
        {
            try
            {
                string cCampo = dFila.Campo.ToString().Trim();
                string cValor = "";
                if (dFila.Valor != null)
                    cValor = dFila.Valor.ToString().Trim().Replace("'", "");
                else
                    cValor = "";
                pTexto = pTexto.Replace(cCampo, cValor).Replace("'", "");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        // GUARDANDO Y CONVIRTIENDO A PDF
        StringReader sr = new StringReader(pTexto.Replace("'", ""));
        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 20f, 10f, 10f, 10f);
        PdfWriter.GetInstance(pdfDoc, new FileStream(pDocumentoGenerado, FileMode.OpenOrCreate));
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        // Descargar el archivo al cliente
        //try
        //{
        //if (GlobalWeb.bMostrarPDF)
        //{
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=" + pDocumentoGenerado + ".pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Write(pdfDoc);
        //}
        //}
        //catch { }
    }


    public void MostrarControl()
    {
        mpeFormatos.Show();
    }

    public void OcultarControl()
    {
        mpeFormatos.Hide();
    }

    protected void btnCancFormat_Click(object sender, ImageClickEventArgs e)
    {
        OcultarControl();
    }

    public string lblErrorText
    {
        set { lblErrorFI.Text = value; }
        get { return lblErrorFI.Text; }
    }

    public bool lblErrorIsVisible
    {
        set { lblErrorFI.Visible = value; }
        get { return lblErrorFI.Visible; }
    }

    public int ddlFormatosIndex
    {
        set { ddlFormatosImp.SelectedIndex = value; }
        get { return ddlFormatosImp.SelectedIndex; }
    }

    public string ddlFormatosValue
    {
        set { ddlFormatosImp.SelectedValue = value; }
        get { return ddlFormatosImp.SelectedValue; }
    }

    public string ddlFormatosItemText
    {
        set { ddlFormatosImp.SelectedItem.Text = value; }
        get { return ddlFormatosImp.SelectedItem.Text; }
    }

    public ListItem ddlFormatosItem
    {
        get { return ddlFormatosImp.SelectedItem; }
    }

}