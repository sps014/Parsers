using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;
using System;


var input = @"Exp:= int | esp
Exp2:=int * Exp | esp";
//K->ε


var grammer = GrammarBuilder.Build(input);

WriteLine("Productions are:");
grammer.PrintProductions();

// grammer.StartSymbol = new("E", SymbolType.Start | SymbolType.NonTerminal);
WriteLine();
LL1 lL1 = new(grammer);
if (lL1.CreateParseTable())
{
    WriteLine("Parse Table is :");
    lL1.PrintParseTable();
    if (lL1.StackImpl(GrammarBuilder.BuildTerminals("int")))
    {
        WriteLine("\nParse Tree is :");
        lL1.Tree.Print();
    }
}
WriteLine();