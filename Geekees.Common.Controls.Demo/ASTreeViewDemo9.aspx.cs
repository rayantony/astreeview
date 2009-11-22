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
#endregion

namespace Geekees.Common.Controls.Demo
{
	public partial class ASTreeViewDemo9 : PageBase
	{
		#region declaration
		private static string ORIGINAL_TREE_NODES_SESSION_KEY = "ORIGINAL_TREE_NODES_SESSION_KEY";

		public Hashtable OriginalTreeNodes
		{
			get
			{
				object o = Session[ORIGINAL_TREE_NODES_SESSION_KEY];
				return o == null ? new Hashtable() : (Hashtable)o;
			}
			set
			{
				Session[ORIGINAL_TREE_NODES_SESSION_KEY] = value;
			}
		}


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

			SaveOriginalTreeNodes();
		}
		#endregion

		protected override void OnPreRender( EventArgs e )
		{
			base.OnPreRender( e );

		}

		#region Nodes Iteration

		protected void btnIterateNodes_Click( object sender, EventArgs e )
		{
			string result = string.Empty;

			IterateNode( this.astvMyTree.RootNode, ref result );

			this.divConsole.InnerHtml += ( string.Format( ">>nodes iteration: <div style='padding-left:20px;'>{0}</div>", result ) );

		}

		private void IterateNode( ASTreeViewNode node, ref string nodeString )
		{
			if( !node.Equals( this.astvMyTree.RootNode ) )
				nodeString += ( "[NODE:]" + node.NodeText + "[PARENT-NODE:]" + node.ParentNode.NodeText + "<br />" );

			foreach( ASTreeViewNode child in node.ChildNodes )
			{
				//recursive call
				IterateNode( child, ref nodeString );
			}
		}

		#endregion

		#region TraverseNodes

		protected void btnTraverseNodes_Click( object sender, EventArgs e )
		{
			string result = string.Empty;

			ASTreeView.ASTreeNodeHandlerDelegate nodeDelegate = delegate( ASTreeViewNode node )
			{
				if( !node.Equals( this.astvMyTree.RootNode ) )
					result += ( "[NODE:]" + node.NodeText + "[PARENT-NODE:]" + node.ParentNode.NodeText + "<br />" );
			};

			astvMyTree.TraverseTreeNode( this.astvMyTree.RootNode, nodeDelegate );

			this.divConsole.InnerHtml += ( string.Format( ">>nodes traverse: <div style='padding-left:20px;'>{0}</div>", result ) );

		}

		#endregion

		#region Resolve ASTreeView Nodes' Modification

		protected void btnResolveNodesModification_Click( object sender, EventArgs e )
		{
			ResolveTreeNodesModification();
		}

		/// <summary>
		/// save the original tree nodes to session for comparison later
		/// </summary>
		private void SaveOriginalTreeNodes()
		{
			//create a hashtable to store nodes
			Hashtable ht = new Hashtable();

			ASTreeView.ASTreeNodeHandlerDelegate nodeDelegate = delegate( ASTreeViewNode node )
			{
				//skip RootNode
				if( node.Equals( this.astvMyTree.RootNode ) )
					return;

				//assume NodeValue is unique
				ht.Add( node.NodeValue, node );
			};

			astvMyTree.TraverseTreeNode( this.astvMyTree.RootNode, nodeDelegate );

			//set the session
			this.OriginalTreeNodes = ht;
		}

		/// <summary>
		/// compare to the OriginalTreeNodes to find the nodes which have been changed
		/// </summary>
		private void ResolveTreeNodesModification()
		{
			//the result string
			StringBuilder sb = new StringBuilder();

			#region traverse all the nodes, detect new nodes, modified nodes
			//traverse all the nodes, detect new nodes, modified nodes
			ASTreeView.ASTreeNodeHandlerDelegate nodeDelegate = delegate( ASTreeViewNode node )
			{
				//skip RootNode
				if( node.Equals( this.astvMyTree.RootNode ) )
					return;

				object obj = this.OriginalTreeNodes[node.NodeValue];
				//if node is a new node, it can't be found in the OriginalNodes
				if( obj == null )
					sb.Append( string.Format( "[NEW] Node: {0} <br />", node.NodeText ) );
				else
				{
					ASTreeViewNode originalNode = (ASTreeViewNode)obj;

					//if the node has been changed
					//compare, here I just demo NodeText and ParentNodeId
					if( node.NodeText != originalNode.NodeText )
						sb.Append( string.Format( "[TEXT CHANGED]NodeText changed! Original Text:{0}, New Text: {1} <br />", originalNode.NodeText, node.NodeText ) );

					if( node.ParentNode.NodeValue != originalNode.ParentNode.NodeValue )
						sb.Append( string.Format( "[PARENT CHANGED]Node's parent changed! Original Parent: {0}, New Parent: {1} <br />", originalNode.ParentNode.NodeText, node.ParentNode.NodeText ) );

					int oldPos = originalNode.ParentNode.IndexOf( originalNode );
					int newPos = node.ParentNode.ChildNodes.IndexOf( node );
					if( newPos != oldPos )
						sb.Append( string.Format( "[POSITION CHANGED]Node's position changed! Original Position: {0}, New Position: {1} <br />", oldPos, newPos ) );

				}
			};

			//do traverse
			astvMyTree.TraverseTreeNode( this.astvMyTree.RootNode, nodeDelegate );

			#endregion

			#region find deleted nodes

			//find deleted nodes
			List<string> originalNodesKeys = new List<string>();
			foreach( string key in this.OriginalTreeNodes.Keys )
				originalNodesKeys.Add( key );

			//traverse all the nodes, detect new nodes, modified nodes
			ASTreeView.ASTreeNodeHandlerDelegate nodeDeleteDelegate = delegate( ASTreeViewNode node )
			{
				//skip RootNode
				if( node.Equals( this.astvMyTree.RootNode ) )
					return;

				if( originalNodesKeys.Contains( node.NodeValue ) )
					originalNodesKeys.Remove( node.NodeValue );
			};

			//do traverse
			astvMyTree.TraverseTreeNode( this.astvMyTree.RootNode, nodeDeleteDelegate );

			//keys remain in the originalNodesKeys are the deleted nodes
			foreach( string deletedNodeKey in originalNodesKeys )
			{
				sb.Append( string.Format( "[NODE DELETED]Original Node: {0}<br />", deletedNodeKey ) );

			}

			#endregion



			this.divConsole.InnerHtml += ( string.Format( ">>The following nodes have been changed: <div style='padding-left:20px;'>{0}</div>", sb.ToString() ) );


		}

		#endregion

		protected void btnClearConsole_Click( object sender, EventArgs e )
		{
			this.divConsole.InnerHtml = string.Empty;
		}
	}
}
