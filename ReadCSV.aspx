<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadCSV.aspx.cs" Inherits="CodChallange.ReadCSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
            border-collapse: collapse;
            background-color: #fff;
        }
        table th
        {
            background-color: #ff7f00;
            color: #fff;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border: 1px solid #ccc;
        }
        table, table table td
        {
            border: 0px solid #ccc;
            align-content:center
        }
        .button {
    background-color: #0094ff; /* Blue */
    border: none;
    color: white;
    padding: 15px 32px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 12px;
}
        .button:enabled,
button[enabled]{
  border: 1px solid #999999;
  background-color: #cccccc;
  color: #666666;
}
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <table>
            <tr>
                <th><b>FILEA</b></th>
                <th><b>FILEB</b></th>
                <th><b>FILEMERGE</b></th>
                <th><b>RESULT</b></th>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnimportA" CssClass="button" runat="server" Text="Import" OnClick="ImportCSV1" />
                </td>
                <td>
                <asp:Button ID="btnimportB" CssClass="button" runat="server" Text="Import" OnClick="ImportCSV2" />
                </td>
                <td>
    <asp:Button ID="btnmerge" CssClass="button" runat="server" Text="MergeFile" OnClick="MergeFiles" />
                </td>
                <td>
    <asp:Button ID="btnresult" CssClass="button" runat="server" Text="Result" OnClick="btnresult_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btndownlaodmerge" CssClass="button" Visible="false" runat="server" Text="Export" OnClick="btndownlaodmerge_Click"  />
                    <asp:Button ID="btndownlaod" CssClass="button" Visible="false" runat="server" Text="Export" OnClick="btndownlaod_Click"  />
                    <asp:Label ID="labeldownload" Text="Export Data in CSV" runat="server" Visible="false" />
                </td>
               <%-- <td>
                    <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>
                </td>
                <td>
                    <asp:GridView ID="GridView3" runat="server">
    </asp:GridView>
                </td>
                <td>
                    <asp:GridView ID="GridView4" runat="server">
    </asp:GridView>
                </td>--%>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
                </td>
               <%-- <td>
                    <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>
                </td>
                <td>
                    <asp:GridView ID="GridView3" runat="server">
    </asp:GridView>
                </td>
                <td>
                    <asp:GridView ID="GridView4" runat="server">
    </asp:GridView>
                </td>--%>
            </tr>
        </table>
        
   <%-- <asp:FileUpload ID="FileUpload1"  CssClass="button"  runat="server" />
    <asp:Button ID="btnImport" CssClass="button" runat="server" Text="Import" OnClick="ImportCSV" />--%>
   <%-- <hr />--%>
    <%--<asp:GridView ID="GridView1" runat="server">
    </asp:GridView>--%>
    </form>
</body>
</html>

