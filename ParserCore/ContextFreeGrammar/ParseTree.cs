using System.Collections.Generic;

namespace Parsers.Grammar
{
    public class SyntaxNode
    {
        public Symbol Value { get; init; }
        public List<SyntaxNode> Children { get; set; } = new();
    }
    public class SyntaxTree
    {
        public SyntaxNode Root { get; private set; }
        private List<Symbol> input;
    }
}