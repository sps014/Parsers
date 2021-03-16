using System.Collections.Generic;
using System.Linq;

namespace Parsers.Grammar
{
    public static class GrammerBuilder
    {
        public static CFGrammar Build(string input)
        {
            CFGrammar grammar = new();
            var productionsString = input.Split("\r\n").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if (productionsString.Count == 0)
                throw new System.Exception($"No production found ");

            //Exp=Niko + lodu + Exp
            int c = 0;
            foreach (var pd in productionsString)
            {

                var parts = pd.Split("=").Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
                if (parts.Length != 2)
                {
                    throw new System.Exception($"Invalid production {pd}");
                }
                List<Symbol> right = new();
                foreach (var s in parts[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)))
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

            return grammar;
        }
    }
}