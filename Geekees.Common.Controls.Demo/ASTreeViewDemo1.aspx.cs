using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Geekees.Common.Utilities.Xml;

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo1 : PageBase
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if( !IsPostBack )
				GenerateTree();
		}

		private void GenerateTree()
		{
			this.astvMyTree.RootNode
								.AppendChild( new ASTreeViewLinkNode( "GM", "GM", "http://www.gm.com", "_self", "Goto GM.com", "~/Images/demoIcons/gm.gif" )
													.AppendChild( new ASTreeViewLinkNode( "Hummer", "Hummer", "http://www.hummer.com", "_self", "Goto Hummer.com", "~/Images/demoIcons/hummer.gif" ) )
													.AppendChild( new ASTreeViewLinkNode( "Cadillac", "Cadillac", "http://www.cadillac.com", "_self", "Goto Cadillac.com", "~/Images/demoIcons/cadillac.gif" ) )
													.AppendChild( new ASTreeViewLinkNode( "SAAB", "SAAB", "http://www.saab.com", "_self", "Goto SAAB.com", "~/Images/demoIcons/saab.gif" ) )
								)
								.AppendChild( new ASTreeViewLinkNode( "Google", "Google", "http://www.google.com", "_self", "Goto Google", "~/Images/demoIcons/google.gif" )
													.AppendChild( new ASTreeViewLinkNode( "Picasa", "Picasa", "http://picasaweb.google.com", "_self", "Goto Picasa", "~/Images/demoIcons/picasa.gif" ) )
								)
								.AppendChild( new ASTreeViewLinkNode( "Microsoft", "Microsoft", "http://www.microsoft.com", "_self", "Goto Microsoft", "~/Images/demoIcons/microsoft.gif" )
													.AppendChild( new ASTreeViewLinkNode( "MSDN", "MSDN", "http://www.msdn.com", "_self", "Goto MSDN", "~/Images/demoIcons/msdn.gif" ) )
								)
								.AppendChild( new ASTreeViewLinkNode( "Amazon", "Amazon", "http://www.amazon.com", "_self", "Goto Amazon", "~/Images/demoIcons/amazon.gif" )
								);
		}

		protected override void OnInit( EventArgs e )
		{
			base.OnInit( e );

			this.btnExpandAllClient.Attributes.Add( "onclick", this.astvMyTree.GetExpandAllScript() + "return false;" );
			this.btnCollapseAllClient.Attributes.Add( "onclick", this.astvMyTree.GetCollapseAllScript() + "return false;" );
			this.btnToggleExpandCollapseAllClient.Attributes.Add( "onclick", this.astvMyTree.GetToggleExpandCollapseAllScript() + "return false;" );
		
			this.astvMyTree.ContextMenu.MenuItems.Add( new ASContextMenuItem( "Custom Menu", "alert('current value:' + " + this.astvMyTree.ContextMenuClientID + ".getSelectedItem().parentNode.getAttribute('treeNodeValue')" + ");return false;", "text" ) );

		}

		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );
			this.btnToggleDragDrop.Text = this.astvMyTree.EnableDragDrop ?
				"DisableDragDrop" : "EnableDragDrop";

			this.btnToggleTreeLines.Text = this.astvMyTree.EnableTreeLines ?
				"DisableTreeLines" : "EnableTreeLines";

			this.btnToggleNodeIcon.Text = this.astvMyTree.EnableNodeIcon ?
				"DisableNodeIcon" : "EnableNodeIcon";

			this.btnToggleCheckbox.Text = this.astvMyTree.EnableCheckbox ?
				"DisableCheckbox" : "EnableCheckbox";

			this.btnToggleDefaultNodeIcon.Text = this.astvMyTree.EnableCustomizedNodeIcon ?
				"UseDefaultNodeIcon" : "UseCustomizedNodeIcon";

			this.btnToggleContextMenu.Text = this.astvMyTree.EnableContextMenu ?
				"DisableContextMenu" : "EnableContextMenu";
		}

		protected void btnToggleDragDrop_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableDragDrop = !this.astvMyTree.EnableDragDrop;
		}

		protected void btnToggleTreeLines_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableTreeLines = !this.astvMyTree.EnableTreeLines;
		}

		protected void btnToggleNodeIcon_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableNodeIcon = !this.astvMyTree.EnableNodeIcon;
		}

		protected void btnToggleCheckbox_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableCheckbox = !this.astvMyTree.EnableCheckbox;

		}

		protected void btnToggleDefaultNodeIcon_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableCustomizedNodeIcon = !this.astvMyTree.EnableCustomizedNodeIcon;
		}

		protected void btnToggleContextMenu_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableContextMenu = !this.astvMyTree.EnableContextMenu;
		}

		protected void btnGetTreeViewXML_Click( object sender, EventArgs e )
		{
			string toConsole = XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			this.divConsole.InnerHtml += ( string.Format( ">>Treeview XML: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
		}
	}
}
