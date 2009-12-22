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
using Geekees.Common.Utilities.Xml;
using System.Text;
using System.Xml;
#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo12 : System.Web.UI.Page
	{
		#region declaration

		#endregion

		#region properties


		public string TraverseNodeScript
		{
			get
			{
				return this.astvMyTree.GetClientTreeObjectId() + ".traverseTreeNode( displayNodeFunc );";
			}
		}


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
				GenerateTree();
		}

		protected void btnGetTreeViewXML_Click( object sender, EventArgs e )
		{
			string toConsole = XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			this.divConsole.InnerHtml += ( string.Format( ">>Treeview XML: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
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

		#endregion
	}
}
