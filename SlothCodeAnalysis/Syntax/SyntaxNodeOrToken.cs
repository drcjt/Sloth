using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothCodeAnalysis.Syntax
{
    public class SyntaxNodeOrToken
    {
        // In a case if we are wrapping a SyntaxNode this is the SyntaxNode itself.
        // In a case where we are wrapping a token, this is the token's parent.
        private readonly SyntaxNode _nodeOrParent;

        // Green node for the token. 
        private readonly InternalSyntax.GreenNode _token;

        // Used in both node and token cases.
        // When we have a node, _position == _nodeOrParent.Position.
        private readonly int _position;

        // TODO - IS THIS NEEDED???
        // Index of the token among parent's children. 
        // This field only makes sense if this is a token.
        // For regular nodes it is set to -1 to distinguish from default(SyntaxToken)
        private readonly int _tokenIndex;

        internal SyntaxNodeOrToken(SyntaxNode node)
        {
            if (node != null)
            {
                _position = node.Position;
                _nodeOrParent = node;
            }

            _tokenIndex = -1;
        }

        internal SyntaxNodeOrToken(SyntaxNode parent, InternalSyntax.GreenNode token, int position, int index)
        {
            _position = position;
            _tokenIndex = index;
            _nodeOrParent = parent;
            _token = token;
        }

        /// <summary>
        /// Returns a new <see cref="SyntaxNodeOrToken"/> that wraps the supplied node.
        /// </summary>
        /// <param name="node">The input node.</param>
        /// <returns>
        /// A <see cref="SyntaxNodeOrToken"/> that wraps the supplied node.
        /// </returns>
        public static implicit operator SyntaxNodeOrToken(SyntaxNode node)
        {
            return new SyntaxNodeOrToken(node);
        }

        /// <summary>
        /// Determines whether this <see cref="SyntaxNodeOrToken"/> is wrapping a token.
        /// </summary>
        public bool IsToken => !IsNode;

        /// <summary>
        /// Determines whether this <see cref="SyntaxNodeOrToken"/> is wrapping a node.
        /// </summary>
        public bool IsNode => _tokenIndex < 0;

        /// <summary>
        /// Returns the underlying node if this <see cref="SyntaxNodeOrToken"/> is wrapping a
        /// node.
        /// </summary>
        /// <returns>
        /// The underlying node if this <see cref="SyntaxNodeOrToken"/> is wrapping a node.
        /// </returns>
        public SyntaxNode AsNode()
        {
            if (_token != null)
            {
                return null;
            }

            return _nodeOrParent;
        }

        /// <summary>
        /// Returns the underlying token if this <see cref="SyntaxNodeOrToken"/> is wrapping a
        /// token.
        /// </summary>
        /// <returns>
        /// The underlying token if this <see cref="SyntaxNodeOrToken"/> is wrapping a token.
        /// </returns>
        public SyntaxToken AsToken()
        {
            if (_token != null)
            {
                return new SyntaxToken(_nodeOrParent, _token, _position, _tokenIndex);
            }

            return default(SyntaxToken);
        }
    }
}
