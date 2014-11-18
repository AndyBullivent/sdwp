<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TileDatabaseSettings.aspx.cs" Inherits="Collabco.SussexDowns.WebParts.LAYOUTS.Settings.TileDatabaseSettings" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div style="background-color:lightgray;padding:0.5em;margin-bottom:0.5em">
        <h3>MyStudents, MyAttendance and MyRegisters Tiles by Collabco <br /> Build 1.1.4 <br />Date 06/06/14<br /><br /></h3>
    </div>
    <asp:Label ID="Label1" runat="server" Text="My Students Database Connection String"></asp:Label><br />
    <asp:TextBox ID="StudConnStr" runat="server" Width="400"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="My Attendance Database Connection String"></asp:Label><br />
    <asp:TextBox ID="StatsConnStr" runat="server" Width="400"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="My Registers Database Connection String"></asp:Label><br />
    <asp:TextBox ID="RegConnStr" runat="server" Width="400"></asp:TextBox>
    <br /><br /><br />
    <asp:Button ID="btSave" runat="server" Text="Save" AccessKey="s" OnClick="btSave_Click" ToolTip="Save Your Changes and return to the site." />
    <asp:Button ID="btCancel" runat="server" Text="Cancel" UseSubmitBehavior="False" ToolTip="Cancel out of this page." AccessKey="c" TabIndex="1" OnClick="btCancel_Click" />



</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Bespoke Tile Database Settings 
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Connect Tiles To Databases
</asp:Content>
