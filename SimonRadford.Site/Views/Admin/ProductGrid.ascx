<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.HtmlHelpers" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>


<div class="scrollTableContainer">
<%: Html.Grid<ProductListViewModelRow>("ProductListRows").Sort(ViewData["sort"] as GridSortOptions).Columns(column =>
{
	column.For(prod => prod.ProductCode).Named("Product Code").HeaderAttributes(sortcolumn => "ProductCode").Attributes(width =>"100px");
	column.For(prod => Html.ActionLink(prod.ProductName, "EditProduct", new { id = prod.Id })).Named("Name").HeaderAttributes(sortcolumn => "ProductName").Attributes(width => "300px");
	column.For(prod => prod.ManafacturerName).Named("Manafacturer").HeaderAttributes(sortcolumn => "ManafacturerName").Attributes(width => "100px");
	column.For(prod => "£" + prod.Price).Named("Price").HeaderAttributes(sortcolumn => "Price").Attributes(width => "40px");

	column.For(prod => Ajax.ImageActionLink("/Content/Icon_delete.gif", "Delete", "DeleteProduct", new { prod.Id }, 
		new AjaxOptions{Confirm = "Are you sure you want to delete this product and all associated reviews?"
			}))
		.Attributes(width => "20px").Encode(false).Sortable(false);
	})%>
</div>
<div style="width:300px;float:left;">
<%=Html.Pager((IPagination)ViewData["ProductListRows"]) %> 
</div>
<div style="width:300px;margin-left:300px;text-align:right;">
<%= Html.PageDropDown((IPagination)ViewData["ProductListRows"])%>
</div>
