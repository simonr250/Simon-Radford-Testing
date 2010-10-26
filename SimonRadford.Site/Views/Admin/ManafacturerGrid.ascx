<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.HtmlHelpers" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>
<div class="scrollTableContainer">
<%: Html.Grid<ManafacturerListViewModelRow>("ManafacturerListRows").Sort(ViewData["sort"] as GridSortOptions).Columns(
	column =>
	    {
	        column.For(man => Html.ActionLink(man.ManafacturerName, "ViewManafacturer", new {id = man.ManafacturerId})).
	            Named("Manafacturer Name").HeaderAttributes(sortcolumn => "ManafacturerName").Attributes(width => "200px");
			column.For(man => Ajax.ImageActionLink("/Content/Icon_delete.gif", "Delete Manafacturer", "DeleteManafacturer", new { id = man.ManafacturerId },
	new AjaxOptions
	{
		Confirm = "Are you sure you want to delete this manafacturer and all associated products and reviews?"
	}))
	.Attributes(width => "20px").Encode(false).Sortable(false);
	    }
                           )%>
</div>
<div style="width:300px;float:left;">
<%=Html.Pager((IPagination)ViewData["ManafacturerListRows"]) %> 
</div>
<div style="width:300px;margin-left:300px;text-align:right;">
<%= Html.PageDropDown((IPagination)ViewData["ManafacturerListRows"])%>
</div>