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
            DfsPrint(Root, 0, 4);
        }
        void DfsPrint(SyntaxNode current, int parent = 0, int sp = 0, bool isLast = false)
        {
            if (current is null)
                return;
            char cb = isLast ? '┕' : '┟';
            Console.WriteLine("".PadRight(parent) + cb + current.Value.ToString().PadLeft(sp, '⎯'));
            parent = sp;
            sp += 4;
            int g = 0;
            foreach (var c in current.Children)
            {
                if (g == current.Children.Count - 1)
                    DfsPrint(c, parent, sp, true);
                else
                    DfsPrint(c, parent, sp);

            }
        }
    }
}