using System;
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

        public bool CreateParseTable()
        {

            var terminal = Table.Terminals
            .Where(x => x != Symbols.EPSILON.Value)
            .Select((v, i) => (v, i))
            .ToDictionary(p => p.v, k => k.i);

            int y = terminal.Count;

            var nonTerminal = Table.NonTerminals
            .Select((v, i) => (v, i))
            .ToDictionary(p => p.v, k => k.i);

            int x = nonTerminal.Count;

            Production[,] table = new Production[x, y];

            foreach (var (sym, productions) in Table.Productions)
            {
                foreach (var pd in productions)
                {
                    var first = Table.First(pd);
                    foreach (var t in first)
                    {
                        if (t.Value != Symbols.EPSILON.Value)
                        {
                            table[nonTerminal[sym], terminal[t.Value]] = pd;
                        }
                        else
                        {
                            foreach (var tt in Table.Follow(new(sym, SymbolType.NonTerminal)))
                            {
                                table[nonTerminal[sym], terminal[tt.Value]] = pd;
                            }
                        }
                    }
                }
            }

            return true;
        }


    }
}