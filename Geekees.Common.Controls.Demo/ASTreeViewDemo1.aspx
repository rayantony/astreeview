<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASTreeViewDemo1.aspx.cs" Inherits="Geekees.Common.Controls.Demo.ASTreeViewDemo1" %>

<%@ Register Assembly="Geekees.Common.Controls" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ASTreeViewDemo1</title>
	<link href="<%=ResolveUrl("~/javascript/astreeview/astreeview.css")%>" type="text/css" rel="stylesheet" />
	<link href="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.css")%>" type="text/css" rel="stylesheet" />
	
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview_packed.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu_packed.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:Button ID="btnToggleDragDrop" CssClass="button" runat="server" Text="EnableDragDrop" OnClick="btnToggleDragDrop_Click" />
		<asp:Button ID="btnToggleTreeLines" CssClass="button" runat="server" Text="EnableTreeLines" OnClick="btnToggleTreeLines_Click" />
		<asp:Button ID="btnToggleNodeIcon" CssClass="button" runat="server" Text="DisableNodeIcon" OnClick="btnToggleNodeIcon_Click" />
		<asp:Button ID="btnToggleCheckbox" CssClass="button" runat="server" Text="EnableCheckbox" OnClick="btnToggleCheckbox_Click" />
		<asp:Button ID="btnToggleDefaultNodeIcon" CssClass="button" runat="server" Text="UseDefaultNodeIcon" OnClick="btnToggleDefaultNodeIcon_Click"  />
		<asp:Button ID="btnExpandAllClient" CssClass="button" runat="server" Text="ExpandAllClient" />
		<asp:Button ID="btnCollapseAllClient" CssClass="button" runat="server" Text="CollapseAllClient" />
		<asp:Button ID="btnToggleExpandCollapseAllClient" CssClass="button" runat="server" Text="ToggleExpandCollapseAllClient" />
		<asp:Button ID="btnToggleContextMenu" CssClass="button" runat="server" Text="EnableContextMenu" OnClick="btnToggleContextMenu_Click"  />
		<asp:Button ID="btnGetTreeViewXML" CssClass="button" runat="server" Text="GetTreeViewXML" OnClick="btnGetTreeViewXML_Click" />
    </div>
    
    <div>
		<ct:ASTreeView ID="astvMyTree" 
				runat="server"
				BasePath="~/Javascript/astreeview/"
				DataTableRootNodeValue="0"
				EnableRoot="false" 
				EnableNodeSelection="false" 
				EnableCheckbox="true" 
				EnableDragDrop="true" 
				EnableTreeLines="true"
				EnableNodeIcon="true"
				EnableCustomizedNodeIcon="true"
				EnableContextMenu="true"
				EnableDebugMode="false"
				EnableContextMenuAdd="false" />
    </div>
    
    <div id="divConsole" runat="server"></div>
    </form>
</body>
</html>
