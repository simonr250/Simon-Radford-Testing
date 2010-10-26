<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Product/Product.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ProductListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<h2>Complete Product List</h2>
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

		$jq14("li:contains('Complete Product List')").css("border", "3px solid #990000");
	
	});
</script>
    
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

