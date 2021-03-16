using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;
using System;

CFGrammer grammer = new();


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
    grammer.AddRule(production1);
}

foreach (var values in grammer)
{
    foreach (var prod in values)
    {
        WriteLine(prod.ToString());
    }
}

grammer.StartSymbol = new("E", SymbolType.Start | SymbolType.NonTerminal);

LL1 lL1 = new(grammer);
if (lL1.CreateParseTable())
{
    lL1.PrintParseTable();
    if (lL1.StackImpl(new List<Symbol> { new Symbol("i"), new Symbol("+"), new Symbol("i") }))
    {
        lL1.Tree.Print();
    }
}
//var vv = lL1.Follow(new("B", SymbolType.NonTerminal));
WriteLine();