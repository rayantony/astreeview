<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASTreeViewDemo4.aspx.cs" Inherits="Geekees.Common.Controls.Demo.ASTreeViewDemo4" %>
<%@ Register Src="Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register Assembly="Geekees.Common.Controls" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ASTreeViewDemo4</title>
	<link href="<%=ResolveUrl("~/javascript/astreeview/astreeview.css")%>" type="text/css" rel="stylesheet" />
	<link href="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.css")%>" type="text/css" rel="stylesheet" />
	
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview_packed.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu_packed.js")%>" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    	<uc1:Header id="Header1" runat="server"></uc1:Header>
		<h2>Xml Datasouce </h2>
    <div>
		<asp:Button ID="btnGetTreeViewXML" CssClass="button" runat="server" Text="GetTreeViewXML" OnClick="btnGetTreeViewXML_Click" />

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
						EnableTreeLines="true"
						EnableNodeIcon="true"
						EnableCustomizedNodeIcon="false"
						EnableDebugMode="false"
						EnableContextMenuAdd="false"
						EnableParentNodeExpand="true" />
					
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
