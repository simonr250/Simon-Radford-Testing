<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.HtmlHelpers" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>


<div class="scrollTableContainer">
<%: Html.Grid<ReviewRowModel>("ReportedReviewListRows").Sort(ViewData["sort"] as GridSortOptions).Columns(column =>
{
	column.For(rev => rev.Manafacturer).Named("Manafacturer").HeaderAttributes(sortcolumn => "Manafacturer").Attributes(width => "100px");
	column.For(rev => rev.Product).Named("Product").HeaderAttributes(sortcolumn => "Product").Attributes(width => "200px");
	column.For(rev => rev.SubmitterName).Named("Name").HeaderAttributes(sortcolumn => "SubmitterName").Attributes(width => "150px");
	column.For(rev => (rev.Rating == 1) ? "<Image src=\"/content/star_1.jpg\" alt =\"1\">" : (rev.Rating == 2) ? "<Image src=\"/content/star_2.jpg\" alt =\"2\">" : (rev.Rating == 3) ? "<Image src=\"/content/star_3.jpg\" alt =\"3\">" : (rev.Rating == 4) ? "<Image src=\"/content/star_4.jpg\" alt =\"4\">" : (rev.Rating == 5) ? "<Image src=\"/content/star_5.jpg\" alt =\"5\">" : rev.Rating.ToString()).Encode(false).Named("Rating").Attributes(width => "100px").HeaderAttributes(sortcolumn => "Rating");
	column.For(rev => rev.DetailedReview).Named("Review").HeaderAttributes(sortcolumn => "DetailedReview").Attributes(width => "600px");

	column.For(prod => Ajax.ImageActionLink("/Content/tick.jpg", "Mark this review as acceptable", "UnflagReview", new { prod.Id },
	new AjaxOptions()))
	.Attributes(width => "20px").Encode(false).Sortable(false);

	column.For(rev => Html.ActionImage("Admin", "EditReview", new { rev.Id }, "/Content/edit.jpg", "Edit this review", null, null
	))
	.Attributes(width => "20px").Encode(false).Sortable(false);
		
	column.For(rev => Ajax.ImageActionLink("/Content/Icon_delete.jpg", "Delete Review", "DeleteReview", new { id = rev.Id },
	new AjaxOptions
	{
		Confirm = "Are you sure you want to delete this review?"
	}))
	.Attributes(width => "20px").Encode(false).Sortable(false);

	    
	})%>


</div>
<div style="width:300px;float:left;">
<%=Html.Pager((IPagination)ViewData["ReportedReviewListRows"]) %> 
</div>
<div style="width:300px;margin-left:300px;text-align:right;">
<%= Html.PageDropDown((IPagination)ViewData["ReportedReviewListRows"])%>
</div>