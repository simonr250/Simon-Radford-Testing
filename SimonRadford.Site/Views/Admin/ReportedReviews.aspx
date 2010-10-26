<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ReportedReviewsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<h2>Reported Reviews</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src="/Scripts/jquery.infinitescroll.js" type="text/javascript"></script>
<script src ="/Scripts/jquery.mvcajax.js" type="text/javascript"></script>
<script type ="text/javascript" >
	$(document).ready(function () {
		var searchWord = '<%:Model.SearchWord%>';
		$("#reviews_grid").mvcajax("/Admin/SortReviewList/", "ReviewGrid", searchWord, {
			defaultsort: "SubmitterName"
		});
		
		$jq14("li:contains('Reported Reviews')").css("border", "3px solid #990000");
	
	});

	function RefreshGrid() {
		var searchWord = '<%:Model.SearchWord %>';
		UpdateGrid('#reviews_grid', '/Admin/SortReviewList/', 'ReviewGrid', 'SubmitterName', 1, searchWord);
	}
</script>	
<p>View, edit, delete or unflag all reviews that have been reported by users</p>
	<% using(Html.BeginForm(new { Action = "ReportedReviews"})) { %> 
	<%: Html.TextBox("SearchWord") %> 
	<input type="submit" value="Search"/>
	<% } %> 
	
	<br />

	<% if (Model.SearchWord != null)
    {%>
	<p>Search results for "<%:Model.SearchWord%>"</p><%
    }%>

<div id="reviews_grid">
    </div>
</asp:Content>
