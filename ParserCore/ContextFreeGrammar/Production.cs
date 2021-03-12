
using System.Collections.Generic;
using System.Linq;

namespace Parsers.Grammar
{
    /// <summary>
    ///  Indicates a production in BNF eg. A->id*A .
    /// alternate production should be created as separate instance aka A-> eps | aAa |bBA
    ///should be A-> epsilon   , A-> aAa , A-> bBA
    /// </summary>
    public readonly struct Production
    {

        /// <summary>
        /// Get the Left Hand Side of the production.
        /// ie. A->a+A  will return A 
        /// </summary>
        public string Left { get; init; }
        /// <summary>
        /// Get the Right Hand Side of the production.
        /// ie. A->a+A  will return a+A 
        /// </summary>
        public List<Symbol> Right { get; init; }
        /// <summary>
        /// create a production with left symbol name
        /// </summary>
        /// <param name="leftSymbolname">Left hand side Terminal name</param>
        public Production(string leftSymbolname)
        {
            Left = leftSymbolname;
            Right = new();
        }
        /// <summary>
        /// Get the Right Hand Side of the production as String.
        /// ie. A->a+A  will return "a+A" 
        /// </summary>
        public string RightAsString => string.Join("", Right.Select(x => x.Value));

        public override string ToString()
        {
            var symbols = Right.Select(x => x.Value);
            return $"{Left}->{string.Join("", symbols)}";
        }
    }

}
