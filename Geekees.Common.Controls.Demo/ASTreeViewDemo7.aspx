<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASTreeViewDemo7.aspx.cs" Inherits="Geekees.Common.Controls.Demo.ASTreeViewDemo7" %>
<%@ Register Src="Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ASTreeViewDemo7</title>
	<link href="<%=ResolveUrl("~/javascript/astreeview/astreeview.css")%>" type="text/css" rel="stylesheet" />
	<link href="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.css")%>" type="text/css" rel="stylesheet" />
	
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview_packed.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu_packed.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    	<uc1:Header id="Header1" runat="server"></uc1:Header>
		<h2>Drag & Drop Between Trees</h2>
    <div>
		<asp:Button ID="btnTreeOneToTreeTwo" CssClass="button" runat="server" Text="TreeOneToTreeTwo" OnClick="btnTreeOneToTreeTwo_Click" />
		<asp:Button ID="btnTreeTwoToTreeOne" CssClass="button" runat="server" Text="TreeTwoToTreeOne" OnClick="btnTreeTwoToTreeOne_Click" />
		<asp:Button ID="btnEnableBothWays" CssClass="button" runat="server" Text="EnableBothWays" OnClick="btnEnableBothWays_Click" />

    </div>
    <div>
		<table>
			<tr>
				<th><h3><asp:Literal ID="aslTreeOne" runat="server" Text="TreeOne"></asp:Literal></h3></th>
				<th><h3><asp:Literal ID="aslTreeTwo" runat="server" Text="TreeTwo"></asp:Literal></h3></th>
			</tr>
			<tr valign="top">
				<td width="300">
					<ct:ASTreeView ID="astvMyTree1" 
						runat="server"
						BasePath="~/Javascript/astreeview/"
						DataTableRootNodeValue="0"
						EnableRoot="true"
						RootNodeText="Accor" 
						EnableNodeSelection="true" 
						EnableCheckbox="false" 
						EnableDragDrop="true" 
						EnableTreeLines="true"
						EnableNodeIcon="true"
						EnableCustomizedNodeIcon="false"
						EnableDebugMode="false"
						EnableContextMenuAdd="false"
						RelatedTrees="astvMyTree2" />
				</td>
				<td>
					<ct:ASTreeView ID="astvMyTree2" 
						runat="server"
						BasePath="~/Javascript/astreeview/"
						DataTableRootNodeValue="0"
						EnableRoot="true" 
						RootNodeText="Accor" 
						EnableNodeSelection="true" 
						EnableCheckbox="false" 
						EnableDragDrop="true" 
						EnableTreeLines="true"
						EnableNodeIcon="true"
						EnableCustomizedNodeIcon="false"
						EnableDebugMode="false"
						EnableContextMenuAdd="false" />
				</td>
			</tr>
		</table>
    </div>
    
    <div id="divConsole" runat="server"></div>
    </form>
</body>
</html>
