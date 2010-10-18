﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ManafacturerViewModel>" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewManafacturer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<!--
<script type ="text/javascript">
	$(document).ready(function () {
		$("table").slideDown(1000, function () { });
	});
</script> -->

    <h2>View Manafacturer</h2>

    <table>
        <tr>
            <th>
                Manafacturer Name
            </th>
            <td>
                <%: Model.ManafacturerName  %>
            </td>
        </tr>
		<tr>
            <th>
                Website
            </th>
            <td>
                <%: Html.ActionLink(Model.ManafacturerWebsite , Model.ManafacturerWebsite)  %>
            </td>
        </tr>
		</table>
    
	<p>Product List for <%: Model.ManafacturerName %></p>

	<%: Html.Grid<ProductListViewModelRow>("ProductListRows").Sort(ViewData["sort"] as GridSortOptions).Columns(column =>
{
          column.For(prod => prod.ProductCode).Named("Product Code").SortColumnName("ProductCode");
		  column.For(prod => Html.ActionLink(prod.ProductName, "ProductDetails", "Product", new { id = prod.Id }, null)).Named("Name").SortColumnName("ProductName");
		  column.For(prod => "£"+prod.Price).Named("Price").SortColumnName("Price");
	})%>
<%= Html.Pager((IPagination)Model.ProductListRows)%>

    <p>
        <%: Html.ActionLink("Back to manafacturers List", "Index") %>
    </p>

</asp:Content>

