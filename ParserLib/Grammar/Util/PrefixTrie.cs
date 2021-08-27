using System;
using System.Collections.Generic;

namespace ParserLib.Grammar.Util;

public class PrefixTrie
{
    public TrieNode Root { get; } = new TrieNode();

        public void AddProduction(Production p)
        {
            var current = Root;
            foreach (var r in p.Right)
            {
                current = current.Add(r);
            }
        }

        public void MarkNodes()
        {
            MarkNodes(Root);
        }

        private void MarkNodes(TrieNode node)
        {
            bool childTrue = false;
            foreach (var k in node.Children.Keys)
            {
                MarkNodes(node.Children[k]);
                if (node.Children[k].Active)
                    childTrue = true;
            }
            
            if (node.Children.Count > 1 || childTrue)
            {
                node.Active = true;
            }
            
        }

        public HashSet<Production> GenerateProductions(Symbol left)
        {
            HashSet<Production> p = new();
            foreach (var (k,v) in Root.Children)
            {
                GenerateProductions(v,k,left,new List<Symbol>(){},p);
            }

            return p;
        }

        private void GenerateProductions(TrieNode parent,Symbol current,Symbol forNonTerminal,List<Symbol> build,HashSet<Production> productions)
        {
            build.Add(current);
            if (parent.Children.Count == 0)
            {
                Production p = new(forNonTerminal,new List<Symbol>(build));
                productions.Add(p);
            }
            else if (parent.Active && parent.Children.Count >= 2)
            {
                Production p = new(forNonTerminal,new List<Symbol>(build));
                productions.Add(p);
                //build k dash
            }
            else
            {
                foreach (var (k,v) in parent.Children)
                {
                    GenerateProductions(v, k, forNonTerminal, build, productions);
                }
            }

            

            build.Remove(current);
        }

        public void Print()
        {
            var nr = new TrieNode();
            nr.Children.Add(Symbol.Terminal("Root"),Root );
            Print(nr);
        }

        private void Print(TrieNode n, int space = 2)
        {
            
            foreach (var (k,v) in n.Children)
            {
                Console.Write("└".PadLeft(space,' '));
                Console.WriteLine(k.ToString().PadLeft(8, '─'));
                Print(v,space+8);
            }
        }
}

public class TrieNode
{
    public bool Active { get; set; } = false;
    public Dictionary<Symbol, TrieNode> Children { get; } = new();

    public TrieNode Add(Symbol symbol)
    {
        if (!Children.ContainsKey(symbol))
        {
            TrieNode c = new();
            Children.Add(symbol,c);
        }
        return Children[symbol];
    }
}
