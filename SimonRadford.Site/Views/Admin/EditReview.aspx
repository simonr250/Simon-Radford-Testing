<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ReviewRowModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<h2>Edit review for <%:Model.Product%> by <%:Model.Manafacturer%></h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>

<script type ="text/javascript" >
	$jq14(document).ready(function () {
		$jq14("li:contains('Reported Reviews')").css("border", "3px solid #990000");
	});
</script>

    <%Html.EnableClientValidation();%>
<%using (Html.BeginForm()){%>
<%:Html.ValidationSummary(true)%>

<fieldset>
	<p>
		<%:Html.LabelFor(model => model.SubmitterName)%>
		<%:Html.TextBoxFor(model => model.SubmitterName)%>
		<%:Html.ValidationMessageFor(model => model.SubmitterName)%>
	</p>

	<p>
		<%:Html.LabelFor(model => model.Rating)%>
		<%:Html.TextBoxFor(model => model.Rating)%><%:Html.ValidationMessageFor(model => model.Rating)%>
		<%:Html.ValidationMessageFor(model => model.Rating)%>
	</p>
	
	<p>
		<%:Html.LabelFor(model => model.DetailedReview)%>
		<%:Html.ValidationMessageFor(model => model.DetailedReview)%>
		<%:Html.TextAreaFor(model => model.DetailedReview)%>
	</p> 
	<%: Html.HiddenFor(model => model.Id) %>
	<p>
		<input type="submit" value="Save review"  />
	</p>
</fieldset>

<%
}%>
<div>
        <%: Html.ActionLink("Cancel", "ReportedReviews") %>
    </div>


</asp:Content>

