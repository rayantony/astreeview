#region using
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Geekees.Common.Controls;
using Geekees.Common.Utilities;
using System.Text;
#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo2 : PageBase
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if( !IsPostBack )
				GenerateTree();
		}

		private void GenerateTree()
		{
			//bind data from data table
			//string path = System.AppDomain.CurrentDomain.BaseDirectory;
			//string connStr = string.Format( "Provider=Microsoft.Jet.OLEDB.4.0;Data source={0}db\\NorthWind.mdb", path );

			DataSet ds = OleDbHelper.ExecuteDataset( base.NorthWindConnectionString, CommandType.Text, "select * from [Products]" );

			ASTreeViewDataTableColumnDescriptor descripter = new ASTreeViewDataTableColumnDescriptor( "ProductName"
				, "ProductID"
				, "ParentID" );

			this.astvMyTree.DataSourceDescriptor = descripter;
			this.astvMyTree.DataSource = ds.Tables[0];
			this.astvMyTree.DataBind();

		}

		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );

			this.btnToggleAutoPostback.Text = this.astvMyTree.AutoPostBack ?
				"DisableAutoPostback" : "EnableAutoPostback";

		}

		protected void btnToggleAutoPostback_Click( object sender, EventArgs e )
		{
			this.astvMyTree.AutoPostBack = !this.astvMyTree.AutoPostBack;
		}

		protected void astvMyTree_OnCheckedNodeChanged( object src, ASTreeViewNodeCheckedEventArgs e )
		{
			string toConsole = string.Format( ">>OnCheckedNodeChanged checked: text:{0} value:{1} state:{2}", e.NodeText, e.NodeValue, e.CheckedState.ToString() );
			this.divConsole.InnerHtml += ( toConsole + "<br />" );
		}

		protected void astvMyTree_OnSelectedNodeChanged( object src, ASTreeViewNodeSelectedEventArgs e )
		{
			string toConsole = string.Format( ">>OnSelectedNodeChanged selected: text:{0} value:{1}", e.NodeText, e.NodeValue );
			this.divConsole.InnerHtml += ( toConsole + "<br />" );
		}

		protected void btnGetSelectedNode_Click( object sender, EventArgs e )
		{
			string toConsole = string.Empty;

			ASTreeViewNode selectedNode = astvMyTree.GetSelectedNode();
			if( selectedNode == null )
				toConsole = ">>no node selected.";
			else
				toConsole = string.Format( ">>node selected: text:{0} value:{1}", selectedNode.NodeText, selectedNode.NodeValue );

			this.divConsole.InnerHtml += ( toConsole + "<br />" );
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
