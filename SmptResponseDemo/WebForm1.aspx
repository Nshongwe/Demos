<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SmptResponseDemo.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
        }

        #TextArea1 {
            height: 150px;
            width: 404px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <h4 style="text-align: center; padding-top: 20px">SMTP Response Test</h4>
        <hr />
        <table>
            <tr>
                <td class="auto-style1" colspan="2">
                    <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label1" runat="server" Text="Enter Too Address"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToAddress" runat="server" Width="326px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label2" runat="server" Text="Enter Message Title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttitle" runat="server" Width="398px" Height="16px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label3" runat="server" Text="Enter Message"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtmsg" runat="server" Height="138px" TextMode="MultiLine" Width="399px"></asp:TextBox>
                </td>
            </tr>
            
            
            <tr>
                <td class="auto-style1"></td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Send" OnClick="Button1_Click" Width="158px" />
                    <asp:Button ID="btnSentStatus" runat="server" OnClick="btnSentStatus_Click" Text="Sent Status" Width="194px" />
                </td>
                    
            </tr>
        </table>


    </form>

</body>
</html>
