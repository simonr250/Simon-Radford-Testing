<%@  Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.SubmitReivewListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<!--<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>-->
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/rating.js" type="text/javascript"></script>

<link href="../../Content/SubmitReview.css" rel="stylesheet" type="text/css" />
<link href="../../Content/rating.css" rel="stylesheet" type="text/css" />

<script type ="text/javascript">
$(document).ready(function() {
	
	$('#rate1').rating('/Home/SubmitReview', {maxvalue:5});
	
});
</script>


<% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Submit a review for <%: Model.Product.Name %></legend>
            <p>
                Your Name
                </p>
                <p>
                <%: Html.TextBoxFor(model => model.Submitter.Name)%>
                <%: Html.ValidationMessageFor(model => model.Submitter.Name)%>
            </p>
             <p>
               Rating (from 1 to 5, where 5 is highest)
                </p>
                <p>
                 <%: Html.TextBoxFor(model => model.Review.Rating)%>
                <div id="rate1" class="rating"></div>
                <div class="implementation"></div>
                
                <%: Html.ValidationMessageFor(model => model.Review.Rating)%>
              
            </p>
             <p>
                Detailed Review
                </p>
                <p>
                <%: Html.TextAreaFor(model => model.Review.Detail)%>
                </p>
                <p>
                <%: Html.ValidationMessageFor(model => model.Review.Detail)%>
            </p>
             
            <p>
                <input type="submit" value="Submit your review"  />
            </p>
        </fieldset>

    <%
} %>

    <div>
        <%: Html.ActionLink("Cancel", "ViewReviews", new { productId = Model.Product.ProductId })%>
    </div>

</asp:Content>
