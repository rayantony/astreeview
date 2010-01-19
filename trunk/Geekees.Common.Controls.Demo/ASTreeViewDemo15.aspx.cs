#region using
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
using System.Xml;

using Geekees.Common.Controls;
using Geekees.Common.Utilities;
using Geekees.Common.Utilities.Xml;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo15 : System.Web.UI.Page
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
				GenerateTree();
		}

		protected void btnToggleCheckbox_Click( object sender, EventArgs e )
		{
			this.astvMyTree.ClearNodesCheck();
			this.astvMyTree.EnableCheckbox = !this.astvMyTree.EnableCheckbox;
		}

		protected void btnEnableThreeStateCheckbox_Click( object sender, EventArgs e )
		{
			this.astvMyTree.ClearNodesCheck();
			this.astvMyTree.EnableThreeStateCheckbox = !this.astvMyTree.EnableThreeStateCheckbox;
		}

		protected void btnEnableLeafOnlyCheckbox_Click( object sender, EventArgs e )
		{
			this.astvMyTree.ClearNodesCheck();
			this.astvMyTree.EnableLeafOnlyCheckbox = !this.astvMyTree.EnableLeafOnlyCheckbox;

			if( !this.astvMyTree.EnableLeafOnlyCheckbox )
			{
				ASTreeView.ASTreeNodeHandlerDelegate nodeDelegate = delegate( ASTreeViewNode node )
				{
					node.EnableCheckbox = true;
				};

				astvMyTree.TraverseTreeNode( astvMyTree.RootNode, nodeDelegate );
			}
		}

		protected void btnClearCheck_Click( object sender, EventArgs e )
		{
			this.astvMyTree.ClearNodesCheck();
		}

		protected void btnGetCheckedNodes_Click( object sender, EventArgs e )
		{
			List<ASTreeViewNode> checkedNodes = this.astvMyTree.GetCheckedNodes( cbIncludeHalfChecked.Checked );
			StringBuilder sb = new StringBuilder();

			foreach( ASTreeViewNode node in checkedNodes )
				sb.Append( string.Format( "[text:{0}, value:{1}]<br />", node.NodeText, node.NodeValue ) );

			this.divConsole.InnerHtml += ( string.Format( ">>nodes checked: <div style='padding-left:20px;'>{0}</div>", sb.ToString() ) );
		}

		protected void btnMakeCheckedNodesUnselectable_Click( object sender, EventArgs e )
		{
			List<ASTreeViewNode> checkedNodes = this.astvMyTree.GetCheckedNodes();
			foreach( ASTreeViewNode node in checkedNodes )
				node.EnableSelection = false;
		}

		protected void btnMakeCheckedNodesSelectable_Click( object sender, EventArgs e )
		{
			List<ASTreeViewNode> checkedNodes = this.astvMyTree.GetCheckedNodes();
			foreach( ASTreeViewNode node in checkedNodes )
				node.EnableSelection = true;
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

		}

		private void GenerateTree()
		{


			XmlDocument doc = new XmlDocument();
			doc.Load( Server.MapPath( "~/ASTreeViewDemo4_Sample_Data.xml" ) );

			ASTreeViewXMLDescriptor descripter = new ASTreeViewXMLDescriptor();

			this.astvMyTree.DataSourceDescriptor = descripter;
			this.astvMyTree.DataSource = doc;
			this.astvMyTree.DataBind();

		}

		#endregion
	}
}
