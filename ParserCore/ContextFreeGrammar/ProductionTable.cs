using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Parsers.Grammar
{
    public class ProductionTable : IEnumerable<List<Production>>
    {
        private readonly Dictionary<string, List<Production>> productions = new();
        private readonly Dictionary<string, string> inverseProductions = new();

        public Symbol StartSymbol { get; set; }

        public ProductionTable(List<Production> prods = null)
        {
            if (prods == null)
                return;

            AddRange(prods);

        }

        public void Add([NotNull] Production p)
        {
            inverseProductions.Add(p.RightAsString, p.Left);

            if (!productions.ContainsKey(p.Left))
                productions.Add(p.Left, new List<Production>());

            foreach (var pd in p.Right)
            {
                if (pd.Type == SymbolType.Terminal)
                    Terminals.Add(pd.Value);
                else if (pd.Type == SymbolType.NonTerminal)
                    NonTerminals.Add(pd.Value);

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
        /// Find LHS symbol from Production 
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


        public List<Production> this[[NotNull] string nonTerminalSymbol]
        {
            get
            {
                if (productions.ContainsKey(nonTerminalSymbol))
                    return productions[nonTerminalSymbol];

                return null;
            }
        }
    }
}