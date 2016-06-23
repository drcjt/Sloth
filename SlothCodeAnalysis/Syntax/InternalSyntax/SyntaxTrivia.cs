namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal class SyntaxTrivia : SyntaxNode
    {
        internal readonly string Text;

        internal SyntaxTrivia(SyntaxKind kind, string text) : base(kind, text.Length)
        {
            Text = text;
        }
    }
}
