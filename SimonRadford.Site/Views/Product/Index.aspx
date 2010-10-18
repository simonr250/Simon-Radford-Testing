<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ProductListViewModel>" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Product List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script type="text/javascript">
	var $jq14 = jQuery.noConflict();
</script>
<script src="/Scripts/jquery.infinitescroll.js" type="text/javascript"></script>


<script type ="text/javascript" >
	$jq14(document).ready(function () {
	/*	$jq14("table.grid").infinitescroll({
			navSelector: "div.pagination",
			// selector for the paged navigation (it will be hidden)
			nextSelector: "div.pagination a:contains('next')",
			// selector for the NEXT link (to page 2)
			itemSelector: "table tr.gridrow, table tr.gridrow_alternate",
			// selector for all items you'll retrieve
			donetext: "All products displayed",
			animate: true
			//extraScrollPx: 200
		}); */
	});
</script>
    <h2>Product List</h2>
    <p>
        <%: Html.ActionLink("Enter New Product for Review", "CreateProduct") %>
    </p>
	<% using(Html.BeginForm(new { Action = "Index"})) { %> 
	<%: Html.TextBox("SearchWord") %> 
	<input type="submit" value="Search"/>
	<% } %> 
	
	<br />

	<% if (Model.SearchWord != null)
    {%>
	<p>Search results for "<%:Model.SearchWord%>"</p><%
    }%>

	<%: Html.Grid<ProductListViewModelRow>("ProductListRows").Sort(ViewData["sort"] as GridSortOptions).Columns(column =>
{
          column.For(prod => prod.ProductCode).Named("Product Code").SortColumnName("ProductCode");
		  column.For(prod => Html.ActionLink(prod.ProductName, "ProductDetails", new{id=prod.Id})).Named("Name").SortColumnName("ProductName");
		  column.For(prod => prod.ManafacturerName).Named("Manafacturer").SortColumnName("ManafacturerName");
		  column.For(prod => "£"+prod.Price).Named("Price").SortColumnName("Price");
	})%>
<%= Html.Pager((IPagination)Model.ProductListRows)%>
	
	<script type="text/javascript">
		$jq14("tr").mouseover(function () {
			$jq14(this).css("background-color", "#CCCCCC");
		});
		$jq14("tr").mouseleave(function () {
			$jq14(this).css("background-color", "white");

		});
	</script>

</asp:Content>

