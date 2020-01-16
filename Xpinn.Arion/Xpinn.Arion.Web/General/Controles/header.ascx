<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="ctrl_header" %>
<style type="text/css">
			
			* {
				margin:0px;
				padding:0px;
			}
			
			#header {
				margin:auto;
				width:500px;
				font-family:Arial, Helvetica, sans-serif;
			}
			
			ul, ol {
				list-style:none;
			}
			
			.nav {
				width:500px; /*Le establecemos un ancho*/
				margin-left:-30%; /*Centramos automaticamente*/
			}
 
			.nav > li {
				float:left;
                text-align:center;
                width:150px;
			}
			
			.nav li .a {
				color:#fff;
				text-decoration:none;
				padding:0px 12px;
                text-align:left;
				display:block;
			}
			
			.nav li ul .a:hover {
                background-color:#d8d8d8;
			}
			
			.nav li ul {
                text-align:center;
				display:none;
				position:absolute;
                border-radius:5px;
                border:1px solid #d1d1d1;
                border-top:none;
				min-width:150px;
			}
			
			.trinagulo{
				display:none;
			}
			.nav li:hover > .trinagulo{
				display:block;
			}
			.nav li:hover > ul {
				display:block;
                background-color:#fff;
                text-align:center;
                color:#000;
			}
			
			.nav li ul li {
				position:relative;
                color:#000;
			}
			
			.nav li ul li ul {
				right:-140px;
				top:0px;
                color:#000;
			}
						

		</style>
    <div id="pruebas" <%=System.Configuration.ConfigurationManager.AppSettings["Ambiente"] == "1" ? "style='display: none;'" : ""%> > Ambiente de pruebas
    </div>
    <div id="produccion"  <%=System.Configuration.ConfigurationManager.AppSettings["Ambiente"] != "1" ? "style='display: none;'" : ""%> >Ambiente de Produccion
    </div>
  
<header>
  
<link href="Styles/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <div class="logo fl-lt"><asp:Image ID="Image1" ImageUrl="~/Images/logoInterna.jpg" runat="server"></asp:Image></div>
    <div class="user fl-rt" style="text-align: right">
        <%--<div class="fotoUser fl-lt" style="text-align: right"><asp:Image ID="Image2" Width="120" Height="56" ImageUrl="~/Images/LogoEmpresa.jpg" runat="server" ImageAlign="Right"></asp:Image></div>--%>
        <div class="txtBienvenido fl-rt" style="text-align:center;">
            <asp:Label style="margin-left:-40px; font-style: normal; font-weight:600; font-size:11px; margin-top:3px;" ID="lblEmpresa" CssClass="lblRol" runat="server" Text=""></asp:Label><br />
            <div id="header" style="margin-top: 15px;">
			<nav> <!-- Aqui estamos iniciando la nueva etiqueta nav -->
				<ul class="nav">
					<li style="color:#fff; background-color:none; text-align:center;"><asp:LinkButton runat="server" href="" style="color:#fff; background-color:none;"><asp:label runat="server" style="display:block; float:left; ">Idioma &nbsp<div style="transform:rotate(180deg); margin-top:4px; display:block; float:right; height: 0px; width: 0px; border-left:6px solid transparent; border-right:6px solid transparent; border-top:6px solid transparent; border-bottom:6px solid #fff;"></div><asp:Image runat="server" style="margin:0px 5px; margin-left:35px; margin-right:10px; height:15px; display:block; float:left;" ImageUrl="~/Images/Español.png"/></asp:label></asp:LinkButton><br />
                        <div class="trinagulo" style="height: 0px; width: 0px; border-left:10px solid transparent; border-right:10px solid transparent; border-top:0px solid transparent; border-bottom:8px solid #fff; margin:0px auto;"></div>
					    <ul class="links" id="equipos1"> 
							<li style="border-bottom:1px solid #eee;" onclick ="Buscar"><asp:LinkButton id="ES" CommandName ="es-CO" runat="server" OnClick="Buscar" class="a" style="color:#000;"><asp:Image style="margin-right:10px; margin-top:3px;" runat="server" ImageUrl="~/Images/Español.png"/><asp:Label runat="server" Value ="es-CO" Text="<%$  Resources:Resource,Español %>"></asp:Label></asp:LinkButton></li>
							<li style="border-bottom:1px solid #eee;" onclick ="Buscar"><asp:LinkButton id="EN" CommandName ="en-US" runat="server" OnClick="Buscar" class="a" style="color:#000;"><asp:Image style="margin-right:10px; margin-top:3px;" runat="server" ImageUrl="~/Images/Ingles.png"/><asp:Label  runat="server" Value ="en-US" Text="<%$  Resources:Resource,Ingles %>"></asp:Label></asp:LinkButton></li>
							<li style="border-bottom:1px solid #eee;" onclick ="Buscar"><asp:LinkButton id="FR" CommandName ="fr-FR" runat="server" OnClick="Buscar" class="a" style="color:#000;"><asp:Image style="margin-right:10px; margin-top:3px;" runat="server" ImageUrl="~/Images/Frances.png"/><asp:Label runat="server" Value ="fr-FR" Text="<%$  Resources:Resource,Frances %>"></asp:Label></asp:LinkButton></li>
						</ul>
					</li>
					<li style="color:#fff; background-color:none; text-align:center;"><a style="color:#fff; background-color:none;" href=""><asp:label runat="server" style="display:block; float:left; margin-left:35px;">Usuario &nbsp<div style="transform:rotate(180deg); margin-top:4px; display:block; float:right; height: 0px; width: 0px; border-left:6px solid transparent; border-right:6px solid transparent; border-top:6px solid transparent; border-bottom:6px solid #fff;"></div></asp:label></a>
                        <div class="trinagulo" style="height: 0px; width: 0px; border-left:10px solid transparent; border-right:10px solid transparent; border-top:0px solid transparent; border-bottom:8px solid #fff; margin:0px auto;"></div>
						<ul class="links" style="text-align:left;">
							<li style="border-bottom:1px solid #eee;"><asp:LinkButton runat="server" class="a"  style="color:#000;"><asp:Label style="font-size:11px; color:#353535; font-style: normal; font-weight:600" runat="server" Text="<%$  Resources:Resource,Usuario %> "></asp:Label>:<asp:Label ID="lblUser" CssClass="lblRol" runat="server" Text="" style="font-style: normal; font-size:11px;"></asp:Label></asp:LinkButton></li>
							<li style="border-bottom:1px solid #eee;"><asp:LinkButton runat="server" class="a"  style="color:#000;"><asp:Label style="font-size:11px; color:#353535; font-style: normal; font-weight:600" runat="server" Text="<%$  Resources:Resource,Tipo_Perfil %> "></asp:Label>:<asp:Label ID="lblRol" CssClass="lblRol" runat="server" Text="" style="font-style: normal; font-size:11px;"></asp:Label></asp:LinkButton></li>
							<li style="border-bottom:1px solid #eee;"><asp:LinkButton runat="server" class="a"  style="color:#000;"><asp:Label style="font-size:11px; color:#353535; font-style: normal; font-weight:600" runat="server" Text="<%$  Resources:Resource,Oficina %> "></asp:Label>:<asp:Label ID="lblOficina" CssClass="lblRol" runat="server" Text="" style="font-style: normal; font-size:11px;"></asp:Label></asp:LinkButton></li>
							<li style="text-align:center;"><asp:LinkButton runat="server" id="LinkCerrarSession"   CommandName ="fr-FR"  OnClick="hlkCerrar_Click" class="a" style="color:#000; font-size:11px; font-weight:600"><asp:Label ID="hlkCerrar" onclick="hlkCerrar_Click" runat="server" Text="<%$  Resources:Resource,Cerrar_Sesion %>"></asp:Label></asp:LinkButton></li>
						</ul>
					</li>
				</ul>
			</nav><!-- Aqui estamos cerrando la nueva etiqueta nav -->
    </div>
    </div>
    </div>
</header>