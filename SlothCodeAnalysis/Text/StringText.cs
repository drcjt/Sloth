namespace SlothCodeAnalysis.Text
{
    public class StringText : SourceText
    {
        private readonly string _source;

        internal StringText(string source)
        {
            _source = source;
        }

        public override int Length => _source.Length;

        public override char this[int position]
        {
            get
            {
                return _source[position];
            }
        }

        public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            _source.CopyTo(sourceIndex, destination, destinationIndex, count);
        }
    }
}
