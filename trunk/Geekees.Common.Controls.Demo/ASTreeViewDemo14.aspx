<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASTreeViewDemo14.aspx.cs" Inherits="Geekees.Common.Controls.Demo.ASTreeViewDemo14" %>
<%@ Register Src="Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ASTreeViewDemo14</title>
	<link href="<%=ResolveUrl("~/javascript/astreeview/astreeview.css")%>" type="text/css" rel="stylesheet" />
	<link href="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.css")%>" type="text/css" rel="stylesheet" />
	
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview_packed.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu_packed.js")%>" type="text/javascript"></script>
	<style type="text/css">
		.astreeview-tree li, .drag-container li {
			background:url(<%=ResolveUrl("~/Javascript/astreeview/images/astreeview-li-bg.jpg")%>) repeat-x;
			cursor:pointer;
			margin-top:2px;
			margin-bottom:2px;
		}
		
		.astreeview-tree li a:hover, .drag-container li a:hover{
			border-bottom:0px;
		}
		.astreeview-tree ul {
			
		}
		
		.astreeview-tree{
			width:280px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
    
    	<uc1:Header id="Header1" runat="server"></uc1:Header>
		<h2> Advanced Drag and Drop</h2>
    <div>
		<asp:Button ID="btnEnableHorizontalLock" CssClass="button" runat="server" ResKey="EnableHorizontalLock" OnClick="btnEnableHorizontalLock_Click" />
		<asp:Button ID="btnEnableContainerDragDrop" CssClass="button" runat="server" ResKey="EnableContainerDragDrop" OnClick="btnEnableContainerDragDrop_Click" />
		<asp:Button ID="btnEnableFixedDepthDragDrop" CssClass="button" runat="server" ResKey="EnableFixedDepthDragDrop" OnClick="btnEnableFixedDepthDragDrop_Click" />
						
    </div>
    <div>
		<table>
			<tr valign="top">
				<td width="400">
					<ct:ASTreeView ID="astvMyTree" 
						runat="server"
						BasePath="~/Javascript/astreeview/"
						DataTableRootNodeValue="0"
						EnableRoot="false" 
						EnableNodeSelection="true" 
						EnableCheckbox="false" 
						EnableDragDrop="true" 
						EnableTreeLines="false"
						EnableNodeIcon="true"
						EnableCustomizedNodeIcon="false"
						EnableDebugMode="false"
						EnableContextMenuAdd="false"
						EnableParentNodeExpand="true"
						EnableFixedDepthDragDrop="true"
						EnableHorizontalLock="true"
						EnableContainerDragDrop="true" />
				</td>
				<td>
				
					<div id="divConsole" runat="server"></div>
				</td>
			</tr>
		</table>
				
    </div>
    
    </form>
</body>
</html>
