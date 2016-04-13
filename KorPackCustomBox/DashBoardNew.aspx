<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoardNew.aspx.cs" Inherits="DashBoardNew"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Style/<%=ConfigurationManager.AppSettings["ThemeName"] %>/menu_dashboard.css"
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tabsMain">
        <ul>
            <li><a href="#tabs-1">Home</a></li>
        </ul>
        <div id="tabs-1">
            <div style="float: left; width: 30%;">
                <ul class="menustyle">
                    <%  
                        Response.Write(MenuCreateMain());
                    %>
                </ul>
            </div>
            <div style="float: right; width: 65%;">
                <h1>
                    Sample DashBoard</h1>
                <p>
                    What is Lorem Ipsum? Lorem Ipsum is simply dummy text of the printing and typesetting
                    industry. Lorem Ipsum has been the industry's standard dummy text ever since the
                    1500s, when an unknown printer took a galley of type and scrambled it to make a
                    type specimen book. It has survived not only five centuries, but also the leap into
                    electronic typesetting, remaining essentially unchanged. It was popularised in the
                    1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more
                    recently with desktop publishing software like Aldus PageMaker including versions
                    of Lorem Ipsum.
                </p>
                <p>
                    Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots
                    in a piece of classical Latin literature from 45 BC, making it over 2000 years old.
                    Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked
                    up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage,
                    and going through the cites of the word in classical literature, discovered the
                    undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de Finibus
                    Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC.
                    This book is a treatise on the theory of ethics, very popular during the Renaissance.
                    The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", comes from a line
                    in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced
                    below for those interested. Sections 1.10.32 and 1.10.33 from "de Finibus Bonorum
                    et Malorum" by Cicero are also reproduced in their exact original form, accompanied
                    by English versions from the 1914 translation by H. Rackham.
                </p>
               
            </div>
            <div class="cleardiv">
            </div>
        </div>
    </div>
</asp:Content>
