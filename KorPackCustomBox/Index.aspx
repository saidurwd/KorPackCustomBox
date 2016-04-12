<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background-color: white">
        <tr>
            <td align="center" rowspan="1" style="font-weight: bold; font-size: xx-large; width: 100%; font-family: Verdana;"></td>
        </tr>
        <tr>
            <td rowspan="3" style="width: 100%; font-weight: bold; font-size: xx-large; font-family: Verdana;" align="center">
                <asp:HiddenField ID="hdnHome" runat="server" Value="Korpack" />
                <%--<asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/SkinFile/images/index.png" /></td>--%>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
    </table>
</asp:Content>

