using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;
using System;

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


var input = @"E->TG
G->+TG
T->FH
H->*FH
H->ε
F->()
F->i
G->ε";
//K->ε


foreach (var v in input.Split("\n"))
{
    var parts = v.Split("->");
    Production production1 = new(parts[0]);

    foreach (var s in parts[1])
    {
        if (s == '\r')
            continue;

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

table.StartSymbol = new("E", SymbolType.Start | SymbolType.NonTerminal);

LL1 lL1 = new(table);
if (lL1.CreateParseTable())
{
    lL1.PrintParseTable();
    if(lL1.StackImpl("i+i"))
    {
        lL1.Tree.Print();
    }
}
//var vv = lL1.Follow(new("B", SymbolType.NonTerminal));
WriteLine();