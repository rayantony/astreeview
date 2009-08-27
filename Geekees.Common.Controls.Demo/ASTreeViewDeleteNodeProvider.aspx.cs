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
	public partial class ASTreeViewDeleteNodeProvider : PageBase
	{
		ASTreeViewAjaxReturnCode returnCode;
		string errorMessage = string.Empty;

		public string DeleteNodeValues
		{
			get
			{
				return Request.QueryString["deleteNodeValues"];
			}
		}

		protected void Page_Load( object sender, EventArgs e )
		{
			if( string.IsNullOrEmpty( this.DeleteNodeValues ) || string.IsNullOrEmpty( this.DeleteNodeValues ) )
			{
				returnCode = ASTreeViewAjaxReturnCode.ERROR;
				return;
			}

			try
			{
				this.DeleteNodes();
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

		protected void DeleteNodes()
		{
			string sql = string.Format( "DELETE FROM [Products] WHERE [ProductID] in ( {0} )", this.DeleteNodeValues );
			OleDbHelper.ExecuteNonQuery( base.NorthWindConnectionString
				, CommandType.Text
				, sql );
		}
	}
}
