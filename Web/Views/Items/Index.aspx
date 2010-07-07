<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Item>>" %>
<%@ Import Namespace="Web.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index.aspx
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Index.aspx</h2>

<% foreach (var item in Model)
   { %>
<p>
<%= item.Name %>, <%= item.Price %>
</p>
<% } %>

<form action="<%= Url.Action("Save") %>" method="post">
    Name: <input type="text" name="item.Name" />
    Price: <input type="text" name="item.Price" />
    
    <input type="submit" value="Add new item" />
</form>
</asp:Content>
