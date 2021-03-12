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
            var res = new HashSet<Symbol>();
            foreach (var v in Table[p])
            {
                GetFirst(v).ToList().ForEach(x => res.Add(x));
            }
            return res;
        }
        HashSet<Symbol> GetFirst([NotNull] Production p)
        {
            var res = new HashSet<Symbol>();
            if (p.Right[0].Type == SymbolType.Terminal)
            {
                res.Add(p.Right[0]);
            }
            else if (p.Right[0].Type == SymbolType.NonTerminal)
            {
                foreach (var s in Table[p.Right[0]])
                {
                    GetFirst(s).ToList().ForEach(x => res.Add(x));
                }
            }
            return res;
        }

    }
}