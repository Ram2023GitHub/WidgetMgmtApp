<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WidgetEdit.aspx.cs" Inherits="WidgetManagementApplication.WidgetEdit" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Widget Record Management</title>

    <script type="text/javascript">
        function validateReorderQuantity(sender, args) {
            // Client-side validation function
            var quantityOnHand = parseInt(document.getElementById('<%= txtQuantityOnHand.ClientID %>').value, 10);
            var reorderQuantity = parseInt(document.getElementById('<%= txtReorderQuantity.ClientID %>').value, 10);

            args.IsValid = reorderQuantity < quantityOnHand;
        }
    </script>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />

</head>

<body>
    <form runat="server">

        <div class="container">
            <h1 class="my-4">Widget Record Management</h1>

            <div class="form-group">
                <label for="txtInventoryCode">Inventory Code <span class="text-danger font-weight-bold">*</span></label>
                <asp:TextBox ID="txtInventoryCode" runat="server" AutoCompleteType="None" CssClass="form-control" Style="width: 50%" />
                <asp:RequiredFieldValidator ID="rfvInventoryCode" runat="server" ControlToValidate="txtInventoryCode"
                    ErrorMessage="Enter Inventory Code." Display="Dynamic" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <label for="txtDescription">Description <span class="text-danger font-weight-bold">*</span></label>
                <asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="None" CssClass="form-control" Style="width: 50%" />
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                    ErrorMessage="Enter Description." Display="Dynamic" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <label for="txtQuantityOnHand">Quantity On Hand <span class="text-danger font-weight-bold">*</span></label>
                <asp:TextBox ID="txtQuantityOnHand" runat="server" AutoCompleteType="None" CssClass="form-control" Style="width: 50%" />
                <asp:RequiredFieldValidator ID="rfvQuantityOnHand" runat="server" ControlToValidate="txtQuantityOnHand"
                    ErrorMessage="Enter Quantity On Hand." Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revQuantityOnHand" runat="server" ControlToValidate="txtQuantityOnHand"
                    ErrorMessage="Enter a valid Quantity On Hand (must be a positive integer)." Display="Dynamic" CssClass="text-danger"
                    ValidationExpression="^\d+$" />
            </div>

            <div class="form-group">
                <label for="txtReorderQuantity">Reorder Quantity <span class="text-danger font-weight-bold">*</span></label>
                <asp:TextBox ID="txtReorderQuantity" runat="server" AutoCompleteType="None" CssClass="form-control" Style="width: 50%" />
                <asp:RequiredFieldValidator ID="rfvReorderQuantity" runat="server" ControlToValidate="txtReorderQuantity"
                    ErrorMessage="Enter Reorder Quantity." Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revReorderQuantity" runat="server" ControlToValidate="txtReorderQuantity"
                    ErrorMessage="Enter a valid Reorder Quantity (must be a non-negative integer)." Display="Dynamic" CssClass="text-danger"
                    ValidationExpression="^\d+$" />
                <asp:CustomValidator ID="cvReorderQuantity" runat="server" ControlToValidate="txtReorderQuantity"
                    ClientValidationFunction="validateReorderQuantity" OnServerValidate="cvReorderQuantity_ServerValidate"
                    ErrorMessage="Reorder Quantity must be less than Quantity On Hand." Display="Dynamic" CssClass="text-danger" />
            </div>

            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Enabled="false" CssClass="btn btn-primary" OnClick="btnDelete_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancel_Click" />

            </div>

        </div>

    </form>
</body>
</html>

