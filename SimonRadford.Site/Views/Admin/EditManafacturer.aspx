<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ManafacturerViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<h2>Edit Manafacturer Details</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>

<script type ="text/javascript" >
	$jq14(document).ready(function () {
		$jq14("li:contains('Manafacturers and Products')").css("border", "3px solid #990000");
	});
</script>

<% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <p class="editor-label">
                <%: Html.LabelFor(model => model.ManafacturerName) %>
                <%: Html.TextBoxFor(model => model.ManafacturerName) %>
                <%: Html.ValidationMessageFor(model => model.ManafacturerName) %>
            </p>
            
            <p class="editor-label">
                <%: Html.LabelFor(model => model.ManafacturerWebsite) %>
                <%: Html.TextBoxFor(model => model.ManafacturerWebsite) %>
                <%: Html.ValidationMessageFor(model => model.ManafacturerWebsite) %>
            </p>
            <%: Html.HiddenFor(model => model.ManafacturerId) %>
            <p>
                <input type="submit" value="Save manafacturer details" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Cancel", "ViewManafacturer", new {id = Model.ManafacturerId}) %>
    </div>

</asp:Content>

