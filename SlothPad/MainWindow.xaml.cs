using Microsoft.Win32;
using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// 
    /// See https://roslyn.codeplex.com/wikipage?title=Syntax%20Visualizer for inspiration
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Reparse();
        }

        string ConvertRichTextBoxContentsToString(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        private void Reparse()
        {
            var sourceCode = ConvertRichTextBoxContentsToString(SourceCode);
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
                    foreach (var property in redNode.GetType().GetProperties())
                    {
                        TreeProperties.Items.Add(new { Name = property.Name, Value = property.GetValue(redNode, null) });
                    }

                    TypeProperty.Text = redNode.GetType().Name;
                    KindProperty.Text = redNode.Kind.ToString();

                    var textRange = SourceCode.Selection;
                    var startPos = SourceCode.GetTextPointerAtOffset(redNode.Span.Start);
                    var endPos = SourceCode.GetTextPointerAtOffset(redNode.Span.Start + redNode.Span.Length);
                    SourceCode.Selection.Select(startPos, endPos);
                }

                if (selectedTreeItem.Tag is SyntaxToken)
                {
                    var token = selectedTreeItem.Tag as SyntaxToken;

                    TypeProperty.Text = token.GetType().Name; // + " " + token.Value.GetType().Name;
                    KindProperty.Text = token.Kind.ToString();

                    foreach (var property in token.GetType().GetProperties())
                    {
                        TreeProperties.Items.Add(new { Name = property.Name, Value = property.GetValue(token, null) });
                    }

                    var textRange = SourceCode.Selection;
                    var startPos = SourceCode.GetTextPointerAtOffset(token.Span.Start);
                    var endPos = SourceCode.GetTextPointerAtOffset(token.Span.Start + token.Span.Length);
                    SourceCode.Selection.Select(startPos, endPos);
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

        private void SourceCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            Reparse();
        }

        private static string CurrentFileName { get; set; }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Filter = "Text Files(*.txt)|*.txt|All(*.*)|*" };
            if (dialog.ShowDialog() == true)
            {
                CurrentFileName = dialog.FileName;
                SlothPadWindow.Title = $"SlothPad - {CurrentFileName}";
                File.WriteAllText(dialog.FileName, ConvertRichTextBoxContentsToString(SourceCode));
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog() { Filter = "Text Files(*.txt)|*.txt|All(*.*)|*" };
            if (dialog.ShowDialog() == true)
            {
                CurrentFileName = dialog.FileName;
                SlothPadWindow.Title = $"SlothPad - {CurrentFileName}";
                var range = new TextRange(SourceCode.Document.ContentStart, SourceCode.Document.ContentEnd);
                range.Text = File.ReadAllText(dialog.FileName);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentFileName))
            {
                File.WriteAllText(CurrentFileName, ConvertRichTextBoxContentsToString(SourceCode));
            }
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
            string line = node.GetType().Name;

            var parentNode = data as TreeViewItem;

            var newTreeViewItem = parentNode;
            if (parentNode.Header.ToString() != "ROOT")
            {
                newTreeViewItem = new TreeViewItem() { Header = $"{node.Kind} {node.Span}", Tag = node, Foreground = Brushes.Blue };
                parentNode.Items.Add(newTreeViewItem);
            }
            else
            {
                parentNode.Header = node.Kind;
                parentNode.Tag = node;
                parentNode.Foreground = Brushes.Blue;
            }

            base.Visit(node, newTreeViewItem);
        }

        protected override void VisitToken(SyntaxToken token, object data)
        {
            string line = token.GetType().Name;
            var newTreeViewItem = new TreeViewItem() { Header = $"{token.Kind} {token.Span}", Tag = token, Foreground = Brushes.Green };
            var parentNode = data as TreeViewItem;
            parentNode.Items.Add(newTreeViewItem);
        }
    }

}
