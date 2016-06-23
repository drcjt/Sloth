using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlothCodeAnalysis.Text;

namespace SlothCodeAnalysis.Tests
{
    [TestFixture]
    class SlidingTextWindowTests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            // Act
            int i = 0;
            var nextChar = slidingTextWindow.NextChar();
            while (nextChar != SlidingTextWindow.InvalidCharacter)
            {
                i++;
                if (i == 10)
                {
                    slidingTextWindow.Reset(10);
                }

                slidingTextWindow.AdvanceChar();
                nextChar = slidingTextWindow.NextChar();
            }

            // Assert
        }
    }
}
