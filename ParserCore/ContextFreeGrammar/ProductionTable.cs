using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Parsers.Grammar
{
    /// <summary>
    /// Utility class to maintain production lists for calculation of 1st and follow
    /// </summary>
    public class ProductionTable : IEnumerable<List<Production>>
    {
        /// <summary>
        /// Maintains map of All productions associated with non terminal
        /// </summary>
        private readonly Dictionary<string, List<Production>> productions = new();

        /// <summary>
        /// Inverse Map from Production to symbol
        /// </summary>
        private readonly Dictionary<string, string> inverseProductions = new();

        /// <summary>
        /// Maintains the index and production where a non terminal appeared
        /// </summary>
        private readonly Dictionary<string, List<(int Index, Production Production)>> nonTerminalPointers = new();
        /// <summary>
        /// Indicates Start Symbol non terminal
        /// </summary>
        public Symbol StartSymbol { get; set; }

        /// <summary>
        /// Utility class to maintain production lists for calculation of 1st and follow
        /// </summary>
        /// <param name="prods">a production list</param>
        /// <param name="startSymbol">start non terminal</param>
        public ProductionTable(List<Production> prods = null, Symbol? startSymbol = null)
        {
            if (prods == null)
                return;

            if (startSymbol.HasValue)
            {
                if (startSymbol.Value.Type != SymbolType.NonTerminal)
                    throw new System.Exception("Start Symbol must be non terminal.");
                else
                    StartSymbol = startSymbol.Value;
            }

            AddRange(prods);

        }
        /// <summary>
        /// Add a  production
        /// </summary>
        /// <param name="p">a production </param>
        public void Add([NotNull] Production p)
        {
            //maintain inverse list
            inverseProductions.TryAdd(p.RightAsString, p.Left);

            //Add production
            if (!productions.ContainsKey(p.Left))
                productions.Add(p.Left, new List<Production>());

            foreach (var (pd, i) in p.Right.Select((v, i) => (v, i)))
            {
                if (pd.Type == SymbolType.Terminal)
                    Terminals.Add(pd.Value);
                else if (pd.Type == SymbolType.NonTerminal)
                {
                    //maintain non terminal index production pointer for follow calculation
                    if (!nonTerminalPointers.ContainsKey(pd.Value))
                    {
                        nonTerminalPointers.Add(pd.Value, new());
                    }
                    nonTerminalPointers[pd.Value].Add((i, p));
                    NonTerminals.Add(pd.Value);
                }
                //update start symbol
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

        /// <summary>
        /// calculate first for a symbol
        /// </summary>
        /// <param name="p">symbol to cal first</param>
        /// <returns>set of termials</returns>
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
        /// <summary>
        /// Return first of specific production
        /// </summary>
        /// <param name="p">Production</param>
        /// <returns>set of terminals</returns>
        public HashSet<Symbol> First([NotNull] Production p)
        {
            return GetFirst(p);
        }

        /*
        Recursive Function to Calculate first
        p= current production
        pos= position of element whose first to calculate used to handle espilon case
        */
        private HashSet<Symbol> GetFirst([NotNull] Production p, int pos = 0)
        {
            var res = new HashSet<Symbol>();
            //if we are  out of symbols in production return 
            if (pos >= p.Right.Count)
                return res;

            //if we have  terminal add in result
            if (p.Right[pos].Type == SymbolType.Terminal)
            {
                res.Add(p.Right[pos]);
            }
            else if (p.Right[pos].Type == SymbolType.NonTerminal)
            {
                //if non terminal Look for each of its production
                foreach (var v in this[p.Right[pos]])
                {
                    var l = GetFirst(v);

                    // if result has epsilon 
                    if (l.Contains(Symbols.EPSILON))
                    {
                        //if esp is not last symbol we need to subsitute esp in production or simply recursive call with pos+1  
                        if (pos < p.Right.Count - 1)
                        {
                            //we dont put epsilon then in result
                            l.Remove(Symbols.EPSILON);
                            res.UnionWith(GetFirst(p, pos + 1));

                        }
                    }
                    res.UnionWith(l);
                }
            }
            return res;
        }

        /// <summary>
        /// Calculate follow for symbol
        /// </summary>
        /// <param name="s">non terminal symbol</param>
        /// <returns>set of terminal </returns>
        public HashSet<Symbol> Follow(Symbol s)
        {
            HashSet<Symbol> res;

            res = GetFollow(s);
            //if start symbol add $
            if (s.Value == StartSymbol.Value)
                res.Add(Symbols.DOLLAR);

            return res;
        }

        private HashSet<Symbol> GetFollow(Symbol s)
        {
            var res = new HashSet<Symbol>();

            //if not a non terminal or non terminal with no production return 
            if (s.Type == SymbolType.NonTerminal && !nonTerminalPointers.ContainsKey(s.Value))
                return res;


            //get index,production of non terminal in all production it is present right hand side
            foreach (var (index, production) in nonTerminalPointers[s.Value])
            {
                //if last index on right side then res is Follow of left
                if (index == production.Right.Count - 1)
                {
                    if (production.Left != s.Value)
                        res.UnionWith(Follow(new Symbol(production.Left, SymbolType.NonTerminal)));
                }
                else
                {
                    //if terminal add to result
                    if (production.Right[index + 1].Type == SymbolType.Terminal)
                        res.Add(production.Right[index + 1]);
                    else
                        //first(next) symbol is result
                        foreach (var firstSymbol in First(production.Right[index + 1]))
                        {
                            //if first of next not epsilon add to result 
                            if (firstSymbol.Value != Symbols.EPSILON.Value)
                                res.Add(firstSymbol);
                            else
                            {
                                //if non terminal and epsilon we need to add follow(next)
                                if (production.Right[index + 1].Type == SymbolType.NonTerminal)
                                    res.UnionWith(Follow(production.Right[index + 1]));
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

        public IReadOnlyDictionary<string, List<Production>> Productions
        => productions.ToImmutableDictionary();

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
        /// <summary>
        /// fetch list of non terminal's production
        /// </summary>
        /// <param name="nonTerminalSymbol">value for non terminal</param>
        /// <returns>returns list of non terminal's production</returns>
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