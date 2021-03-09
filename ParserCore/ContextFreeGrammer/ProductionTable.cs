using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Parsers.Grammer
{
    public class ProductionTable : IEnumerable<List<Production>>
    {
        private readonly Dictionary<string, List<Production>> productions = new();

        public ProductionTable(List<Production> prods = null)
        {
            if (prods == null)
                return;

            AddRange(prods);

        }
        public void Add([NotNull] Production p)
        {
            if (!productions.ContainsKey(p.Left))
            {
                productions.Add(p.Left, new List<Production>());
            }
            productions[p.Left].Add(p);
        }
        public void AddRange([NotNull] List<Production> prods)
        {
            foreach (var v in prods)
            {
                Add(v);
            }
        }
        public bool Contains([NotNull] string nonTerminalSymbol)
        {
            return productions.ContainsKey(nonTerminalSymbol);
        }

        public IEnumerator<List<Production>> GetEnumerator()
        {
            return productions.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return productions.Values.GetEnumerator();
        }

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