using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;
using System;


var input = @"Exp:= int + Exp2 | int
Exp2:=int * Exp | int";
//K->ε


var grammer = GrammerBuilder.Build(input);

grammer.PrintProductions();

// grammer.StartSymbol = new("E", SymbolType.Start | SymbolType.NonTerminal);

LL1 lL1 = new(grammer);
if (lL1.CreateParseTable())
{
    lL1.PrintParseTable();
    if (lL1.StackImpl(GrammerBuilder.BuildTerminals("int + int * int")))
    {
        lL1.Tree.Print();
    }
}
WriteLine();