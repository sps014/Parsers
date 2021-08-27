using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParserLib.Grammar.Util;

namespace ParserLib.Grammar;

public class Cfg
{
    public Symbol Start { get; set; }
    private Dictionary<Symbol, HashSet<Production>> _nt_map = new();

    public Cfg(string start = null!)
    {
        if (!string.IsNullOrWhiteSpace(start))
            Start = Symbol.NonTerminal(start);
    }
    public Cfg(Symbol start)
    {
        Start = start;
    }
    public Cfg(string start, Dictionary<Symbol, HashSet<Production>> nt_map)
    {
        Start = Symbol.NonTerminal(start);
        _nt_map = nt_map;
    }
    public void AddProduction(Production p)
    {
        if (!_nt_map.ContainsKey(p.Left))
            _nt_map.Add(p.Left, new());
        _nt_map[p.Left].Add(p);
    }

    public void EliminateLeftFactoring()
    {
        var keys = _nt_map.Keys.ToList();
        foreach (var k in keys)
            BuildPrefixTree(k);
    }
    private void BuildPrefixTree(Symbol symbol)
    {
        var prefixTree = new PrefixTrie();

        foreach (var v in _nt_map[symbol])
            prefixTree.AddProduction(v);

        prefixTree.MarkNodes();
        var productions = prefixTree.GenerateProductions(symbol);
        EraseAndAdd(productions, symbol);
        //prefixTree.Print();
    }

    private void EraseAndAdd(HashSet<Production> p, Symbol original)
    {
        var left = p.Select(p => p.Left).Distinct().ToList();
        foreach (var l in left)
        {
            if (_nt_map.ContainsKey(l))
                _nt_map[l].Clear();
        }

        foreach (var pd in p)
        {
            AddProduction(pd);
        }
        foreach (var l in left)
        {
            if (l != original)
            {
                BuildPrefixTree(l);
            }
        }
    }
    public void EliminateLeftRecursion()
    {

    }
    public void Print()
    {
        Console.WriteLine(ToString());
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("");
        foreach (var v in _nt_map.Values)
            sb.Append(string.Join(" | ", v.Select(x => x.ToString())) + "\n");
        return sb.ToString();
    }
}