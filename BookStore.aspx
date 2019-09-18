<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookStore.aspx.cs" Inherits="BookStore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Store</title>
    <link href="App_Themes/SiteStyles.css" rel="stylesheet" />
</head>
<body>
    <h1>Online Book Store</h1>
    <asp:Panel runat="server">
    <form id="form1" runat="server">
        <a  href="ShoppingCartView.aspx">View Cart</a> (<asp:Label runat="server" ID="lblNumItems"></asp:Label>) <br /><br />
        <asp:DropDownList  ID="drpBookSelection" runat="server" CssClass="dropdown" 
            OnSelectedIndexChanged="drpBookSelection_SelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem Value="-1">Select a Book ... </asp:ListItem>
        </asp:DropDownList><br />
        
        <%-- todo: Add Required Field Validator --%>
        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="drpBookSelection" InitialValue="-1" CssClass="error" Display="Dynamic" ErrorMessage="Must Select One"></asp:RequiredFieldValidator>
        
        <div class="description">
            <asp:Label runat="server" ID="lblDescription"></asp:Label>
        </div>
        <br />
        <span class="emphsis">Price: </span><asp:Label runat="server" ID="lblPrice" CssClass="Price" ></asp:Label>                
        <span class="emphsis">Quantity: </span><asp:TextBox runat="server" ID="txtQuantity" cssclass="input"/>
        
        <%-- todo: Add Required Field Validator --%>
        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtQuantity" CssClass="error" Display="Dynamic" ErrorMessage="Required!"></asp:RequiredFieldValidator>
        <%-- todo: Add Range Validator --%>
        <asp:RangeValidator runat="server" ID="RangeValidator1" Type="Integer" ControlToValidate="txtQuantity" CssClass="error" MinimumValue="1" MaximumValue="20" Display="Dynamic" ErrorMessage="Must be between 1 to 20"></asp:RangeValidator>
        

        <br /><br /><asp:Button runat="server" ID="btnAddToCart" Text="Add to Cart" cssclass="button" OnClick="btnAddToCart_Click"/>
    </form>  
    </asp:Panel>
</body>
</html>

