using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Parsers.Grammer
{
    public class ProductionTable
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
        public List<Production> this[string nonTerminalSymbol]
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