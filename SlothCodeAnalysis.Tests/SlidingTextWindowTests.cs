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
        public void Text_WithSlidingTextWindowCreatedWithSetText_ReturnsText()
        {
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            Assert.AreEqual(sourceText, slidingTextWindow.Text);
        }

        [Test]
        public void Position_WithNewSlidingTextWindow_IsZero()
        {
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            Assert.AreEqual(0, slidingTextWindow.Position);
        }

        [Test]
        public void Offset_WithNewSlidingTextWindow_IsZero()
        {
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            Assert.AreEqual(0, slidingTextWindow.Offset);
        }

        [Test]
        public void AdvanceChar_OnNewSlidingTextWindow_ChangesOffsetTo1()
        {
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            slidingTextWindow.AdvanceChar();

            Assert.AreEqual(1, slidingTextWindow.Offset);
        }

        [Test]
        public void AdvanceCharBy5_OnNewSlidingTextWindow_ChangesOffsetTo5()
        {
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            slidingTextWindow.AdvanceChar(5);

            Assert.AreEqual(5, slidingTextWindow.Offset);
        }

        [Test]
        public void PeekChar_WithOffsetLessThanDefaultWindowLength_ReturnsCharacter()
        {
            var text = "abcdefghijklmnopqrstuvwyx0123456789";
            var sourceText = SourceText.From(text);
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            var peekedChar = slidingTextWindow.PeekChar();

            Assert.AreEqual(text[0], peekedChar);
        }

        [Test]
        public void PeekChar_WithOffsetMoreThanDefaultWindowLength_ReturnsCharacter()
        {
            var text = "abcdefghijklmnopqrstuvwyx0123456789";
            var sourceText = SourceText.From(text);
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            slidingTextWindow.AdvanceChar(30);

            var peekedChar = slidingTextWindow.PeekChar();

            Assert.AreEqual(text[30], peekedChar);
        }

        [Test]
        public void NextChar_WithNoText_ReturnsInvalidCharacter()
        {
            var slidingTextWindow = new SlidingTextWindow(SourceText.From(""));

            Assert.AreEqual(SlidingTextWindow.InvalidCharacter, slidingTextWindow.NextChar());
        }

        [Test]
        public void NextChar_WithNoText_DoesNotChangePosition()
        {
            var slidingTextWindow = new SlidingTextWindow(SourceText.From(""));

            slidingTextWindow.NextChar();

            Assert.AreEqual(0, slidingTextWindow.Position);
        }

        [Test]
        public void NextChar_WithValidCharacter_IncrementsPosition()
        {
            var slidingTextWindow = new SlidingTextWindow(SourceText.From("A"));

            slidingTextWindow.NextChar();

            Assert.AreEqual(1, slidingTextWindow.Position);
        }

        [Test]
        public void Reset_WithPositionInWindow_SetOffsetBasedOnStartOfWindow()
        {
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            // Setup a non zero basis
            slidingTextWindow.Reset(25);

            // Real test
            slidingTextWindow.Reset(30);

            Assert.AreEqual(5, slidingTextWindow.Offset); 
        }

        [Test]
        public void Reset_WithPositionNotInWindow_MovesWindow()
        {
            var sourceText = SourceText.From("abcdefghijklmnopqrstuvwyx0123456789");
            var slidingTextWindow = new SlidingTextWindow(sourceText);

            slidingTextWindow.Reset(25);

            Assert.AreEqual(0, slidingTextWindow.Offset);
            Assert.AreEqual(25, slidingTextWindow.Position);
        }
    }
}
