using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Parsers.Grammer
{
    public class ProductionTable : IEnumerable<List<Production>>
    {
        private readonly Dictionary<string, List<Production>> productions = new();
        private readonly Dictionary<string, string> inverseProductions = new();

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
                    Terminals.Add(pd.SymbolName);
                else if (pd.Type == SymbolType.NonTerminal)
                    NonTerminals.Add(pd.SymbolName);
            }
            productions[p.Left].Add(p);
        }
        public void AddRange([NotNull] List<Production> prods) =>
            prods.ForEach(v => Add(v));
        public bool Contains([NotNull] string nonTerminalSymbol) =>
            productions.ContainsKey(nonTerminalSymbol);

        public bool ContainsProduction([NotNull] string production) =>
            inverseProductions.ContainsKey(production);

        public string InverseProduction([NotNull] string production) =>
            ContainsProduction(production) ? inverseProductions[production] : null;

        public IEnumerator<List<Production>> GetEnumerator() =>
            productions.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            productions.Values.GetEnumerator();

        public HashSet<string> Terminals { get; private set; } = new() { "$" };
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