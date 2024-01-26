<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WidgetRecordList.aspx.cs" Inherits="WidgetManagementApplication.WidgetRecordList" %>

<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html lang="en">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Widget Record List</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function CancelClick() {
            return false;
        }
    </script>
</head>

<body>

    <form runat="server">

        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div class="container">

            <h1 class="my-4">Widget Record List</h1>

            <asp:GridView ID="GridView1" DataKeyNames="WidgetID" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="5"
                PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="GridView1_PageIndexChanging"
                OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" CssClass="GridView1" />

                <Columns>
                    <asp:BoundField DataField="WidgetID" HeaderText="Widget ID" ReadOnly="True" SortExpression="WidgetID" />
                    <asp:BoundField DataField="InventoryCode" HeaderText="Inventory Code" SortExpression="Inventoryode" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="QuantityOnHand" HeaderText="Quantity OnHand" SortExpression="QuantityOnHand" />
                    <asp:BoundField DataField="ReorderQuantity" HeaderText="Reorder Quantity" SortExpression="ReorderQuantity" />

                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="Edit" ToolTip="Edit"
                                ImageUrl="../images/icons/pencil.png" CausesValidation="false" />
                            <asp:ImageButton ID="imgDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                ImageUrl="../images/icons/cross-circle.png" />
                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText='<%# "Are you sure you want to delete Widget ID " + Eval("WidgetID") + "?" %>'
                                TargetControlID="imgDelete" OnClientCancel="CancelClick"></cc1:ConfirmButtonExtender>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <span>Action</span>
                        </HeaderTemplate>
                    </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <EmptyDataTemplate>
                    <span class="text-danger font-weight-bold">No Records Found......</span>
                </EmptyDataTemplate>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

            <div class="mt-3">
                <asp:Button ID="btnAdd" runat="server" Text="Add Widget Record" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
            </div>
        </div>
    </form>
</body>

</html>


