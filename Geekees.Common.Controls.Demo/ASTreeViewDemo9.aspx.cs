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
	public partial class ASTreeViewDemo9 : PageBase
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
		}

		#region Nodes Iteration

		protected void btnIterateNodes_Click( object sender, EventArgs e )
		{
			string result = string.Empty;

			IterateNode( this.astvMyTree.RootNode, ref result );

			this.divConsole.InnerHtml += ( string.Format( ">>nodes iteration: <div style='padding-left:20px;'>{0}</div>", result ) );

		}

		private void IterateNode( ASTreeViewNode node, ref string nodeString )
		{
			if( !node.Equals( this.astvMyTree.RootNode ) )
				nodeString += ( "[NODE:]" + node.NodeText + "[PARENT-NODE:]" + node.ParentNode.NodeText + "<br />" );

			foreach( ASTreeViewNode child in node.ChildNodes )
			{
				//recursive call
				IterateNode( child, ref nodeString );
			}
		}

		#endregion

		#region TraverseNodes 

		protected void btnTraverseNodes_Click( object sender, EventArgs e )
		{
			string result = string.Empty;

			ASTreeView.ASTreeNodeHandlerDelegate nodeDelegate = delegate( ASTreeViewNode node )
			{
				if( !node.Equals( this.astvMyTree.RootNode ) )
					result += ( "[NODE:]" + node.NodeText + "[PARENT-NODE:]" + node.ParentNode.NodeText + "<br />" );
			};

			astvMyTree.TraverseTreeNode( this.astvMyTree.RootNode, nodeDelegate );

			this.divConsole.InnerHtml += ( string.Format( ">>nodes traverse: <div style='padding-left:20px;'>{0}</div>", result ) );

		}

		#endregion

		protected void btnClearConsole_Click( object sender, EventArgs e )
		{
			this.divConsole.InnerHtml = string.Empty;
		}
		
		

	}
}
