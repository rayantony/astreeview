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
using System.Text;
using System.Xml;
#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo11 : System.Web.UI.Page
	{
		
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
			{
				GenerateTreeByLevel( 10 );

				ASTreeViewTheme macOS = new ASTreeViewTheme();
				macOS.BasePath = "~/javascript/astreeview/themes/macOS/";
				macOS.CssFile = "macOS.css";
				this.astvMyTree.Theme = macOS;
				this.astvMyTree.EnableTreeLines = false;
				this.astvMyTree.EnableRightToLeftRender = false;
			}
		}

		protected void btnGenerateTree_Click( object sender, EventArgs e )
		{
			this.astvMyTree.RootNode.ChildNodes.Clear();

			int nodesPerLevel = 30;

			try
			{
				nodesPerLevel = int.Parse( this.txtNodesPerLevel.Text );
			}
			catch { }

			GenerateTreeByLevel( nodesPerLevel );
		}

		#endregion

		#region private methods

		/// <summary>
		/// initial controls, bind you events etc. here
		/// </summary>
		private void InitializeComponent()
		{

		}

		private void GenerateTreeByLevel( int nodesPerLevel )
		{



			if( nodesPerLevel > 50 )
			{

				this.divConsole.InnerHtml += ( string.Format( "{0} nodes, too many, isn't it? :-)", nodesPerLevel * nodesPerLevel ) );

				//FlashMessage.SetFlash( string.Format( "{0} nodes, too many, isn't it? :-)", nodesPerLevel * nodesPerLevel ), FlashMessage.FlashMessageType.Error );
				return;
			}

			for( int i = 0; i < nodesPerLevel; i++ )
			{
				ASTreeViewNode l1 = new ASTreeViewNode( string.Format( "Node-L{0}-{1}", 1, i ) );
				this.astvMyTree.RootNode.ChildNodes.Add( l1 );

				for( int j = 0; j < nodesPerLevel; j++ )
				{
					ASTreeViewNode l2 = new ASTreeViewNode( string.Format( "Node-L{0}-{1}-{2}", 2, i, j ) );
					l1.ChildNodes.Add( l2 );

					/*
					for( int k = 0; k < nodesPerLevel; k++ )
					{
						ASTreeViewNode l3 = new ASTreeViewNode( string.Format( "Node-{0}-{1}", 3, k ) );
						l2.ChildNodes.Add( l3 );
					}*/
				}
			}

			//FlashMessage.SetFlash( string.Format( "{0} nodes generated.", nodesPerLevel * nodesPerLevel ) );

			this.divConsole.InnerHtml += ( string.Format( ">>{0} nodes generated.<br />", nodesPerLevel * nodesPerLevel ) );
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
