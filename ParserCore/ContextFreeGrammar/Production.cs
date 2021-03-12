
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Parsers.Grammar
{
    /// <summary>
    ///  Indicates a production in BNF eg. A->id*A .
    /// alternate production should be created as separate instance aka A-> eps | aAa |bBA
    ///should be A-> epsilon   , A-> aAa , A-> bBA
    /// </summary>
    public readonly struct Production : IEnumerable<Symbol>, IEqualityComparer<Production>
    {

        /// <summary>
        /// Get the Left Hand Side of the production.
        /// ie. A->a+A  will return A 
        /// </summary>
        public readonly string Left { get; init; }
        /// <summary>
        /// Get the Right Hand Side of the production.
        /// ie. A->a+A  will return a+A 
        /// </summary>
        public readonly List<Symbol> Right { get; init; }
        /// <summary>
        /// create a production with left symbol name
        /// </summary>
        /// <param name="leftSymbolname">Left hand side Terminal name</param>
        public Production([DisallowNull] string leftSymbolname)
        {
            Left = leftSymbolname;
            Right = new();
        }
        /// <summary>
        /// Get the Right Hand Side of the production as String.
        /// ie. A->a+A  will return "a+A" 
        /// </summary>
        public readonly string RightAsString => string.Join("", Right.Select(x => x.Value));

        public override string ToString()
        {
            var symbols = Right.Select(x => x.Value);
            return $"{Left}->{string.Join("", symbols)}";
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            return Right.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Right.GetEnumerator();
        }

        public bool Equals(Production x, Production y)
        {
            return x.Left == y.Left && y.RightAsString == x.RightAsString;
        }

        public int GetHashCode([DisallowNull] Production obj)
        {
            return HashCode.Combine(obj.Right.GetHashCode(), obj.Left.GetHashCode());
        }
    }

}
