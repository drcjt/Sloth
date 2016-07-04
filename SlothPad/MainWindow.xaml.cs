using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SlothPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sourceCode = SourceCode.Text;
            var tree = SyntaxTree.ParseText(sourceCode);

            ASTTree.Items.Clear();
            var rootItem = new TreeViewItem() { Header = "ROOT" };
            ASTTree.Items.Add(rootItem);
            var writer = new ASTWalker();
            writer.Visit(tree.GetRoot(), rootItem);
            rootItem.ExpandSubtree();
        }

        private void ASTTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null)
            {
                var selectedTreeItem = ((TreeViewItem)e.NewValue);

                TreeProperties.Items.Clear();

                if (selectedTreeItem.Tag is RedNode)
                {
                    var redNode = selectedTreeItem.Tag as RedNode;
                    TreeProperties.Items.Add(new { Name = "Kind", Value = redNode.Kind });
                }

                if (selectedTreeItem.Tag is SyntaxToken)
                {
                    var token = selectedTreeItem.Tag as SyntaxToken;
                    TreeProperties.Items.Add(new { Name = "Kind", Value = token.Kind });
                    TreeProperties.Items.Add(new { Name = "Value", Value = token.Value });
                }
            }
        }

        private void TreeProperties_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 35;
            var col1 = 0.5;
            var col2 = 0.5;

            gridView.Columns[0].Width = workingWidth * col1;
            gridView.Columns[1].Width = workingWidth * col2;
        }
    }

    public class ASTWalker : SyntaxWalker
    {
        public string _treeText;

        private Stack<TreeViewItem> _treeViewStack = new Stack<TreeViewItem>();

        public ASTWalker() : base(SyntaxWalkerDepth.Token)
        {
        }

        public override void Visit(SyntaxNode node, object data)
        {
            string line = node.GetType().ToString();
            var newTreeViewItem = new TreeViewItem() { Header = line, Tag = node };

            var parentNode = data as TreeViewItem;
            parentNode.Items.Add(newTreeViewItem);

            base.Visit(node, newTreeViewItem);
        }

        protected override void VisitToken(SyntaxToken token, object data)
        {
            string line = token.GetType().ToString();
            var newTreeViewItem = new TreeViewItem() { Header = line, Tag = token };
            var parentNode = data as TreeViewItem;
            parentNode.Items.Add(newTreeViewItem);
        }
    }

}
