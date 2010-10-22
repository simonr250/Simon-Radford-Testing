<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ManafacturerListViewModel>" %>

<%@ Import Namespace="MvcContrib.Pagination" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manafacturer List
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
		$("#manafacturer_grid").mvcajax("/Admin/SortManafacturerList/", "ManafacturerGrid", searchWord, {
			defaultsort: "ManafacturerName"
		});
	});

	function DeleteRefreshGrid() {
		var searchWord = '<%:Model.SearchWord %>';
		UpdateGrid('#manafacturer_grid', '/Admin/SortManafacturerList/', 'ManafacturerGrid', 'ManafacturerName', 1, searchWord);
	}
</script>
	<h3><%:Html.ActionLink("Manage reviews", "Index")%> </h3>
    <h2>Manage manafacturers and products</h2> 

    <p>
        <%: Html.ActionLink("Add a new Manafacturer", "AddNewManafacturer") %>, or click on a manafacturer name to edit and manage products
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

<div id="manafacturer_grid">
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

