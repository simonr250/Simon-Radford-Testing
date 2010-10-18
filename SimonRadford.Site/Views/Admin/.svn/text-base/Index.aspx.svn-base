<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ManafacturerListViewModel>" %>

<%@ Import Namespace="MvcContrib.Pagination" %>

<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Manafacturer List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src="/Scripts/jquery.infinitescroll.js" type="text/javascript"></script>
<script type="text/javascript">
	var $jq14 = jQuery.noConflict();
</script>

    <h2>ManafacturersList</h2>

    <p>
        <%: Html.ActionLink("Add a new Manafacturer", "AddNewManafacturer") %>
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

	<%: Html.Grid<ManafacturerListViewModelRow>("ManafacturerListRows").Sort(ViewData["sort"] as GridSortOptions).Columns(column => column.For(man => Html.ActionLink(man.ManafacturerName, "ViewManafacturer", new { id = man.ManafacturerId }))
	.Named("Manafacturer Name").SortColumnName("ManafacturerName"))%>
<%= Html.Pager((IPagination)Model.ManafacturerListRows)%>


	<script type="text/javascript">
		$jq14("tr").mouseover(function () {
			$jq14(this).css("background-color", "#CCCCCC");
		});
		$jq14("tr").mouseleave(function () {
			$jq14(this).css("background-color", "white");

		});
	</script>

</asp:Content>

