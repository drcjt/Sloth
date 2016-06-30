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

            var writer = new ASTWalker();
            writer.Visit(tree.GetRoot());

            AST.Text = writer._treeText;
        }
    }

    public class ASTWalker : SyntaxWalker
    {
        public string _treeText;

        static int _tabs = 0;

        public override void Visit(SyntaxNode node)
        {
            _tabs++;

            string line = node.GetType().ToString();
            //Write the line
            _treeText += new String('\t', _tabs) + line + "\r\n";
            base.Visit(node);

            _tabs--;
        }
    }

}
