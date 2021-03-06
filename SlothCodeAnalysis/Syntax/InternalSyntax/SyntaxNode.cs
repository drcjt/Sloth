﻿namespace SlothCodeAnalysis.Syntax.InternalSyntax
{
    internal abstract class SyntaxNode : GreenNode
    {
        internal SyntaxNode(SyntaxKind kind) : base(kind)
        {
        }

        internal SyntaxNode(SyntaxKind kind, int fullWidth) : base(kind, fullWidth)
        {
        }
    }
}
