<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASTreeViewDemoServerControl.aspx.cs" Inherits="Geekees.Common.Controls.Demo.ASTreeViewDemoServerControl" %>
<%@ Register Assembly="Geekees.Common.Controls.Demo" Namespace="Geekees.Common.Controls.Demo" TagPrefix="ct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Test Server Side Control</title>
    
    <link href="<%=ResolveUrl("~/javascript/astreeview/astreeview.css")%>" type="text/css" rel="stylesheet" />
	<link href="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.css")%>" type="text/css" rel="stylesheet" />
	<!--<script src="<%=ResolveUrl("~/javascript/astreeview/ajax.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/astreeview/json.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu.js")%>" type="text/javascript"></script>-->
	<script src="<%=ResolveUrl("~/javascript/astreeview/astreeview_packed.js")%>" type="text/javascript"></script>
	<script src="<%=ResolveUrl("~/javascript/contextmenu/contextmenu_packed.js")%>" type="text/javascript"></script>
	
	
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<ct:DemoServerControl ID="dscDemoServerControl" runat="server" />
    </div>
    </form>
</body>
</html>
