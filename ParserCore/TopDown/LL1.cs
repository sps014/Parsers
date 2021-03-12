using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Parsers.Grammar;

namespace Parsers.TopDown
{
    public class LL1
    {
        public LL1([NotNull] ProductionTable table)
        {
            Table = table;
        }

        public ProductionTable Table { get; init; }

        public HashSet<Symbol> First([NotNull] Symbol p)
        {
            return Table.First(p);
        }
        public HashSet<Symbol> Follow(Symbol s)
        {
            var res = new HashSet<Symbol>();
            if (s.Type == SymbolType.Start)
                res.Add(Symbols.DOLLAR);

            return res;
        }

        HashSet<Symbol> GetFollow(Production p, Symbol s, ref Dictionary<Symbol, HashSet<Symbol>> MemoFollow)
        {
            var res = new HashSet<Symbol>();
            //foreach (var prods in Table.Productions.Values)
            {
            }
            return res;
        }

    }
}