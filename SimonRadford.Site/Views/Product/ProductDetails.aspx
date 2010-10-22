<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ProductViewModel>" %>
<%@ Import Namespace="MvcContrib.Pagination" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="MvcContrib.UI.Pager" %>
<%@ Import Namespace="SimonRadford.Site.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Product Details for <%: Model.ProductName %>
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

   		$jq14("tr:contains('1'):contains('Report this review')").css("background-color", "#CC3333")
   		$jq14("tr:contains('2'):contains('Report this review')").css("background-color", "#FF9966")
   		$jq14("tr:contains('3'):contains('Report this review')").css("background-color", "#FFFF66")
   		$jq14("tr:contains('4'):contains('Report this review')").css("background-color", "#99FF99")
   		$jq14("tr:contains('5'):contains('Report this review')").css("background-color", "#66CC66")
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
          column.For(rev => rev.SubmitterName).Named("Name").SortColumnName("SubmitterName");
		  column.For(rev => rev.Rating);
		  column.For(rev => rev.DetailedReview).Named("Review").SortColumnName("DetailedReview");
		  column.For(rev => Ajax.ActionLink("Report this review", "FlagReview", new { rev.Id }, new AjaxOptions())).Named("");
	})%>
	<%= Html.Pager((IPagination)Model.ReviewRows)%>
	</div>
    
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

