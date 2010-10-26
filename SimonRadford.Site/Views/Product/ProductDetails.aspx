<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Product/Product.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ProductViewModel>" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>
<%@ Import Namespace="SimonRadford.Site.HtmlHelpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<h2>Product Details for <%: Model.ProductName %></h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>

<script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script type="text/javascript">
	var $jq14 = jQuery.noConflict();
</script>
<script src="/Scripts/jquery.infinitescroll.js" type="text/javascript"></script>

<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/rating.js" type="text/javascript"></script>

   <script type="text/javascript">
   	$(document).ready(function () {

   		$jq14("table.grid").infinitescroll({
   			debug: true,

   			navSelector: "div.pagination",
   			// selector for the paged navigation (it will be hidden)
   			nextSelector: "div.pagination a:contains('next')",
   			// selector for the NEXT link (to page 2)
   			itemSelector: "table tr.gridrow, table tr.gridrow_alternate",
   			// selector for all items you'll retrieve
   			donetext: "All reviews displayed",
   			loadingText: "Loading reviews...",
   			animate: true,
   			extraScrollPx: 150
   		});

   		$('#rate1').rating('/Home/SubmitReview/2', { maxvalue: 5 });
   		// $("table").slideDown(1000, function () { });

   		/*  Formatting reviews in different colours depending on rating
		1 = red
		3 = amber
		5 = green

   		$jq14("tr:contains('1'):contains('Report this review')").css("background-color", "#CC3333")
   		$jq14("tr:contains('2'):contains('Report this review')").css("background-color", "#FF9966")
   		$jq14("tr:contains('3'):contains('Report this review')").css("background-color", "#FFFF66")
   		$jq14("tr:contains('4'):contains('Report this review')").css("background-color", "#99FF99")
   		$jq14("tr:contains('5'):contains('Report this review')").css("background-color", "#66CC66")
   		*/
   	});   
	 </script>
 
 <link href="../../Content/rating.css" rel="stylesheet" type="text/css" />

<div class="container">
	 <div id="productDetailsPanel">
    <fieldset>
        <table>
            <tr>
        <th><div class="display-label">Product Code</div></th>
        <td><div class="display-field"><%: Model.ProductCode %></div></td>
            </tr>
            <tr>
        <th><div class="display-label">Name</div></th>
        <td><div class="display-field"><%: Model.ProductName %></div></td>
            </tr>
            <tr>
        <th><div class="display-label">Manafacturer</div></th>
        <td><div class="display-field"><%: Model.ManafacturerName %></div></td>
            </tr>
            <tr>
        <th><div class="display-label">Price</div></th>
        <td><div class="display-field">£<%: String.Format("{0:F}", Model.Price) %></div></td>
            </tr>
            <tr>
        <th><div class="display-label">Description</div></th>
        <td><div class="display-field"><%: Model.Description %></div></td>
            </tr>
        </table>
    </fieldset>
    
	</div>
	<div id="reviewsPanel">
        <h2>Reviews for <%:Model.ProductName%> by <%:Model.ManafacturerName%></h2>
    <p>
    There are <%: Model.TotalReviewRows %> user reviews. <% if (Model.TotalReviewRows > 0)
                                                            {%>The Average rating for this product is <%:Model.AverageRating%> <%
                                                            }%>
    </p>
     <p>
        <a href="#form0">Submit a Review</a>
    </p>
       <div id="ScrollingTable">
<%: Html.Grid<ReviewRowModel>("ReviewRows").Sort(ViewData["sort"] as GridSortOptions).Columns(column =>
   {
	   column.For(rev => rev.SubmitterName).Named("Name").SortColumnName("SubmitterName").Attributes(width => "150px");
	   column.For(rev => (rev.Rating == 1) ? "<Image src=\"/content/star_1.jpg\" alt =\"1\">" : (rev.Rating == 2) ? "<Image src=\"/content/star_2.jpg\" alt =\"2\">" : (rev.Rating == 3) ? "<Image src=\"/content/star_3.jpg\" alt =\"3\">" : (rev.Rating == 4) ? "<Image src=\"/content/star_4.jpg\" alt =\"4\">" : (rev.Rating == 5) ? "<Image src=\"/content/star_5.jpg\" alt =\"5\">" : rev.Rating.ToString()).Encode(false).Named("Rating").Attributes(width => "100px").HeaderAttributes(sortcolumn => "Rating");
	   column.For(rev => rev.DetailedReview).Named("Review").SortColumnName("DetailedReview").Attributes(width => "700px");
	   column.For(rev => (rev.Flagged == true) ? "<Image src=\"/content/reported_review.jpg\" alt =\"This review has been reported to the administrators\" title =\"This review has been reported to the administrators\" class=\"actionLink\">" : Ajax.ImageActionLink("/Content/report_review.jpg", "Report this review to an administrator", "FlagReview", new { rev.Id }, new AjaxOptions())).Named("").Attributes(width => "30px").Encode(false);
	})%>
	<%= Html.Pager((IPagination)Model.ReviewRows)%>
	</div>

    <p>
	<a href="#title">Back to product details</a>
	</p>

	<!-- Submit review form begins -->

<%Html.EnableClientValidation();%>
<%using (Html.BeginForm()){%>
<%:Html.ValidationSummary(true)%>

<fieldset>
	<legend>Submit a review for <%:Model.ProductName%></legend>
	<p>
		<%:Html.LabelFor(model => model.SubmitterName)%>
		<%:Html.TextBoxFor(model => model.SubmitterName)%>
		<%:Html.ValidationMessageFor(model => model.SubmitterName)%>
	</p>

	<p>
		<%:Html.LabelFor(model => model.Rating)%>
		<%:Html.HiddenFor(model => model.Rating)%><%:Html.ValidationMessageFor(model => model.Rating)%>
	</p>
	
	<div id="rate1" class="rating"></div>
	
	<p>
		<%:Html.LabelFor(model => model.DetailedReview)%>
		<%:Html.ValidationMessageFor(model => model.DetailedReview)%>
		<%:Html.TextAreaFor(model => model.DetailedReview)%>
	</p> 
	
	<p>
		<input type="submit" value="Submit your review"  />
	</p>
</fieldset>

<%
}%>
    </div>
   </div> 
   <p>
        <%: Html.ActionLink("Back to Product List", "Index") %>
    </p>

</asp:Content>

