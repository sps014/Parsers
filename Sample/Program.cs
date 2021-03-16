using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;
using System;


var input = @"Exp= ( Abc ) | esp | id | j
Abc= id";
//K->ε


var grammer = GrammerBuilder.Build(input);



foreach (var values in grammer)
{
    foreach (var prod in values)
    {
        WriteLine(prod.ToString());
    }
}

// grammer.StartSymbol = new("E", SymbolType.Start | SymbolType.NonTerminal);

 LL1 lL1 = new(grammer);
if (lL1.CreateParseTable())
 {
     lL1.PrintParseTable();
     if(lL1.StackImpl(new List<Symbol> { new Symbol("("), new Symbol("id"), new Symbol(")") }))
     {
         lL1.Tree.Print();
     }
 }
//var vv = lL1.Follow(new("B", SymbolType.NonTerminal));
WriteLine();