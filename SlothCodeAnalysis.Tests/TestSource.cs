using SlothCodeAnalysis.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlothCodeAnalysis.Tests
{
    class TestSource
    {
        public static TestSource None => new TestSource(null);

        public object Value { get; }

        private TestSource(object value)
        {
            Value = value;
        }

        public SyntaxTree GetSyntaxTree()
        { 
            switch (Value)
            {
                case string source:
                    return TestBase.Parse(source);
                case SyntaxTree tree:
                    return tree;
                case null:
                    return null;
                default:
                    throw new Exception($"Unexpected value: {Value}");
            }
        }

        public static implicit operator TestSource(string source) => new TestSource(source);
        public static implicit operator TestSource(SyntaxTree source) => new TestSource(source);
    }
}
