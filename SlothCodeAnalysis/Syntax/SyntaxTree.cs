using SlothCodeAnalysis.Text;
using System;

namespace SlothCodeAnalysis.Syntax
{
    public abstract partial class SyntaxTree
    {
        public static SyntaxTree ParseText(SourceText text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            using (var lexer = new InternalSyntax.Lexer(text))
            {
                using (var parser = new InternalSyntax.LanguageParser(lexer))
                {
                    var compilationUnit = (CompilationUnitSyntax)parser.ParseCompilationUnit().CreateRedNode();
                    var tree = new ParsedSyntaxTree(text, compilationUnit);
                    return tree;
                }
            }
        }

        public static SyntaxTree ParseText(string text)
        {
            return ParseText(SourceText.From(text));
        }

        /// <summary>
        /// Gets the root node of the syntax tree.
        /// </summary>
        public abstract SyntaxNode GetRoot();

        internal static SyntaxTree CreateWithoutClone(SyntaxNode root)
        {
            return new ParsedSyntaxTree(null, root);
        }
    }
}
