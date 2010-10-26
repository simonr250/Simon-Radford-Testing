<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Admin/Admin.Master" Inherits="System.Web.Mvc.ViewPage<SimonRadford.Site.ViewModels.ManafacturerViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 <h2>Manafacturer Details</h2>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src ="/Scripts/jquery.mvcajax.js" type="text/javascript"></script>
<script type ="text/javascript" >
	$(document).ready(function () {

		var searchWord = '<%:Model.SearchWord%>';
		var id = '<%:Model.ManafacturerId%>';

		$("#product_grid").mvcajaxManafacturerView("/Admin/SortProductList/", "ProductGrid", searchWord, id, {
			defaultsort: "ProductCode"
		});

		$jq14("li:contains('Manafacturers and Products')").css("border", "3px solid #990000");
	
	});

	function DeleteRefreshGrid() {
		var manafacturerId = '<%:Model.ManafacturerId %>';
		var searchWord = '<%:Model.SearchWord %>';
		UpdateGridManafacturerView('#product_grid', '/Admin/SortProductList/', 'ProductGrid','ProductCode', 1, searchWord, manafacturerId);
	}
	
</script>

	<p>
	<%: Html.ActionLink( "Edit manafacturer's details", "EditManafacturer", new {id = Model.ManafacturerId}) %>
	</p>
	<% using(Html.BeginForm(new { Action = "ViewManafacturer"})) { %> 
    <table>
        <tr>
            <th>
                Manafacturer Name
            </th>
            <td>
                <%: Model.ManafacturerName  %>
            </td>
        </tr>
		<tr>
            <th>
                Website
            </th>
            <td>
                <%: Html.ActionLink(Model.ManafacturerWebsite , Model.ManafacturerWebsite)  %>
            </td>
        </tr>
		</table>

		<%: Html.HiddenFor(model => model.ManafacturerId) %>
    
	<p>Product List for <%: Model.ManafacturerName %></p>
	
	<p>
	<%: Html.ActionLink("Add a new product", "CreateProduct", new {id= Model.ManafacturerId}) %> , or click on a product name to edit.
	</p>

		
	<%: Html.TextBox("SearchWord") %> 
	<input type="submit" value="Search Product List"/>
	<% } %> 
	
	<br />

	<% if (Model.SearchWord != null)
    {%>
	<p>Search results for "<%:Model.SearchWord%>"</p><%
    }%>
	
	<div id="product_grid">
    </div>


    <p>
        <%: Html.ActionLink("Back to manafacturers List", "Index", new {id = Model.ManafacturerId}) %>
    </p>

</asp:Content>

