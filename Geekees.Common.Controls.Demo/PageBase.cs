using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Geekees.Common.Controls.Demo
{
	public class PageBase : Page
	{
		protected string NorthWindConnectionString
		{
			get
			{
				//bind data from data table
				string path = Server.MapPath( "~/" ); //System.AppDomain.CurrentDomain.BaseDirectory;
				string connStr = string.Format( "Provider=Microsoft.Jet.OLEDB.4.0;Data source={0}db\\NorthWind.mdb", path );

				return connStr;
			}
		}
	}
}
