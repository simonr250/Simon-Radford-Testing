<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Product/Product.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ProductListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<h2>Top Rated Products</h2>
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

		$("#top_rated_product_grid").mvcajax("/Product/SortTopRated/", "ProductGrid", searchWord, {
			defaultsort: "AverageRating"
		});

		$jq14("li:contains('Top Rated Products')").css("border", "3px solid #990000");
	
	});
</script>
	
	<div id="top_rated_product_grid">
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
