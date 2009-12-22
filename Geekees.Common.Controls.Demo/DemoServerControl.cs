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

#endregion

namespace Geekees.Common.Controls.Demo
{

	[ToolboxData( "<{0}:DemoServerControl runat=server></{0}:DemoServerControl>" )]
	public class DemoServerControl : WebControl, INamingContainer
	{

		#region event

		#region OnSelectedNodeChanged

		private DemoServerControlNodeSelectedEventHandler _onSelectedNodeChanged;

		/// <summary>
		/// Occurs when [on selected node changed].
		/// </summary>
		public event DemoServerControlNodeSelectedEventHandler OnSelectedNodeChanged
		{
			add { _onSelectedNodeChanged += value; }
			remove { _onSelectedNodeChanged -= value; }
		}

		/// <summary>
		/// Fires the node selected event.
		/// </summary>
		protected virtual void FireNodeSelectedEvent()
		{
			if( _onSelectedNodeChanged != null )
			{
				string nodeText = HttpUtility.UrlDecode( this.hfSelectedNodeText.Value );
				string nodeValue = this.hfSelectedNodeValue.Value;

				_onSelectedNodeChanged( this, new DemoServerControlNodeSelectedEventArgs( nodeText, nodeValue ) );
			}
		}

		#endregion

		#endregion

		#region properties

		protected string NorthWindConnectionString
		{
			get
			{
				//bind data from data table
				string path = this.Page.Server.MapPath( "~/" ); //System.AppDomain.CurrentDomain.BaseDirectory;
				string connStr = string.Format( "Provider=Microsoft.Jet.OLEDB.4.0;Data source={0}db\\NorthWind.mdb", path );

				return connStr;
			}
		}

		#endregion

		#region declaration

		protected ASTreeView astvMyTree;
		protected HiddenField hfSelectedNodeText;
		protected HiddenField hfSelectedNodeValue;
		protected Button btnPostBackTrigger;


		#endregion

		#region constructor

		public DemoServerControl()
		{
			this.astvMyTree = new ASTreeView();
			this.hfSelectedNodeText = new HiddenField();
			this.hfSelectedNodeValue = new HiddenField();
			this.btnPostBackTrigger = new Button();
		}

		#endregion

		#region overrided methods

		protected override void OnInit( EventArgs e )
		{
			base.OnInit( e );

			InitializeControls();
		}

		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			this.Controls.Add( this.astvMyTree );
			this.Controls.Add( this.hfSelectedNodeText );
			this.Controls.Add( this.hfSelectedNodeValue );
			this.Controls.Add( this.btnPostBackTrigger );
		}

		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad( e );

			EnsureChildControls();

			if( !this.Page.IsPostBack )
				this.GenerateTree();
		}


		protected override void Render( HtmlTextWriter writer )
		{
			if( this.Page.Request.QueryString["t1"] == "ajaxLoad" )
			{
				#region ajaxLoad

				string virtualParentKey = this.Page.Request.QueryString["virtualParentKey"];

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

				DataTable dt = OleDbHelper.ExecuteDataset( this.NorthWindConnectionString, CommandType.Text, sql ).Tables[0];

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


				writer.Write( astvMyTree.AjaxResponseStartTag );

				HtmlGenericControl ulRoot = new HtmlGenericControl( "ul" );
				astvMyTree.TreeViewHelper.ConvertTree( ulRoot, root, false );
				foreach( Control c in ulRoot.Controls )
					c.RenderControl( writer );


				writer.Write( astvMyTree.AjaxResponseEndTag );

				#endregion

			}
			else if( this.Page.Request.QueryString["t2"] == "ajaxAdd" )
			{
				#region ajaxAdd

				string addNodeText = this.Page.Request.QueryString["addNodeText"];
				int parentNodeValue = int.Parse( this.Page.Request.QueryString["parentNodeValue"] );

				string maxSql = "select max( productId ) from products";
				int max = (int)OleDbHelper.ExecuteScalar( this.NorthWindConnectionString, CommandType.Text, maxSql );
				int newId = max + 1;

				string sql = string.Format( @"INSERT INTO products( productid, Discontinued, productname, parentid ) values( {0} ,0, '{1}', {2})"
					, max + 1, addNodeText.Replace( "'", "''" ), parentNodeValue );

				int i = OleDbHelper.ExecuteNonQuery( this.NorthWindConnectionString, CommandType.Text, sql );

				ASTreeViewNode root = new ASTreeViewNode( "root" );

				ASTreeViewLinkNode node = new ASTreeViewLinkNode( addNodeText, newId.ToString() );
				node.NavigateUrl = "#";
				node.AdditionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );

				root.AppendChild( node );


				writer.Write( astvMyTree.AjaxResponseStartTag );

				HtmlGenericControl ulRoot = new HtmlGenericControl( "ul" );
				astvMyTree.TreeViewHelper.ConvertTree( ulRoot, root, false );
				foreach( Control c in ulRoot.Controls )
					c.RenderControl( writer );

				writer.Write( astvMyTree.AjaxResponseEndTag );

				#endregion
			}
			else
			{
				base.Render( writer );

				#region render click script

				string clickScript = string.Format( @"
<script type='text/javascript'>
function nodeSelectHandler{0}(elem){{
	document.getElementById('{1}').value = encodeURIComponent(elem.innerHTML);
	document.getElementById('{2}').value = elem.parentNode.getAttribute(""treeNodeValue"");
	document.getElementById('{3}').click();
}}
</script>"
					, this.ClientID /*0*/
					, this.hfSelectedNodeText.ClientID /*1*/
					, this.hfSelectedNodeValue.ClientID /*2*/
					, this.btnPostBackTrigger.ClientID /*3*/);

				writer.Write( clickScript );

				#endregion
			}

		}

		#endregion

		#region private methods

		private void InitializeControls()
		{
			#region astvMyTree

			this.astvMyTree.ID = "astvMyTree";
			this.astvMyTree.EnableManageJSError = false;
			this.astvMyTree.EnableStripAjaxResponse = true;
			this.astvMyTree.BasePath = "~/Javascript/astreeview/";
			this.astvMyTree.DataTableRootNodeValue = "0";
			this.astvMyTree.EnableRoot = false;
			this.astvMyTree.EnableNodeSelection = true;
			this.astvMyTree.EnableCheckbox = true;
			this.astvMyTree.EnableDragDrop = false;
			this.astvMyTree.EnableTreeLines = true;
			this.astvMyTree.EnableNodeIcon = true;
			this.astvMyTree.EnableCustomizedNodeIcon = false;
			this.astvMyTree.EnableContextMenu = true;
			this.astvMyTree.EnableDebugMode = false;
			this.astvMyTree.EnableAjaxOnEditDelete = true;
			this.astvMyTree.AddNodeProvider = this.Page.Request.Url.GetLeftPart( UriPartial.Path );
			this.astvMyTree.AdditionalAddRequestParameters = "{'t2':'ajaxAdd'}";
			this.astvMyTree.EditNodeProvider = "~/ASTreeViewRenameNodeHandler.aspx";
			this.astvMyTree.DeleteNodeProvider = "~/ASTreeViewDeleteNodeProvider.aspx";
			this.astvMyTree.LoadNodesProvider = this.Page.Request.Url.GetLeftPart( UriPartial.Path );
			this.astvMyTree.AdditionalLoadNodesRequestParameters = "{'t1':'ajaxLoad'}";

			this.astvMyTree.OnNodeSelectedScript = string.Format( "nodeSelectHandler{0}(elem);", this.ClientID );

			#endregion

			#region hidden fields and postback trigger

			this.hfSelectedNodeValue.ID = "hfSelectedNodeValue";
			this.hfSelectedNodeText.ID = "hfSelectedNodeText";
			this.btnPostBackTrigger.ID = "btnPostBackTrigger";
			this.btnPostBackTrigger.Click += new EventHandler( btnPostBackTrigger_Click );
			this.btnPostBackTrigger.Style.Add( "display", "none" );

			#endregion
		}

		void btnPostBackTrigger_Click( object sender, EventArgs e )
		{
			FireNodeSelectedEvent();
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

			DataTable dt = OleDbHelper.ExecuteDataset( this.NorthWindConnectionString, CommandType.Text, sql ).Tables[0];

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
				node.AdditionalAttributes.Add( new KeyValuePair<string, string>( "onclick", "return false;" ) );
				//node.AdditionalAttributes = attrs;

				root.AppendChild( node );
			}
		}

		#endregion
	}

	#region delegate

	public delegate void DemoServerControlNodeSelectedEventHandler( object src, DemoServerControlNodeSelectedEventArgs e );

	/// <summary>
	/// ASTreeView Node Selected EventArgs
	/// </summary>
	public sealed class DemoServerControlNodeSelectedEventArgs : EventArgs
	{
		private readonly string _nodeText = string.Empty;
		private readonly string _nodeValue = string.Empty;

		/// <summary>
		/// Initializes a new instance of the <see cref="ASTreeViewNodeSelectedEventArgs"/> class.
		/// </summary>
		/// <param name="nodeText">The node text.</param>
		/// <param name="nodeValue">The node value.</param>
		public DemoServerControlNodeSelectedEventArgs( string nodeText, string nodeValue )
		{
			this._nodeText = nodeText;
			this._nodeValue = nodeValue;
		}

		/// <summary>
		/// Gets the node text.
		/// </summary>
		/// <value>The node text.</value>
		public string NodeText
		{
			get { return this._nodeText; }
		}

		/// <summary>
		/// Gets the node value.
		/// </summary>
		/// <value>The node value.</value>
		public string NodeValue
		{
			get { return this._nodeValue; }
		}
	}


	#endregion
}
