﻿using System;

namespace SlothCodeAnalysis.Text
{
    // A sliding buffer over the SourceText of a file for the lexer.
    public sealed class SlidingTextWindow
    {
        private readonly SourceText _text;
        private int _basis;
        private int _offset;
        private readonly int _textEnd;

        private const int DefaultWindowLength = 20; // Set to 50 to be more testing as sample input is going to be pretty small for this compiler

        public const char InvalidCharacter = char.MaxValue;

        private char[] _characterWindow = new char[DefaultWindowLength];
        private int _characterWindowCount;

        public SlidingTextWindow(SourceText text)
        {
            _text = text;
            _basis = 0;
            _offset = 0;
            _textEnd = text.Length;
        }

        public SourceText Text
        {
            get
            {
                return _text;
            }
        }

        // The current absolute position in the text file.
        public int Position
        {
            get
            {
                return _basis + _offset;
            }
        }

        // The current offset inside the window, relative to the window start.
        public int Offset
        {
            get
            {
                return _offset;
            }
        }

        public void AdvanceChar()
        {
            _offset++;
        }

        public void AdvanceChar(int n)
        {
            _offset += n;
        }

        public char NextChar()
        {
            char c = PeekChar();
            return c;
        }

        public char PeekChar()
        {
            // Do we need to read more characters
            if (_offset >= _characterWindowCount)
            {
                if (!MoreChars())
                {
                    // If no more characters to read then return invalid character
                    return InvalidCharacter;
                }
            }

            return _characterWindow[_offset];
        }

        public void Reset(int position)
        {
            int relative = position - _basis;
            if (relative >= 0 && relative <= _characterWindowCount)
            {
                _offset = relative;
            }
            else
            {
                // Reread text buffer
                int amountToRead = Math.Min(_text.Length, position + _characterWindow.Length) - position;
                amountToRead = Math.Max(amountToRead, 0);
                if (amountToRead > 0)
                {
                    _text.CopyTo(position, _characterWindow, 0, amountToRead);
                }

                _offset = 0;
                _basis = position;
            }
        }

        private bool MoreChars()
        {
            if (_offset >= _characterWindowCount)
            {
                if (Position >= _textEnd)
                {
                    return false;
                }

                int amountToRead = Math.Min(_textEnd - (_basis + _characterWindowCount), _characterWindow.Length - _characterWindowCount);
                _text.CopyTo(_basis + _characterWindowCount, _characterWindow, _characterWindowCount, amountToRead);
                _characterWindowCount += amountToRead;
                return amountToRead > 0;
            }

            return true;
        }
    }
}
