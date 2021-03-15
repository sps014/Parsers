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

        public Production?[,] ParseTable { get; private set; }

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
                        if (t.Value == Symbols.EPSILON.Value)
                        {
                            foreach (var tt in Table.Follow(new(sym, SymbolType.NonTerminal)))
                            {

                                j = terminal[tt.Value];
                                if (table[i, j] != null && table[i, j].Value.ToString() != pd.ToString())
                                    return false;
                                table[nonTerminal[sym], terminal[tt.Value]] = pd;
                            }
                        }
                        else
                        {
                            j = terminal[t.Value];
                            if (table[i, j] != null && table[i, j].Value.ToString() != pd.ToString())
                                return false;

                            table[i, j] = pd;
                        }
                    }
                }
            }
            ParseTable = table;
            return true;
        }

        public void PrintParseTable(int padd = 20)
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

            Console.Write("".PadRight(padd));

            foreach (var t in terminal.Keys)
            {
                Console.Write(t.PadRight(padd));
            }

            Console.WriteLine();
            for (int i = 0; i <= y; i++)
            {
                Console.Write("".PadRight(padd, '-'));
            }
            Console.WriteLine();

            var keys = nonTerminal.Keys.ToList();
            for (int i = 0; i < x; i++)
            {
                Console.Write(keys[i].PadRight(padd - 5) + "|");

                for (int j = 0; j < y; j++)
                {
                    if (ParseTable[i, j] == null)
                        Console.Write("".PadRight(padd));
                    else
                        Console.Write(ParseTable[i, j].ToString().PadRight(padd));
                }
                Console.WriteLine();
            }

        }

        public bool StackImpl(string input)
        {
            input += Symbols.DOLLAR.Value;

            var terminal = Table.Terminals
           .Where(x => x != Symbols.EPSILON.Value)
           .Select((v, i) => (v, i))
           .ToDictionary(p => p.v, k => k.i);

            int y = terminal.Count;

            var nonTerminal = Table.NonTerminals
            .Select((v, i) => (v, i))
            .ToDictionary(p => p.v, k => k.i);

            int x = nonTerminal.Count;

            Stack<Symbol> stack = new();
            stack.Push(Symbols.DOLLAR);
            stack.Push(Table.StartSymbol);

            while (stack.Count > 0 && input.Length > 0)
            {
                var st = stack.Peek();
                if (st.Type == SymbolType.Terminal)
                {
                    if (st == Symbols.EPSILON)
                        stack.Pop();

                    else if (input[0].ToString() == st.Value)
                    {
                        Console.WriteLine($"Matched {st}=={input[0]}");
                        stack.Pop();
                        input = input[1..];
                    }
                    else
                        return false;
                }
                else if (st.Type == SymbolType.NonTerminal||st.Type==SymbolType.Start)
                {
                    stack.Pop();
                    int j = terminal[input[0].ToString()];
                    int i = nonTerminal[st.Value];

                    if (ParseTable[i, j] != null)
                    {
                        var nss = new List<Symbol>(ParseTable[i, j].Value.Right);
                        Console.WriteLine($"Expanding {ParseTable[i, j]}");
                        nss.Reverse();
                        nss.ForEach(v => stack.Push(v));
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            return input.Length == 0 && input.Length == 0;
        }

    }
}