using static System.Console;
using Parsers.ShiftReduce;
using Parsers.Grammer;
using System.Collections.Generic;


ProductionTable table = new();


// Production production = new("A");

// production.Right = new()
// {
//     new TerminalSymbol("id^"),
//     new NonTerminalSymbol("B"),
//     new TerminalSymbol("+"),
//     TerminalSymbol.EPSILON
// };

// table.Add(production);

// Production production2 = new("B");
// production2.Right = new()
// {
//     new TerminalSymbol("m"),
//     new NonTerminalSymbol("X"),
//     new TerminalSymbol("+"),
//     new TerminalSymbol("c")
// };

// table.Add(production2);


var input = @"A->B+C*D
C->i
D->j
B->k";

foreach (var v in input.Split("\n"))
{
    var parts = v.Split("->");
    Production production1 = new(parts[0]);
    foreach (var s in parts[1])
    {
        if (s == '\r')
            continue;

        production1.Right ??= new List<Symbol>();

        if (char.IsUpper(s))
        {
            production1.Right.Add(new Symbol(s.ToString(), SymbolType.NonTerminal));
        }
        else
        {
            production1.Right.Add(new Symbol(s.ToString()));
        }
    }
    table.Add(production1);
}

foreach (var values in table)
{
    foreach (var prod in values)
    {
        WriteLine(prod.ToString());
    }
}
//A-> return Statement;
string res = "";
DFS(table["A"][0], ref res);
WriteLine("\r\n\r\n" + res);

void DFS(Production p, ref string current)
{
    foreach (var v in p.Right)
    {
        if (v.Type == SymbolType.Terminal)
        {
            current += v.SymbolName;
        }
        else
        {
            DFS(table[v.SymbolName][0], ref current);
        }
    }
}

OperatorPrecedenceParser precedence = new(table);
precedence.FillPrecedenceTable();
precedence.PrintTable();
