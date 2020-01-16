<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="ctrl_menu" %>

<div class="fecha"><asp:Label ID="lblFecha" runat="server" Text=""></asp:Label></div>
    <h1><asp:Label ID="lblModulo" runat="server" Text=""></asp:Label></h1>
    <nav>
    <asp:PlaceHolder ID="phOpciones" runat="server"></asp:PlaceHolder>
</nav>