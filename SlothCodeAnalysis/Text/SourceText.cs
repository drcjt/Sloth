using System;

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
    }
}
