using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;
using System;


var input = @"Exp= ( Abc ) | id | j
Abc= id";
//K->ε


var grammer = GrammerBuilder.Build(input);

grammer.PrintProductions();

// grammer.StartSymbol = new("E", SymbolType.Start | SymbolType.NonTerminal);

LL1 lL1 = new(grammer);
if (lL1.CreateParseTable())
{
    lL1.PrintParseTable();
    if (lL1.StackImpl(new List<Symbol> { new Symbol("("), new Symbol("id"), new Symbol(")") }))
    {
        lL1.Tree.Print();
    }
}
WriteLine();