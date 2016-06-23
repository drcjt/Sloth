using SlothCodeAnalysis.Syntax;
using NUnit.Framework;
using System.Collections.Generic;

namespace SlothCodeAnalysis.Tests
{
    [TestFixture]
    class LexicalTests
    {
        private IEnumerable<SyntaxToken> Lex(string text)
        {
            return SyntaxFactory.ParseTokens(text);
        }

        private SyntaxToken LexToken(string text)
        {
            SyntaxToken result = default(SyntaxToken);
            foreach (var token in Lex(text))
            {
                if (result == null)
                {
                    result = token;
                }
                else if (token.Kind == SyntaxKind.EndOfFileToken)
                {
                    continue;
                }
                else
                {
                    Assert.Fail($"More than one token was lexed {token}");
                }
            }
            if (result.Kind == SyntaxKind.None)
            {
                Assert.Fail("No tokens were lexed");
            }
            return result;
        }

        [Test]
        public void TestProgramWithLoop()
        {
            var text =
@"var ntimes = 0;
print ""How much do you love this company? (1-10) "";
read_int ntimes;
        var x = 0;
for x = 0 to ntimes do
   print ""Developers!"";
end;
print ""Who said sit down?!!!!!"";";

            var lexedText = "";
            foreach (var token in Lex(text))
            {
                lexedText += token.LeadingTrivia + token.Text + token.TrailingTrivia;
            }

            Assert.AreEqual(text, lexedText);
        }

        [Test]
        public void TestSingleLetterIdentifier()
        {
            var text = "a";
            var token = LexToken(text);

            Assert.NotNull(token);
            Assert.AreEqual(SyntaxKind.IdentifierToken, token.Kind);
            Assert.AreEqual(text, token.Text);
        }
    }
}
