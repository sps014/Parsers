using System.Collections.Generic;

namespace Parsers.Grammar
{
    public class SyntaxNode
    {
        public Symbol Value { get; set; }
        public List<SyntaxNode> Children { get; set; } = new();
    }
    public class SyntaxTree
    {
        public SyntaxNode Root { get; set; }
    }
}