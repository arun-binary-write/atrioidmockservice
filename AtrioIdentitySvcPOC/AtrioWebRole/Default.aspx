<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AtrioWebRole._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                Welcome to ATRIO Admin Portal
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Below buttons are displayed based on permissions:</h3>
    <div>

        <asp:Button ID="Button1" runat="server" Text="Search" />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Add Item" />

    </div>
    
</asp:Content>
