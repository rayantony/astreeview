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
using System.Xml;

using Geekees.Common.Controls;
using Geekees.Common.Utilities;
using Geekees.Common.Utilities.Xml;

#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo6 : PageBase
	{
		protected void Page_Load( object sender, EventArgs e )
		{
			if( !IsPostBack )
				GenerateTree();
		}

		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );

			this.btnToggleCloseOnNodeSelection.Text = this.astvMyTree2.EnableCloseOnNodeSelection ?
				"DisableCloseOnNodeSelection" : "EnableCloseOnNodeSelection";
		}

		private void GenerateTree()
		{


			XmlDocument doc = new XmlDocument();
			doc.Load( Server.MapPath( "~/ASTreeViewDemo4_Sample_Data.xml" ) );

			ASTreeViewXMLDescriptor descripter = new ASTreeViewXMLDescriptor();

			this.astvMyTree.DataSourceDescriptor = descripter;
			this.astvMyTree.DataSource = doc;
			this.astvMyTree.DataBind();

			this.astvMyTree2.DataSourceDescriptor = descripter;
			this.astvMyTree2.DataSource = doc;
			this.astvMyTree2.DataBind();

		}

		protected void btnGetCheckedNodes_Click( object sender, EventArgs e )
		{
			//if( !Page.IsValid )
			//	return;

			List<ASTreeViewNode> nodes = this.astvMyTree.GetCheckedNodes();
			string toConsole = string.Empty;
			if( nodes.Count == 0 )
				toConsole = "nothing checked";
			else
			{
				foreach( ASTreeViewNode node in nodes )
					toConsole += ( node.NodeText + ", " );

				toConsole = toConsole.Substring( 0, toConsole.Length - 1 );

				//toConsole = node.NodeText;//XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			}
			this.divConsole.InnerHtml += ( string.Format( ">>Checked node: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
		}

		protected void btnGetSelectedNode_Click( object sender, EventArgs e )
		{
			//if( !Page.IsValid )
			//	return;
			ASTreeViewNode node = this.astvMyTree2.GetSelectedNode();
			string toConsole = string.Empty;
			if( node == null )
				toConsole = "nothing selected";
			else
			{
				toConsole += ( node.NodeText );

				toConsole = toConsole.Substring( 0, toConsole.Length - 1 );

				//toConsole = node.NodeText;//XmlHelper.GetFormattedXmlString( this.astvMyTree.GetTreeViewXML(), true );
			}
			this.divConsole.InnerHtml += ( string.Format( ">>Selected node: <pre style='padding-left:20px;'>{0}</pre>", toConsole.ToString() ) );
		}

		protected void btnChangeDropdownText_Click( object sender, EventArgs e )
		{
			if( !Page.IsValid )
				return;

			this.astvMyTree.DropdownText = "Hello world!";

			this.divConsole.InnerHtml += ( ">>Text has been changed.<br />" );

		}

		protected void btnToggleCloseOnNodeSelection_Click( object sender, EventArgs e )
		{
			this.astvMyTree2.EnableCloseOnNodeSelection = !this.astvMyTree2.EnableCloseOnNodeSelection;
		}
	}
}
