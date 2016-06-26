namespace SlothCodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        None = 0,
        List = 1,

        // Punctuation
        SemicolonToken = 1000,
        EqualsToken = 1001,
        PlusToken = 1002,
        MinusToken = 1003,
        AsteriskToken = 1004,
        SlashToken = 1005,

        // Keywords
        VarKeyword = 2001,
        ForKeyword = 2002,
        ToKeyword = 2003,
        DoKeyword = 2004,
        EndKeyword = 2005,
        ReadIntKeyword = 2006,
        ReadStringKeyword = 2007,
        PrintKeyword = 2008,

        // Tokens with Text
        IdentifierToken = 3000,
        NumericLiteralToken = 3001,
        StringLiteralToken = 3002,

        // Trivia
        EndOfLineTrivia = 4000,
        WhitespaceTrivia = 4001,

        // Names
        IdentifierName = 4500,

        // Binary expressions
        AddExpression = 5000,
        SubtractExpression = 5001,
        MultiplyExpression = 5002,
        DivideExpression = 5003,

        // Primary expressions
        NumericLiteralExpression = 5500,
        StringLiteralExpression = 5501,

        // Statements
        Block = 6000,
        PrintStatement = 6001,
        ReadIntStatemt = 6002,
        AssignmentStatement = 6003,
        VariableDeclaration = 6004,
        EmptyStatement = 6005,

        // Jump Statements
        ForStatement = 7000,

        // Declarations
        CompilationUnit = 8000,

        // Other
        EndOfFileToken = 9000,
    }
}
