namespace SlothCodeAnalysis.Syntax
{
    public abstract class SyntaxTree
    {
        public abstract string FilePath { get; }

        public abstract int Length { get; }

        public static SyntaxTree ParseText(string text)
        {
            return null;
        }

        public SyntaxNode GetRoot()
        {
            return null;
        }
    }
}
