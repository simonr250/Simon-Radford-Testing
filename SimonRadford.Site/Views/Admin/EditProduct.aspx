<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ProductViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Product
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>

    <h2>Edit Product</h2>

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
        <%: Html.ActionLink("Cancel", "Index") %>
    </div>

</asp:Content>
