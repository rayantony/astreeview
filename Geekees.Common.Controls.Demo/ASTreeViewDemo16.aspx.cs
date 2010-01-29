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
using System.Collections.Generic;
using System.Text;

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo16 : PageBase
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

			this.btnToggleNodeIcon.Text = this.astvMyTree.EnableNodeIcon ?
				"DisableNodeIcon" : "EnableNodeIcon";

			this.btnToggleCheckbox.Text = this.astvMyTree.EnableCheckbox ?
				"DisableCheckbox" : "EnableCheckbox";

			this.btnToggleDefaultNodeIcon.Text = this.astvMyTree.EnableCustomizedNodeIcon ?
				"UseDefaultNodeIcon" : "UseCustomizedNodeIcon";

			this.btnToggleThreeStateCheckbox.Text = this.astvMyTree.EnableThreeStateCheckbox ?
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
			this.astvMyTree.ContextMenu.MenuItems.Add( new ASContextMenuItem( "Custom Menu", "alert('current value:' + " + this.astvMyTree.ContextMenuClientID + ".getSelectedItem().parentNode.getAttribute('treeNodeValue')" + ");return false;", "text" ) );
		}

		private void GenerateTree()
		{
			this.astvMyTree.RootNode
				.AppendChild( new ASTreeViewTextNode( "<span isTreeNodeChild='true' style='color:#124AC8;font-weight:bold;'>G</span><span isTreeNodeChild='true' style='color:#EF5344;font-weight:bold;'>o</span><span isTreeNodeChild='true' style='color:#D7A703;font-weight:bold;'>o</span><span isTreeNodeChild='true' style='color:#2E4F90;font-weight:bold;'>g</span><span isTreeNodeChild='true' style='color:#406A3F;font-weight:bold;'>l</span><span isTreeNodeChild='true' style='color:#C31402;font-weight:bold;'>e</span>" )
									.AppendChild( new ASTreeViewLinkNode( "LinkNode 1", "ln1", "http://www.ask.com", "frm", "Goto ask.com", "~/Images/demoIcons/hummer.gif" ) )
									.AppendChild( new ASTreeViewLinkNode( "LinkNode 2", "ln2", "http://www.picasa.com", "frm", "Goto picasa.com", "~/Images/demoIcons/cadillac.gif" ) )
									.AppendChild( new ASTreeViewTextNode( "<span style='color:blue;' isTreeNodeChild='true'>link1:</span><a isTreeNodeChild='true' style='color:blue;font-weight:bold;' href='http://www.google.com' target='frm'>Google</a>&nbsp;&nbsp;<span isTreeNodeChild='true' style='color:#BEAC12;'>link2:</span><a isTreeNodeChild='true' style='color:#BEAC12;font-weight:bold;' href='http://www.google.com/chrome' target='frm'>Chrome</a>" ) )
				)
				.AppendChild( new ASTreeViewTextNode( "<span isTreeNodeChild='true' style='color:#4BBE12;cursor:pointer;' id='sOverMe' onmouseover='document.getElementById(\"sOverMe\").style.color=\"red\";' onmouseout='document.getElementById(\"sOverMe\").style.color=\"#4BBE12\";'>Move your mouse(js demo)</span>" )
									.AppendChild( new ASTreeViewTextNode( "<span style='font-size:18px;' isTreeNodeChild='true'>Big Text</span>" ) )
				)
				.AppendChild( new ASTreeViewTextNode( "<span isTreeNodeChild='true'>Images:</span><img src='../images/demoIcons/ast.gif' /> <img src='../images/demoIcons/hummer.gif' /> <img src='../images/demoIcons/saab.gif' />" )
									.AppendChild( new ASTreeViewTextNode( "<marquee isTreeNodeChild='true' style='width:100px;display:inline-block;'>hello astreeview demo!</marquee>" ) )
				)
				.AppendChild( new ASTreeViewTextNode( "Clock:<span id='sClock'></span><script>window.setInterval('var cur_date = new Date();var cur_hour = cur_date.getHours();var cur_min = cur_date.getMinutes();var cur_sec = cur_date.getSeconds();document.getElementById(\"sClock\").innerHTML = cur_hour + \":\" + cur_min + \":\" +cur_sec;',1000);</script>" )/*.AppendChild( n )*/ )
				.AppendChild( new ASTreeViewTextNode( "<font style='color:blue;font-weight:bold;font-style:italic;' isTreeNodeChild='true'>ASTreeView</font>" ) )
				.AppendChild( new ASTreeViewTextNode( "<div isTreeNodeChild='true' style='width:200px;height:100px;border:1px solid green;margin-top:0px\\9;margin-left:0px\\9;+margin-top:-18px;+margin-left:48px;_margin-top:-18px;_margin-left:48px;'><p>Block Element Line 1</p><p>Block Element Line 2</p></div>" )
				);
		}
		#endregion

		protected void btnToggleDragDrop_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableDragDrop = !this.astvMyTree.EnableDragDrop;
		}

		protected void btnToggleNodeIcon_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableNodeIcon = !this.astvMyTree.EnableNodeIcon;
		}

		protected void btnToggleCheckbox_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableCheckbox = !this.astvMyTree.EnableCheckbox;
		}
		protected void btnToggleThreeStateCheckbox_Click( object sender, EventArgs e )
		{
			this.astvMyTree.ClearNodesCheck();
			this.astvMyTree.EnableThreeStateCheckbox = !this.astvMyTree.EnableThreeStateCheckbox;
		}

		protected void btnToggleDefaultNodeIcon_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableCustomizedNodeIcon = !this.astvMyTree.EnableCustomizedNodeIcon;
		}

		protected void btnGetTreeViewXML_Click( object sender, EventArgs e )
		{
			string toConsole = XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			this.divConsole.InnerHtml += ( string.Format( ">>Treeview XML: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
		}

		protected void btnGetCheckedNodes_Click( object sender, EventArgs e )
		{
			List<ASTreeViewNode> checkedNodes = this.astvMyTree.GetCheckedNodes( cbIncludeHalfChecked.Checked );
			StringBuilder sb = new StringBuilder();

			foreach( ASTreeViewNode node in checkedNodes )
				sb.Append( string.Format( "[text:{0}, value:{1}]<br />", node.NodeText, node.NodeValue ) );

			this.divConsole.InnerHtml += ( string.Format( ">>nodes checked: <div style='padding-left:20px;'>{0}</div>", sb.ToString() ) );
		}
	}
}
