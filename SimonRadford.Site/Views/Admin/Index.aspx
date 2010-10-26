<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ManafacturerListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<h2>Manafacturer List</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src ="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src ="/Scripts/jquery.mvcajax.js" type="text/javascript"></script>
<script type ="text/javascript" >
	$(document).ready(function () {
		var searchWord = '<%:Model.SearchWord%>';
		$("#manafacturer_grid").mvcajax("/Admin/SortManafacturerList/", "ManafacturerGrid", searchWord, {
			defaultsort: "ManafacturerName"
		});

	
		$jq14("li:contains('Manafacturers and Products')").css("border", "3px solid #990000");
	
	});

	function RefreshGrid() {
		var searchWord = '<%:Model.SearchWord %>';
		UpdateGrid('#manafacturer_grid', '/Admin/SortManafacturerList/', 'ManafacturerGrid', 'ManafacturerName', 1, searchWord);
	}
</script>

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

</asp:Content>

