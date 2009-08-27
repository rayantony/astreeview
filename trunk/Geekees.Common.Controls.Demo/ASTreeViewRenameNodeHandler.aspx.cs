#region using
using System;
using System.Data;
using System.Data.OleDb;
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
#endregion
namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewRenameNodeHandler : PageBase
	{
		ASTreeViewAjaxReturnCode returnCode;
		string errorMessage = string.Empty;

		public string NodeValue
		{
			get
			{
				return Request.QueryString["nodeValue"];
			}
		}

		public string NewNodeText
		{
			get
			{
				return Request.QueryString["newNodeText"];
			}
		}


		protected void Page_Load( object sender, EventArgs e )
		{
			if( string.IsNullOrEmpty( this.NodeValue ) || string.IsNullOrEmpty( this.NewNodeText ) )
			{
				returnCode = ASTreeViewAjaxReturnCode.ERROR;
				return;
			}

			try
			{
				this.RenameNode();
			}
			catch( Exception ex )
			{
				this.returnCode = ASTreeViewAjaxReturnCode.ERROR;
				this.errorMessage = ex.Message;
			}
		}

		protected override void Render( HtmlTextWriter writer )
		{
			if( this.returnCode == ASTreeViewAjaxReturnCode.OK )
				writer.Write( (int)this.returnCode );
			else
				writer.Write( this.errorMessage );
		}

		protected void RenameNode()
		{
			string sql = "UPDATE [Products] SET [ProductName]=@NewName WHERE [ProductID]=@PId";
			OleDbHelper.ExecuteNonQuery( base.NorthWindConnectionString
				, CommandType.Text
				, sql
				, new OleDbParameter( "@NewName", this.NewNodeText )
				, new OleDbParameter( "@PId", this.NodeValue ) );
			//DataSet ds = OleDbHelper.ExecuteDataset( base.NorthWindConnectionString, CommandType.Text, "select * from [Products]" );

		}
	}
}
