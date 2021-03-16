using System;
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

        public void Print()
        {
            DfsPrint(Root);
        }
        void DfsPrint(SyntaxNode current, int sp = 0)
        {
            if (current is null)
                return;
            Console.WriteLine(current.Value.ToString().PadLeft(sp, '-'));
            foreach (var c in current.Children)
            {
                DfsPrint(c, sp + 4);
            }
        }
    }
}