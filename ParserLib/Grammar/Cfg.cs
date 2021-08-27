using System.Collections.Generic;
using ParserLib.Grammar.Util;

namespace ParserLib.Grammar;

public class Cfg
{
        public Symbol Start { get; set; }
        private Dictionary<Symbol, HashSet<Production>> _nt_map=new();

        public Cfg(string start=null!)
        {
                if (!string.IsNullOrWhiteSpace(start))
                        Start = Symbol.NonTerminal(start);
        }
        public Cfg(Symbol start)
        { 
                Start = start;
        }
        public Cfg(string start,Dictionary<Symbol, HashSet<Production>> nt_map)
        {
                Start = Symbol.NonTerminal(start);
                _nt_map = nt_map;
        }
        public void AddProduction(Production p)
        {
                if (!_nt_map.ContainsKey(p.Left))
                {
                        _nt_map.Add(p.Left,new());
                }
                _nt_map[p.Left].Add(p);
        }

        public void EliminateLeftFactoring()
        {
                foreach (var k in _nt_map.Keys)
                {
                        BuildPrefixTree(k);
                }
        }
        private  List<Symbol> BuildPrefixTree(Symbol symbol)
        {
                //let mut common=Vec::new();
                var prefixTree = new PrefixTrie();
                foreach (var v in _nt_map[symbol])
                {
                        prefixTree.AddProduction(v);
                }
                prefixTree.MarkNodes();
                var productions=prefixTree.GenerateProductions(symbol);
                
                prefixTree.Print();
                return new List<Symbol>();
        }
}