using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Parsers.Grammar
{
    public class ProductionTable : IEnumerable<List<Production>>
    {
        private Dictionary<string, List<Production>> productions = new();
        private readonly Dictionary<string, string> inverseProductions = new();
        public Dictionary<string, List<(int Index, Production Production)>> NonTerminalPointers = new();
        public Symbol StartSymbol { get; set; }

        public ProductionTable(List<Production> prods = null)
        {
            if (prods == null)
                return;

            AddRange(prods);

        }

        public void Add([NotNull] Production p)
        {
            inverseProductions.TryAdd(p.RightAsString, p.Left);

            if (!productions.ContainsKey(p.Left))
                productions.Add(p.Left, new List<Production>());

            foreach (var (pd, i) in p.Right.Select((v, i) => (v, i)))
            {
                if (pd.Type == SymbolType.Terminal)
                    Terminals.Add(pd.Value);
                else if (pd.Type == SymbolType.NonTerminal)
                {
                    if (!NonTerminalPointers.ContainsKey(pd.Value))
                    {
                        NonTerminalPointers.Add(pd.Value, new());
                    }
                    NonTerminalPointers[pd.Value].Add((i, p));
                    NonTerminals.Add(pd.Value);
                }
                if (pd.Type == SymbolType.Start)
                    StartSymbol = pd;
            }
            productions[p.Left].Add(p);
        }
        public void AddRange([NotNull] List<Production> prods) =>
            prods.ForEach(v => Add(v));
        public bool Contains([NotNull] string nonTerminalSymbol) =>
            productions.ContainsKey(nonTerminalSymbol);

        public bool ContainsProduction([NotNull] string production) =>
            inverseProductions.ContainsKey(production);

        public HashSet<Symbol> First([NotNull] Symbol p)
        {

            var res = new HashSet<Symbol>();

            if (p.Type == SymbolType.NonTerminal)
                foreach (var v in this[p])
                {
                    res.UnionWith(GetFirst(v));
                }
            else if (p.Type == SymbolType.Terminal)
                res.Add(p);

            return res;
        }
        private HashSet<Symbol> GetFirst([NotNull] Production p, int pos = 0)
        {
            var res = new HashSet<Symbol>();
            if (pos >= p.Right.Count)
                return res;

            if (p.Right[pos].Type == SymbolType.Terminal)
            {
                res.Add(p.Right[pos]);
            }
            else if (p.Right[pos].Type == SymbolType.NonTerminal)
            {
                foreach (var v in this[p.Right[pos]])
                {
                    var l = GetFirst(v);

                    if (l.Contains(Symbols.EPSILON))
                    {
                        if (pos < p.Right.Count - 1)
                        {
                            l.Remove(Symbols.EPSILON);
                            res.UnionWith(GetFirst(p, pos + 1));

                        }
                    }
                    res.UnionWith(l);
                }
            }
            return res;
        }

        public HashSet<Symbol> Follow(Symbol s)
        {
            HashSet<Symbol> res;

            res = GetFollow(s);
            if (s.Value == StartSymbol.Value)
                res.Add(Symbols.DOLLAR);

            return res;
        }

        private HashSet<Symbol> GetFollow(Symbol s)
        {
            var res = new HashSet<Symbol>();
            if (s.Type == SymbolType.NonTerminal && !NonTerminalPointers.ContainsKey(s.Value))
                return res;
            foreach (var (index, production) in NonTerminalPointers[s.Value])
            {
                if (index == production.Right.Count - 1)
                {
                    if (production.Left != s.Value)
                        Follow(new Symbol(production.Left, SymbolType.NonTerminal))
                       .ToList()
                       .ForEach(x => res.Add(x));
                }
                else
                {
                    if (production.Right[index + 1].Type == SymbolType.Terminal)
                        res.Add(production.Right[index + 1]);
                    else
                        foreach (var firstSymbol in First(production.Right[index + 1]))
                        {
                            if (firstSymbol.Value != Symbols.EPSILON.Value)
                                res.Add(firstSymbol);
                            else
                            {

                                if (production.Right[index + 1].Type == SymbolType.NonTerminal)
                                    Follow(production.Right[index + 1])
                                    .ToList()
                                    .ForEach(x => res.Add(x));
                            }

                        }
                }
            }
            return res;
        }

        /// <summary>
        /// Find LHS symbol from a Production 
        /// </summary>
        /// <param name="production">Production as string</param>
        /// <returns>return LHS symbol string</returns>
        public string InverseLookup([NotNull] string production) =>
            ContainsProduction(production) ? inverseProductions[production] : null;

        public IEnumerator<List<Production>> GetEnumerator() =>
            productions.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            productions.Values.GetEnumerator();

        /// <summary>
        /// Get set of all terminals in all productions.
        /// </summary>
        public HashSet<string> Terminals { get; private set; } = new() { "$" };
        /// <summary>
        /// Get set of all non terminals in all productions.
        /// </summary>
        public HashSet<string> NonTerminals { get; private set; } = new();

        /// <summary>
        /// fetch list of non terminal's production
        /// </summary>
        /// <param name="nonTerminalSymbol">value for non terminal</param>
        /// <returns>returns list of non terminal's production</returns>
        public List<Production> this[[NotNull] string nonTerminalSymbol]
        {
            get
            {
                if (productions.ContainsKey(nonTerminalSymbol))
                    return productions[nonTerminalSymbol];

                return null;
            }
        }
        public List<Production> this[[NotNull] Symbol nonTerminalSymbol]
        {
            get
            {
                if (productions.ContainsKey(nonTerminalSymbol.Value))
                    return productions[nonTerminalSymbol.Value];

                return null;
            }
        }
    }
}