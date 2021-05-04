using System;
using System.Text;

namespace SlothCodeAnalysis.Text
{
    public abstract class SourceText
    {
        public abstract int Length { get; }

        public abstract char this[int position] { get; }

        public static SourceText From(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            return new StringText(text);
        }

        /// <summary>
        /// Copy a range of characters from this SourceText to a destination array.
        /// </summary>
        public abstract void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);

        public virtual string ToString(TextSpan span)
        {
            // default implementation constructs text using CopyTo
            var builder = new StringBuilder();
            var buffer = new char[Math.Min(span.Length, 1024)];

            int position = Math.Max(Math.Min(span.Start, this.Length), 0);
            int length = Math.Min(span.End, this.Length) - position;

            while (position < this.Length && length > 0)
            {
                int copyLength = Math.Min(buffer.Length, length);
                this.CopyTo(position, buffer, 0, copyLength);
                builder.Append(buffer, 0, copyLength);
                length -= copyLength;
                position += copyLength;
            }

            return builder.ToString();
        }
    }
}
