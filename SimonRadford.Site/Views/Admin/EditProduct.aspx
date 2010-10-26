<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ProductViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<h2>Edit Product</h2>
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
                <%: Html.LabelFor(model => model.ProductCode) %>
                <%: Html.TextBoxFor(model => model.ProductCode) %>
                <%: Html.ValidationMessageFor(model => model.ProductCode) %>
            </p>
            
            <p class="editor-label">
                <%: Html.LabelFor(model => model.ProductName) %>
                <%: Html.TextBoxFor(model => model.ProductName) %>
                <%: Html.ValidationMessageFor(model => model.ProductName) %>
            </p>
            
            <p class="editor-label">
                <%: Html.LabelFor(model => model.ManafacturerName) %>
				<%: Html.DropDownListFor(model => model.ManafacturerName, new SelectList(Model.ManafacturerNames as IEnumerable))%>
                <%: Html.ValidationMessageFor(model => model.ManafacturerName) %>
            </p>
            
            <p class="editor-label">
                <%: Html.LabelFor(model => model.Price) %>
                <%: Html.TextBoxFor(model => model.Price) %>
                <%: Html.ValidationMessageFor(model => model.Price) %>
            </p>
            
            <p class="editor-label">
                <%: Html.LabelFor(model => model.Description) %>
                <%: Html.TextAreaFor(model => model.Description) %>
                <%: Html.ValidationMessageFor(model => model.Description) %>
            </p>
            
			
			<%: Html.HiddenFor( model => model.ProductId) %>

            <p>
                <input type="submit" value="Save Product Details" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Cancel", "ViewManafacturer", new {id = Model.ManafacturerId}) %>
    </div>

</asp:Content>
