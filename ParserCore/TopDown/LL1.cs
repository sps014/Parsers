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
            return Table.Follow(s);
        }


    }
}