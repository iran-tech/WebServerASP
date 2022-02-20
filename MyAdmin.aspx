<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAdmin.aspx.cs" Inherits="IranTech.MyAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="direction: ltr">
            SQL My ADMIN<br />
            Copyright All rights recieved<br />
            <br />
            -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            Create table<br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server">table name</asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" Height="260px" TextMode="MultiLine" Width="641px">columns</asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; each column in one line, write column type after column name whith ane space like (ID INT)<br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Create" />
            <br />
            <br />
            Result:<br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            <br />
            <br />
            ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            Drop or Delete<br />
            <br />
            <asp:TextBox ID="TextBox3" runat="server">table name</asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Drop" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" Text="truncate" />
            <br />
            <br />
            Result:<br />
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br />
            --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            <br />
            <br />
            --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            Insert<br />
            <br />
            <asp:TextBox ID="TextBox4" runat="server">table name</asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox5" runat="server" Height="192px" TextMode="MultiLine" Width="600px">data</asp:TextBox>
&nbsp;write column_name&nbsp; value in each line<br />
            <br />
            <asp:Button ID="Button4" runat="server" Text="Insert" />
            <br />
            Result:<br />
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <br />
            -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            <br />
            <br />
            -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            Delete<br />
            <br />
            <asp:TextBox ID="TextBox8" runat="server">table name</asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox9" runat="server">filter</asp:TextBox>
&nbsp; column_name = value<br />
            <br />
            <asp:Button ID="Button6" runat="server" Text="Delete" />
            <br />
            Result:<br />
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
            <br />
            ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            <br />
            <br />
            ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            Update<br />
            <br />
            <asp:TextBox ID="TextBox10" runat="server">table name</asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox11" runat="server">filter</asp:TextBox>
&nbsp;&nbsp;&nbsp; column_name = value<br />
            <asp:TextBox ID="TextBox12" runat="server">data</asp:TextBox>
&nbsp;&nbsp;&nbsp; column_name value<br />
            <br />
            <asp:Button ID="Button7" runat="server" Text="Update" />
            <br />
            Result:<br />
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            <br />
            <br />
            -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------<br />
            Select<br />
            <br />
            <asp:TextBox ID="TextBox6" runat="server">table name</asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox7" runat="server">columns</asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; write columns like column1,column2,.......&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; or empty for all<br />
            <br />
            <asp:Button ID="Button5" runat="server" Text="Select" />
            <br />
            Result:<br />
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
            </asp:GridView>
            <br />
            </div>
            </form>
            </body>
            </html>
