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
<script src ="/Scripts/jquery.mvcajax.js" type="text/javascript"></script>
<script src="/Scripts/jquery.infinitescroll.js" type="text/javascript"></script>

<script type ="text/javascript" >
	$(document).ready(function () {
	
		var searchWord = '<%:Model.SearchWord%>'; 
    
		$("#product_grid").mvcajax("/Product/Sort/","ProductGrid", searchWord ,  {
			defaultsort: "ProductCode"
		});
	});
</script>
    <h2>Product List</h2>
    
	<% using(Html.BeginForm(new { Action = "Index"})) { %> 
	<%: Html.TextBox("SearchWord") %> 
	<input type="submit" value="Search"/>
	<% } %> 
	
	<br />

	<% if (Model.SearchWord != null)
    {%>
	<p>Search results for "<%:Model.SearchWord%>"</p><%
    }%>
	
	<div id="product_grid">
    </div>

	<script type="text/javascript">
		$("tr").mouseover(function () {
			$(this).css("background-color", "#CCCCCC");
		});
		$("tr").mouseleave(function () {
			$(this).css("background-color", "white");

		});
	</script>

</asp:Content>

