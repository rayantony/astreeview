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
using System.Xml;

using Geekees.Common.Controls;
using Geekees.Common.Utilities;
using Geekees.Common.Utilities.Xml;

#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo6 : PageBase
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if( !IsPostBack )
			{
				GenerateTree();
				GenerateTree2();
			}
		}

		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );

			this.btnToggleCloseOnNodeSelection.Text = this.astvMyTree2.EnableCloseOnNodeSelection ?
				"DisableCloseOnNodeSelection" : "EnableCloseOnNodeSelection";
		}

		private void GenerateTree2()
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

			ASTreeViewNode root = this.astvMyTree2.RootNode;

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
				node.AdditionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );
				//node.AdditionalAttributes = attrs;

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
					node.AdditionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );

					root.AppendChild( node );
				}



				HtmlGenericControl ulRoot = new HtmlGenericControl( "ul" );
				astvMyTree2.TreeViewHelper.ConvertTree( ulRoot, root, false );
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
				node.AdditionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );

				root.AppendChild( node );

				HtmlGenericControl ulRoot = new HtmlGenericControl( "ul" );
				astvMyTree2.TreeViewHelper.ConvertTree( ulRoot, root, false );
				foreach( Control c in ulRoot.Controls )
					c.RenderControl( writer );
			}
			else
				base.Render( writer );

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

		protected void btnGetCheckedNodes_Click( object sender, EventArgs e )
		{
			//if( !Page.IsValid )
			//	return;

			List<ASTreeViewNode> nodes = this.astvMyTree.GetCheckedNodes();
			string toConsole = string.Empty;
			if( nodes.Count == 0 )
				toConsole = "nothing checked";
			else
			{
				foreach( ASTreeViewNode node in nodes )
					toConsole += ( node.NodeText + ", " );

				toConsole = toConsole.Substring( 0, toConsole.Length - 1 );

				//toConsole = node.NodeText;//XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			}
			this.divConsole.InnerHtml += ( string.Format( ">>Checked node: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
		}

		protected void btnGetSelectedNode_Click( object sender, EventArgs e )
		{
			//if( !Page.IsValid )
			//	return;
			ASTreeViewNode node = this.astvMyTree2.GetSelectedNode();
			string toConsole = string.Empty;
			if( node == null )
				toConsole = "nothing selected";
			else
			{
				toConsole += ( node.NodeText );

				toConsole = toConsole.Substring( 0, toConsole.Length - 1 );

				//toConsole = node.NodeText;//XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			}
			this.divConsole.InnerHtml += ( string.Format( ">>Selected node: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
		}

		protected void btnChangeDropdownText_Click( object sender, EventArgs e )
		{
			if( !Page.IsValid )
				return;

			this.astvMyTree.DropdownText = "Hello world!";

			this.divConsole.InnerHtml += ( ">>Text has been changed.<br />" );

		}

		protected void btnToggleCloseOnNodeSelection_Click( object sender, EventArgs e )
		{
			this.astvMyTree2.EnableCloseOnNodeSelection = !this.astvMyTree2.EnableCloseOnNodeSelection;
		}
	}
}
