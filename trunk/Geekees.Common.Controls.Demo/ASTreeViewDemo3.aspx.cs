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
	public partial class ASTreeViewDemo3 : PageBase
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

		protected override void Render( HtmlTextWriter writer )
		{
			if( Request.QueryString["t"] == "ajaxAdd" )
			{
				string addNodeText = HttpUtility.UrlDecode( Request.QueryString["addNodeText"] );
				int parentNodeValue = int.Parse( HttpUtility.UrlDecode( Request.QueryString["parentNodeValue"] ) );

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
				astvMyTree.TreeViewHelper.ConvertTree( ulRoot, root, false );
				foreach( Control c in ulRoot.Controls )
					c.RenderControl( writer );

				/*
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
				astvMyTree.TreeViewHelper.ConvertTree( ulRoot, root, false );
				foreach( Control c in ulRoot.Controls )
					c.RenderControl( writer );*/
			}
			else
				base.Render( writer );

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
			this.astvMyTree.ContextMenu.MenuItems.Add( new ASContextMenuItem( "Custom Menu 1", "alert('current value:' + " + this.astvMyTree.ContextMenuClientID + ".getSelectedItem().parentNode.getAttribute('treeNodeValue')" + ");return false;", "otherevent" ) );
			this.astvMyTree.ContextMenu.MenuItems.Add( new ASContextMenuItem( "Custom Menu 2", "alert('current text:' + " + this.astvMyTree.ContextMenuClientID + ".getSelectedItem().innerHTML" + ");return false;", "otherevent" ) );
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

			if( this.astvMyTree.RootNode.ChildNodes.Count > 0 )
				this.astvMyTree.RootNode.ChildNodes[0].EnableDeleteContextMenu = false;
		}

		#endregion


		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );

			this.btnToggleAjaxOnEdit.Text = this.astvMyTree.EnableAjaxOnEditDelete ?
				"DisableAjaxOnEditDelete" : "EnableAjaxOnEditDelete";

		}

		protected void btnToggleAjaxOnEdit_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableAjaxOnEditDelete = !this.astvMyTree.EnableAjaxOnEditDelete;
		}
	}
}
