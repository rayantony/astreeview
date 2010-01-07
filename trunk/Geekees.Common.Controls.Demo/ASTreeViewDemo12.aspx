<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASTreeViewDemo12.aspx.cs" Inherits="Geekees.Common.Controls.Demo.ASTreeViewDemo12" %>
<%@ Register Src="Header.ascx" TagName="Header" TagPrefix="uc1" %>

<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ASTreeViewDemo12</title>
	<link href="<%=ResolveUrl("~/javascript/astreeview/astreeview.css")%>" type="text/css" rel="stylesheet" />
	<link href="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.css")%>" type="text/css" rel="stylesheet" />
	
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview_packed.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu_packed.js")%>" type="text/javascript"></script>
	
	<script type="text/javascript">
		function nodeSelectHandler(elem){
			var val = "selected node:" + elem.parentNode.getAttribute("treeNodeValue");
			document.getElementById("<%=divConsole.ClientID %>").innerHTML 
			+= (">>" + val + "<br />");
			
		}
		
		function nodeCheckHandler(elem){
			var cs = elem.parentNode.getAttribute("checkedState");
			
			var csStr = "";
			switch( parseInt(cs) ){
				case 0:
				csStr = "checked";
				break;
				case 1:
				csStr = "half checked";
				break;
				case 2:
				csStr = "unchecked";
				break;
			}
			var val = csStr +" node:" + elem.parentNode.getAttribute("treeNodeValue");
			document.getElementById("<%=divConsole.ClientID %>").innerHTML 
			+= (">>" + val + "<br />");
			
		}
		
		//parameter must be "elem"
		function dndStartHandler( elem ){
			document.getElementById( "<%=divConsole.ClientID %>" ).innerHTML 
			+= ( ">>drag started. [Node]" + elem.getAttribute("treeNodeValue") 
			+ " [Parent]:" + elem.parentNode.parentNode.getAttribute("treeNodeValue") 
			+ "<br />" );
		}
		
		//parameter must be "elem"
		function dndCompleteHandler( elem ){
			document.getElementById( "<%=divConsole.ClientID %>" ).innerHTML 
			+= ( ">>drag completed. [Node]" + elem.getAttribute("treeNodeValue") 
			+ " [Parent]:" + elem.parentNode.parentNode.getAttribute("treeNodeValue") 
			+ "<br />" );
		}
		
		//parameter must be "elem"
		function editedHandler( elem ){
			document.getElementById( "<%=divConsole.ClientID %>" ).innerHTML 
			+= ( ">>edit completed. [Node]" + elem.getAttribute("treeNodeValue") 
			+ "<br />" );
		}
		
		//parameter must be "val"
		function deletedHandler( val ){
			document.getElementById( "<%=divConsole.ClientID %>" ).innerHTML 
			+= ( ">>delete completed. [Node]" + val
			+ "<br />" );
		}
		
		//elem is the LI element of each node
		var displayNodeFunc = function( elem ){
			document.getElementById( "<%=divConsole.ClientID %>" ).innerHTML 
			+= ( ">>[Node]" + elem.getAttribute("treeNodeValue") 
			+ " [Parent]:" + elem.parentNode.parentNode.getAttribute("treeNodeValue") 
			+ " [CheckState]:" + elem.getAttribute("checkedState") 
			+ "<br />" );	
		}
	</script>
</head>
<body>
    <form id="form1" runat="server">
    
    	<uc1:Header id="Header1" runat="server"></uc1:Header>
		<h2>Client Side Javascript</h2>
    <div>
		<button id="btnClientTraverseNode" class="button" onclick="<%=TraverseNodeScript %>return false;"><asp:Literal ID="lClientTraverseNode" runat="server" Text="ClientTraverseNode"></asp:Literal></button>
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
						EnableRoot="true" 
						EnableNodeSelection="true" 
						EnableCheckbox="true" 
						EnableDragDrop="true" 
						EnableTreeLines="true"
						EnableNodeIcon="true"
						EnableCustomizedNodeIcon="false"
						EnableDebugMode="false"
						EnableContextMenuAdd="false"
						EnableParentNodeExpand="true"
						OnNodeSelectedScript="nodeSelectHandler(elem);"
						OnNodeCheckedScript="nodeCheckHandler(elem);"
						OnNodeDragAndDropCompleteScript="dndCompleteHandler( elem )"
						OnNodeDragAndDropStartScript="dndStartHandler( elem )" 
						OnNodeEditedScript="editedHandler(elem)"
						OnNodeDeletedScript="deletedHandler(val)" />
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
