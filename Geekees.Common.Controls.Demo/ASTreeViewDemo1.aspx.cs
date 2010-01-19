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
		#region declaration

		#endregion

		#region properties

		#endregion

		#region overrided methods
		/// <summary>
		/// OnInit
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit( EventArgs e )
		{
			InitializeComponent();
			base.OnInit( e );
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

			this.btnToggleMultiLineEdit.Text = this.astvMyTree.EnableMultiLineEdit ?
				"DisableMultiLineEdit" : "EnableMultiLineEdit";

			this.btnToggleEscapeInput.Text = this.astvMyTree.EnableEscapeInput ?
				"DisableEscapeInput" : "EnableEscapeInput";

			this.btnToggleEnableThreeStateCheckbox.Text = this.astvMyTree.EnableThreeStateCheckbox ?
				"DisableThreeStateCheckbox" : "EnableThreeStateCheckbox";
		}

		protected override void Render( HtmlTextWriter writer )
		{
			base.Render( writer );

			string s = "";
		}

		#endregion

		#region event handlers (Page_Load etc...)

		/// <summary>
		/// Page load logic
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load( object sender, EventArgs e )
		{
			if( !IsPostBack )
			{
				GenerateTree();
			}
		}

		#endregion

		#region public methods

		#endregion

		#region protected methods

		#endregion

		#region private methods

		/// <summary>
		/// initial controls, bind you events etc. here
		/// </summary>
		private void InitializeComponent()
		{
			this.btnExpandAllClient.Attributes.Add( "onclick", this.astvMyTree.GetExpandAllScript() + "return false;" );
			this.btnCollapseAllClient.Attributes.Add( "onclick", this.astvMyTree.GetCollapseAllScript() + "return false;" );
			this.btnToggleExpandCollapseAllClient.Attributes.Add( "onclick", this.astvMyTree.GetToggleExpandCollapseAllScript() + "return false;" );

			this.astvMyTree.ContextMenu.MenuItems.Add( new ASContextMenuItem( "Custom Menu", "alert('current value:' + " + this.astvMyTree.ContextMenuClientID + ".getSelectedItem().parentNode.getAttribute('treeNodeValue')" + ");return false;", "text" ) );

		}

		private void GenerateTree()
		{

			ASTreeViewLinkNode n = new ASTreeViewLinkNode( "Picasa", "Picasa", "http://picasaweb.google.com", "frm", "Goto Picasa", "~/Images/demoIcons/picasa.gif" );
			n.NodeText = "The node cannot have children.";
			n.EnableChildren = false;
			n.EnableEditContextMenu = false;

			//n.AdditionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "alert(1);return false;" ) );
			//n.AdditionalAttributes.Add( new KeyValuePair<string, string>( "disableChildren1", "true" ) );

			this.astvMyTree.RootNode
				//.AppendChild( new ASTreeViewLinkNode( "Accor", "Accor", "http://www.accor.com", "frm", "Goto Accor", "~/Images/demoIcons/accor.gif" )
				//                    .AppendChild( new ASTreeViewLinkNode( "Accor Services", "Accor Services", "http://www.accorservices.com", "frm", "Goto Accor Services", "~/Images/demoIcons/accorservices.gif" ) )
				//                    .AppendChild( new ASTreeViewLinkNode( "Accor Hospitality", "Accor Hospitality", "http://www.accorhotels.com", "frm", "Goto Accor Hospitality", "~/Images/demoIcons/accorhospitality.gif" ) )
				//)
								.AppendChild( new ASTreeViewLinkNode( "GM", "GM", "http://www.gm.com", "frm", "Goto GM.com", "~/Images/demoIcons/gm.gif" )
													.AppendChild( new ASTreeViewLinkNode( "Hummer", "Hummer", "http://www.hummer.com", "frm", "Goto Hummer.com", "~/Images/demoIcons/hummer.gif" ) )
													.AppendChild( new ASTreeViewLinkNode( "Cadillac", "Cadillac", "http://www.cadillac.com", "frm", "Goto Cadillac.com", "~/Images/demoIcons/cadillac.gif" ) )
													.AppendChild( new ASTreeViewLinkNode( "SAAB", "SAAB", "http://www.saab.com", "frm", "Goto SAAB.com", "~/Images/demoIcons/saab.gif" ) )
								)
								.AppendChild( new ASTreeViewLinkNode( "Google", "Google Site", "http://www.google.com", "frm", "Goto Google", "~/Images/demoIcons/google.gif" )
													.AppendChild( new ASTreeViewLinkNode( "Picasa", "Picasa", "http://picasaweb.google.com", "frm", "Goto Picasa", "~/Images/demoIcons/picasa.gif" ) )
								)
								.AppendChild( new ASTreeViewLinkNode( "Microsoft", "Microsoft", "http://www.microsoft.com", "frm", "Goto Microsoft", "~/Images/demoIcons/microsoft.gif" )
													.AppendChild( new ASTreeViewLinkNode( "MSDN", "MSDN", "http://www.msdn.com", "frm", "Goto MSDN", "~/Images/demoIcons/msdn.gif" ) )
								)
								.AppendChild( new ASTreeViewLinkNode( "Amazon", "Amazon", "http://www.amazon.com", "frm", "Goto Amazon", "~/Images/demoIcons/amazon.gif" ).AppendChild( n ) )
								.AppendChild( new ASTreeViewLinkNode( "<font style='color:blue;font-weight:bold;font-style:italic;' isTreeNodeChild='true'>ASTreeView</font>", "Best Free TreeView Control for ASP.Net", "http://www.astreeview.com", "frm", "Html as TreeNode Text", "~/Images/demoIcons/ast.gif" )
								);



			/*
			string path = System.AppDomain.CurrentDomain.BaseDirectory;
			string connStr = string.Format( "Provider=Microsoft.Jet.OLEDB.4.0;Data source={0}db\\NorthWind.mdb", path );

			DataSet ds = OleDbHelper.ExecuteDataset( connStr, CommandType.Text, "select * from [Products]" );

			ASTreeViewDataTableColumnDescriptor descripter = new ASTreeViewDataTableColumnDescriptor( "ProductName"
				, "ProductID"
				, "ParentID" );
			//descripter.AddSingleQuotationOnQuery = false;
			this.astvMyTree.DataTableColumnDescriptor = descripter;
			this.astvMyTree.DataSource = ds.Tables[0];
			this.astvMyTree.DataBind();
			*/
		}
		#endregion

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

		protected void btnEnableThreeStateCheckbox_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableThreeStateCheckbox = !this.astvMyTree.EnableThreeStateCheckbox;
		}

		protected void btnToggleDefaultNodeIcon_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableCustomizedNodeIcon = !this.astvMyTree.EnableCustomizedNodeIcon;
		}

		protected void btnToggleContextMenu_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableContextMenu = !this.astvMyTree.EnableContextMenu;
		}

		protected void btnToggleMultiLineEdit_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableMultiLineEdit = !this.astvMyTree.EnableMultiLineEdit;
		}

		protected void btnToggleEscapeInput_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableEscapeInput = !this.astvMyTree.EnableEscapeInput;
		}

		protected void btnGetTreeViewXML_Click( object sender, EventArgs e )
		{
			string toConsole = XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			this.divConsole.InnerHtml += ( string.Format( ">>Treeview XML: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
		}
	}
}
