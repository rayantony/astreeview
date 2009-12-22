#region using
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
using System.Xml;

using Geekees.Common.Controls;
using Geekees.Common.Utilities;
using Geekees.Common.Utilities.Xml;

#endregion


namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo14 : System.Web.UI.Page
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

		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );

			this.btnEnableHorizontalLock.Text = this.astvMyTree.EnableHorizontalLock ?
			"DisableHorizontalLock" : "EnableHorizontalLock";

			this.btnEnableContainerDragDrop.Text = this.astvMyTree.EnableContainerDragDrop ?
			"DisableContainerDragDrop" : "EnableContainerDragDrop";

			this.btnEnableFixedDepthDragDrop.Text = this.astvMyTree.EnableFixedDepthDragDrop ?
			"DisableFixedDepthDragDrop" : "EnableFixedDepthDragDrop";


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

		protected void btnEnableHorizontalLock_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableHorizontalLock = !this.astvMyTree.EnableHorizontalLock;
		}

		protected void btnEnableContainerDragDrop_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableContainerDragDrop = !this.astvMyTree.EnableContainerDragDrop;
		}

		protected void btnEnableFixedDepthDragDrop_Click( object sender, EventArgs e )
		{
			this.astvMyTree.EnableFixedDepthDragDrop = !this.astvMyTree.EnableFixedDepthDragDrop;
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
