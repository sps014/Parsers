using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;

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


var input = @"T->E+T
T->F
E->F*F
F->i";

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

table.StartSymbol = new("T");
//A-> return Statement;



RecursiveDescentParser parser = new(table);
parser.Parse(new() { new("i"), new("*"), new("i"), new("+"), new("i") });
/*
                    T
             /     |     \
             E     +      T
           / | \          |
          F  *  F         F
          |     |         |
          i    id         id
*/