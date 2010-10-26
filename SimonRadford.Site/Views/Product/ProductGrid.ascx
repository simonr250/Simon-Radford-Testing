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
	column.For(prod => Html.ActionLink(prod.ProductName, "ProductDetails", new { id = prod.Id })).Named("Name").HeaderAttributes(sortcolumn => "ProductName").Attributes(width => "300px");
	column.For(prod => prod.ManafacturerName).Named("Manafacturer").HeaderAttributes(sortcolumn => "ManafacturerName").Attributes(width => "100px");
	column.For(prod => "£" + prod.Price).Named("Price").HeaderAttributes(sortcolumn => "Price").Attributes(width => "40px");
	column.For(rev => (rev.AverageRating == 1) ? "<Image src=\"/content/star_1.jpg\" alt =\"1\">" : (rev.AverageRating == 2) ? "<Image src=\"/content/star_2.jpg\" alt =\"2\">" : (rev.AverageRating == 3) ? "<Image src=\"/content/star_3.jpg\" alt =\"3\">" : (rev.AverageRating == 4) ? "<Image src=\"/content/star_4.jpg\" alt =\"4\">" : (rev.AverageRating == 5) ? "<Image src=\"/content/star_5.jpg\" alt =\"5\">" : (rev.AverageRating == 0) ? "No ratings" :rev.AverageRating.ToString()).Encode(false).Named("Average Rating").Attributes(width => "100px").HeaderAttributes(sortcolumn => "AverageRating"); 
	})%>
</div>
<div style="width:300px;float:left;">
<%=Html.Pager((IPagination)ViewData["ProductListRows"]) %> 
</div>
<div style="width:300px;margin-left:300px;text-align:right;">
<%= Html.PageDropDown((IPagination)ViewData["ProductListRows"])%>
</div>
