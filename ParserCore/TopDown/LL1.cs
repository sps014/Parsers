using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Parsers.Grammar;

namespace Parsers.TopDown
{
    public class LL1
    {
        public LL1([NotNull] CFGrammer table)
        {
            Table = table;
        }

        public CFGrammer Table { get; init; }
        public SyntaxTree Tree { get; private set; }

        public Production?[,] ParseTable { get; private set; }

        public bool CreateParseTable()
        {

            var terminal = Terminals;

            int y = terminal.Count;

            var nonTerminal = NonTerminals;

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
            var terminal = Terminals;

            int y = terminal.Count;

            var nonTerminal = NonTerminals;

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
        private Dictionary<string, int> Terminals => Table.Terminals
           .Where(x => x != Symbols.EPSILON.Value)
           .Select((v, i) => (v, i))
           .ToDictionary(p => p.v, k => k.i);

        private Dictionary<string, int> NonTerminals => Table.NonTerminals
            .Select((v, i) => (v, i))
            .ToDictionary(p => p.v, k => k.i);


        public bool StackImpl(IReadOnlyList<Symbol> inputs)
        {
            Console.WriteLine();
            var input = new List<Symbol>(inputs)
            {
                Symbols.DOLLAR
            };

            var terminal = Terminals;

            var nonTerminal = NonTerminals;

            int x = nonTerminal.Count;
            Stack<Symbol> stack = new();
            stack.Push(Symbols.DOLLAR);
            stack.Push(Table.StartSymbol);

            SyntaxTree tree = new();

            var root = new SyntaxNode() { Value = stack.Peek() };
            tree.Root = root;

            if (!DfsStack(ref input, stack, root, terminal, nonTerminal))
                return false;

            if (input.Count == 0)
            {
                Tree = tree;
                return true;
            }

            return false;
        }

        private bool DfsStack(ref List<Symbol> input, Stack<Symbol> stack, SyntaxNode parent, Dictionary<string, int> terms, Dictionary<string, int> nonterms)
        {
            while (stack.Count > 0 && input.Count > 0)
            {
                var top = stack.Peek();
                if (top.Type == SymbolType.Terminal)
                {
                    if (top != Symbols.EPSILON)
                    {
                        if (top.Value == input[0].ToString())
                        {
                            var node = new SyntaxNode() { Value = top };
                            if (top != Symbols.DOLLAR)
                                parent.Children.Add(node);
                            Console.WriteLine($"Matched {top.Value}=={input[0]}");
                            stack.Pop();
                            input.RemoveAt(0);

                        }
                        else
                        {
                            Console.WriteLine($"MisMatched {top.Value}!={input[0]}");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Consumed {Symbols.EPSILON}");
                        stack.Pop();
                    }
                }
                else
                {
                    int j = terms[input[0].ToString()];
                    int i = nonterms[top.Value];
                    if (ParseTable[i, j] != null)
                    {
                        stack.Pop();

                        var nss = new List<Symbol>(ParseTable[i, j].Value.Right);
                        Console.WriteLine($"Expanding {ParseTable[i, j]}");
                        nss.Reverse();
                        var ns = new Stack<Symbol>();
                        nss.ForEach(v => ns.Push(v));
                        var node = new SyntaxNode() { Value = top };
                        parent.Children.Add(node);


                        if (!DfsStack(ref input, ns, node, terms, nonterms))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Not Expanding not entry found for {top} {input[0]}");
                        return false;
                    }
                }
            }
            return true;
        }

    }
}