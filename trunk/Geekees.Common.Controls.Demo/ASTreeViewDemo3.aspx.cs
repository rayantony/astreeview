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
		protected override void OnInit( EventArgs e )
		{
			base.OnInit( e );
			this.astvMyTree.ContextMenu.MenuItems.Add( new ASContextMenuItem( "Custom Menu 1", "alert('current value:' + " + this.astvMyTree.ContextMenuClientID + ".getSelectedItem().parentNode.getAttribute('treeNodeValue')" + ");return false;", "otherevent" ) );
			this.astvMyTree.ContextMenu.MenuItems.Add( new ASContextMenuItem( "Custom Menu 2", "alert('current text:' + " + this.astvMyTree.ContextMenuClientID + ".getSelectedItem().innerHTML" + ");return false;", "otherevent" ) );
		}

		protected void Page_Load( object sender, EventArgs e )
		{
			if( !IsPostBack )
				GenerateTree();
		}

		private void GenerateTree()
		{
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

		protected override void Render( HtmlTextWriter writer )
		{
			if( Request.QueryString["t"] == "ajaxAdd" )
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
	}
}
