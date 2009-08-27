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
using System.Text;

using Geekees.Common.Controls;
using Geekees.Common.Utilities;
using Geekees.Common.Utilities.Xml;

#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo5 : PageBase
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if( !IsPostBack )
				GenerateTree();
		}

		private void GenerateTree()
		{
			string para = "= 1";

			string sql = @"SELECT p1.[ProductID] as ProductID, p1.[ProductName] as ProductName, p3.childNodesCount as ChildNodesCount, p1.[ParentID] as ParentID
FROM [Products] p1
INNER JOIN 
(
	SELECT COUNT(*) AS childNodesCount , p2.[ParentID] AS pId 
	FROM [Products] p2
	GROUP BY p2.[ParentID]
) p3
ON p1.[ProductID] = p3.pId
WHERE p1.[ParentID] " + para;

			DataTable dt = OleDbHelper.ExecuteDataset( base.NorthWindConnectionString, CommandType.Text, sql ).Tables[0];

			ASTreeViewNode root = this.astvMyTree.RootNode;

			foreach( DataRow dr in dt.Rows )
			{
				string productName = dr["ProductName"].ToString();
				string productId = dr["ProductID"].ToString();
				string parentId = dr["ParentID"].ToString();
				int childNodesCount = int.Parse( dr["ChildNodesCount"].ToString() );

				ASTreeViewLinkNode node = new ASTreeViewLinkNode( productName, productId );
				node.VirtualNodesCount = childNodesCount;
				node.VirtualParentKey = productId;
				node.IsVirtualNode = childNodesCount > 0;
				node.NavigateUrl = "#";
				//List<KeyValuePair<string, string>> attrs = new List<KeyValuePair<string, string>>();
				node.AddtionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );
				//node.AddtionalAttributes = attrs;

				root.AppendChild( node );
			}
		}

		protected override void Render( HtmlTextWriter writer )
		{
			if( Request.QueryString["t1"] == "ajaxLoad" )
			{
				string virtualParentKey = Request.QueryString["virtualParentKey"];

				string para = string.Empty;// "= 1";
				if( virtualParentKey == null )
					para = " is NULL";
				else
					para = "=" + virtualParentKey;

				string sql = @"SELECT p1.[ProductID] as ProductID, p1.[ProductName] as ProductName, p1.[ParentID] as ParentID, p3.childNodesCount as ChildNodesCount
FROM [Products] p1
LEFT OUTER JOIN 
(
	SELECT COUNT(*) AS childNodesCount , p2.[ParentID] AS pId 
	FROM [Products] p2
	GROUP BY p2.[ParentID]
) p3
ON p1.[ProductID] = p3.pId
WHERE p1.[ParentID] " + para;

				DataTable dt = OleDbHelper.ExecuteDataset( base.NorthWindConnectionString, CommandType.Text, sql ).Tables[0];

				ASTreeViewNode root = new ASTreeViewNode( "root" );

				foreach( DataRow dr in dt.Rows )
				{
					string productName = dr["ProductName"].ToString();
					string productId = dr["ProductID"].ToString();
					string parentId = dr["ParentID"].ToString();
					int childNodesCount = 0;
					if( !string.IsNullOrEmpty( dr["ChildNodesCount"].ToString() ) )
						childNodesCount = int.Parse( dr["ChildNodesCount"].ToString() );

					ASTreeViewLinkNode node = new ASTreeViewLinkNode( productName, productId );
					node.VirtualNodesCount = childNodesCount;
					node.VirtualParentKey = productId;
					node.IsVirtualNode = childNodesCount > 0;
					node.NavigateUrl = "#";
					node.AddtionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );

					root.AppendChild( node );
				}



				HtmlGenericControl ulRoot = new HtmlGenericControl( "ul" );
				astvMyTree.TreeViewHelper.ConvertTree( ulRoot, root, false );
				foreach( Control c in ulRoot.Controls )
					c.RenderControl( writer );
			}
			else if( Request.QueryString["t2"] == "ajaxAdd" )
			{
				string addNodeText = Request.QueryString["addNodeText"];
				int parentNodeValue = int.Parse( Request.QueryString["parentNodeValue"] );

				string maxSql = "select max( productId ) from products";
				int max = (int)OleDbHelper.ExecuteScalar( base.NorthWindConnectionString, CommandType.Text, maxSql );
				int newId = max + 1;

				string sql = string.Format( @"INSERT INTO products( productid, Discontinued, productname, parentid ) values( {0} ,0, '{1}', {2})"
					, max + 1, addNodeText.Replace( "'", "''" ), parentNodeValue );

				int i = OleDbHelper.ExecuteNonQuery( base.NorthWindConnectionString, CommandType.Text, sql );

				ASTreeViewNode root = new ASTreeViewNode( "root" );

				ASTreeViewLinkNode node = new ASTreeViewLinkNode( addNodeText, newId.ToString() );
				node.NavigateUrl = "#";
				node.AddtionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );

				root.AppendChild( node );

				HtmlGenericControl ulRoot = new HtmlGenericControl( "ul" );
				astvMyTree.TreeViewHelper.ConvertTree( ulRoot, root, false );
				foreach( Control c in ulRoot.Controls )
					c.RenderControl( writer );
			}
			else
				base.Render( writer );
			
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
