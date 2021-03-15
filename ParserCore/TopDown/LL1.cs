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

            Production?[,] table = new Production?[x, y];

            foreach (var (sym, productions) in Table.Productions)
            {
                foreach (var pd in productions)
                {
                    var first = Table.First(pd);
                    foreach (var t in first)
                    {
                        int i = nonTerminal[sym];
                        int j = 0;
                        if (t.Value != Symbols.EPSILON.Value)
                        {
                            j = terminal[t.Value];
                            if (table[i, j] != null && table[i, j].Value.ToString() != pd.ToString())
                                return false;

                            table[i, j] = pd;
                        }
                        else
                        {
                            foreach (var tt in Table.Follow(new(sym, SymbolType.NonTerminal)))
                            {

                                j = terminal[tt.Value];
                                if (table[i, j] != null && table[i, j].Value.ToString() != pd.ToString())
                                    return false;
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