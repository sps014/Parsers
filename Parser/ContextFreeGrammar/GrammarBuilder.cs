using System.Collections.Generic;
using System.Linq;

namespace Parsers.Grammar
{
    public static class GrammarBuilder
    {
        public static CFGrammar Build(string input,string lrSeparactor=":=",string symbolSeparator=" ")
        {
            CFGrammar grammar = new();
            var productionsString = input.Split("\r\n").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if (productionsString.Count == 0)
                throw new System.Exception($"No production found ");

            int c = 0;
            foreach (var pd in productionsString)
            {

                var parts = pd.Split(lrSeparactor).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
                if (parts.Length != 2)
                {
                    throw new System.Exception($"Invalid production {pd}");
                }
                var left = parts[0].Replace(" ", "");

                if (string.IsNullOrWhiteSpace(left))
                    throw new System.Exception("left side of production is null");

                if (string.IsNullOrWhiteSpace(parts[1]))
                    throw new System.Exception("right side of production is null");


                foreach (var p1 in parts[1].Split('|'))
                {
                    List<Symbol> right = new();
                    foreach (var s in p1.Split(symbolSeparator).Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        SymbolType type = SymbolType.Terminal;

                        if (char.IsUpper(s.First()))
                            type = SymbolType.NonTerminal;

                        if (s.ToLower() == "eps")
                            right.Add(Symbols.EPSILON);
                        else
                            right.Add(new Symbol(s, type));
                    }

                    if (c == 0)
                        grammar.StartSymbol = new(parts[0].Replace(" ", ""), SymbolType.NonTerminal | SymbolType.Start);

                    Production p = new() { Left = parts[0].Replace(" ", ""), Right = right };
                    grammar.AddRule(p);
                    c++;
                }

            }

            return grammar;
        }
        public static List<Symbol> BuildTerminals(string input,string seperator=" ")
        {
            var lst = new List<Symbol>();
            foreach (var v in input.Split(seperator))
            {
                if (!string.IsNullOrWhiteSpace(v))
                    lst.Add(new(v));
            }
            return lst;
        }
    }
}