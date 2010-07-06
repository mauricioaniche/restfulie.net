<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Item>" %>
<%@ Import Namespace="Web.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Get
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Get</h2>
    
    Id: <%= Model.Id %><br />
    Name: <%= Model.Nome %><br />
    Price: <%= Model.Preco %><br />

</asp:Content>
