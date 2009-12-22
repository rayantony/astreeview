using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo13 : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{

		}

		protected override void OnInit( EventArgs e )
		{
			base.OnInit( e );

			this.dscDemoServerControl.OnSelectedNodeChanged += new DemoServerControlNodeSelectedEventHandler( dscDemoServerControl_OnSelectedNodeChanged );
		}

		void dscDemoServerControl_OnSelectedNodeChanged( object src, DemoServerControlNodeSelectedEventArgs e )
		{
			Response.Write( string.Format( "[node text]:{0}<br />[node value]:{1}", e.NodeText, e.NodeValue ) );
		}
	}
}
