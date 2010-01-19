<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASTreeViewDemo5.aspx.cs" Inherits="Geekees.Common.Controls.Demo.ASTreeViewDemo5" %>
<%@ Register Src="Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ASTreeViewDemo5</title>
	<link href="<%=ResolveUrl("~/javascript/astreeview/astreeview.css")%>" type="text/css" rel="stylesheet" />
	<link href="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.css")%>" type="text/css" rel="stylesheet" />
	
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview_packed.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu_packed.js")%>" type="text/javascript"></script>
	
	<script type="text/javascript">
		function nodeSelectHandler(elem){
			
			document.getElementById('<%=this.txtSelectedNode.ClientID%>').value = elem.parentNode.getAttribute("treeNodeValue");
			document.getElementById('<%=this.btnSelectionPBTrigger.ClientID%>').click();
			
		}
		
		function nodeCheckHandler(elem){
			
			document.getElementById('<%=this.txtCheckedNode.ClientID%>').value = elem.parentNode.getAttribute("treeNodeValue");
			document.getElementById('<%=this.btnCheckedPBTrigger.ClientID%>').click();
		}
	</script>
	
</head>
<body>
    <form id="form1" runat="server">
    	<uc1:Header id="Header1" runat="server"></uc1:Header>
		<h2>Ajax nodes loading </h2>
    <div>
		<asp:Button ID="btnGetSelectedNode" CssClass="button" runat="server" Text="GetSelectedNode" OnClick="btnGetSelectedNode_Click" />
		<asp:Checkbox ID="cbIncludeHalfChecked" CssClass="no-border" runat="server" Text="IncludeHalfChecked" /> <asp:Button ID="btnGetCheckedNodes" CssClass="button" runat="server" Text="GetCheckedNodes" OnClick="btnGetCheckedNodes_Click" />
			
    </div>
     <div>
		<table>
			<tr valign="top">
				<td width="400">
					<div style="display:none;">
						<asp:Button ID="btnSelectionPBTrigger" runat="server" OnClick="btnSelectionPBTrigger_Click" />
						<asp:Button ID="btnCheckedPBTrigger" runat="server" OnClick="btnCheckPBTrigger_Click" />
						<asp:TextBox ID="txtSelectedNode" runat="server"></asp:TextBox>
						<asp:TextBox ID="txtCheckedNode" runat="server"></asp:TextBox>
					</div>
				
					<ct:ASTreeView ID="astvMyTree" 
						runat="server"
						BasePath="~/Javascript/astreeview/"
						DataTableRootNodeValue="0"
						EnableRoot="false" 
						EnableNodeSelection="true" 
						EnableCheckbox="true" 
						EnableDragDrop="false" 
						EnableTreeLines="true"
						EnableNodeIcon="true"
						EnableCustomizedNodeIcon="false"
						EnableContextMenu="true"
						EnableDebugMode="false" 
						EnableAjaxOnEditDelete="true"
						AddNodeProvider="~/ASTreeViewDemo5.aspx"
						AdditionalAddRequestParameters="{'t2':'ajaxAdd'}"
						EditNodeProvider="~/ASTreeViewRenameNodeHandler.aspx"
						DeleteNodeProvider="~/ASTreeViewDeleteNodeProvider.aspx"
						LoadNodesProvider="~/ASTreeViewDemo5.aspx"
						AdditionalLoadNodesRequestParameters="{'t1':'ajaxLoad'}"
						OnNodeSelectedScript="nodeSelectHandler(elem);"
						OnNodeCheckedScript="nodeCheckHandler(elem);"/>
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
