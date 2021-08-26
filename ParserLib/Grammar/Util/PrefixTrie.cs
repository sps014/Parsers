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
}

public class TrieNode
{
    public bool Active { get; set; } = true;
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
