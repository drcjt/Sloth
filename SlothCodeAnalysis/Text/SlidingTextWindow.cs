using System;

namespace SlothCodeAnalysis.Text
{
    /// <summary>
    /// A sliding window over the SourceText of a file for the lexer.
    /// 
    /// By using character arrays this allows for much faster "substring" than String.Substring - this is
    /// primarily used in GetText.
    /// Note GetText can be further improved by using interning for common character strings
    ///
    ///
    /// TODO: ideally need to use an object pool of char arrays rather than have this always allocate new char arrays
    /// </summary>
    public sealed class SlidingTextWindow
    {
        private readonly SourceText _text;
        private int _basis;
        private int _offset;
        private readonly int _textEnd;

        private int _lexemeStart;

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

            // Populate window
            int amountToRead = Math.Min(_textEnd - _characterWindowCount, _characterWindow.Length);
            _characterWindowCount = _characterWindow.Length;
            _text.CopyTo(0, _characterWindow, 0, amountToRead);
            _characterWindowCount = amountToRead;
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

        public int Width
        {
            get
            {
                return _offset - _lexemeStart;
            }
        }

        public void Start()
        {
            _lexemeStart = _offset;
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
            if (c != InvalidCharacter)
            {
                this.AdvanceChar();
            }
            return c;
        }

        public char PeekChar()
        {
            // Do we need to read more characters
            if (_offset >= _characterWindowCount && !MoreChars())
            {
                // If no more characters to read then return invalid character
                return InvalidCharacter;
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

                if (_characterWindowCount >= _characterWindow.Length)
                {
                    char[] oldWindow = _characterWindow;
                    char[] newWindow = new char[_characterWindow.Length * 2];
                    Array.Copy(oldWindow, 0, newWindow, 0, _characterWindowCount);
                    _characterWindow = newWindow;
                }

                int amountToRead = Math.Min(_textEnd - (_basis + _characterWindowCount), _characterWindow.Length - _characterWindowCount);
                _text.CopyTo(_basis + _characterWindowCount, _characterWindow, _characterWindowCount, amountToRead);
                _characterWindowCount += amountToRead;
                return amountToRead > 0;
            }

            return true;
        }

        public string GetText()
        {
            return GetText(_lexemeStart, Width);
        }

        public string GetText(int position, int length)
        {
            int offset = position - _basis;

            // TODO: Use string interning here to avoid allocation of new string objects

            return new string(_characterWindow, offset, length);
        }
    }
}
