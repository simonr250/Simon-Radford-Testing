<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ReviewListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Product Reviews
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../Content/ViewReviews.css" rel="stylesheet" type="text/css" />
    <h2>Reviews for <%:Model.Product.Name %> by <%:Model.Manafacturer.Name %></h2>
    <p>
    <%
        int total = 0; %>
    <% for (int i=0; i<Model.Reviews.Count; i++) %>
    <%
       {
           total = total + Model.Reviews[i].Rating;
       } %>
       <% double avg = total/Model.Reviews.Count; %>
    The Average rating for this product is <%: avg %>
    </p>
     <p>
        <%: Html.ActionLink("Submit a review", "SubmitReview", new{productId=Model.Product.ProductId}) %>
    </p>
       <table>
        <tr>
            <th>
                Reviewed By
            </th>
            <th>
                Rating
            </th>
            <th class="reviewDetailColumn">
                Review
            </th>
        </tr>

    <% for (int i=0; i < Model.Reviews.Count(); i++) { %>
    
        <tr>
            <td>
                <%: Model.Submitters[i].Name %>
            </td>
            <td>
                <%: Model.Reviews[i].Rating %>
            </td>
            <td class="reviewDetailColumn">
                <%: Model.Reviews[i].Detail %>
            </td>
        </tr>
    
    <% } %>

    </table>
    <p>
        <%: Html.ActionLink("Back to product details", "ProductDetails", new{productId=Model.Product.ProductId}) %>
    </p>

</asp:Content>

