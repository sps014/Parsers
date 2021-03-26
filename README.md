# Parsers
 
A parse tree generator.

```cs

using static System.Console;
using Parsers.Grammar;
using System.Collections.Generic;
using Parsers.TopDown;
using System;


var input = @"Exp:= int + Exp2 | int
Exp2:=int * Exp | int";


//create grammar from string
var grammer = GrammerBuilder.Build(input);

//display productions
grammer.PrintProductions();

//initialize LL1 parser
LL1 lL1 = new(grammer);

//if we can create parse table
if (lL1.CreateParseTable())
{
     // print table
    lL1.PrintParseTable();
    //stack implement for given input
    if (lL1.StackImpl(GrammerBuilder.BuildTerminals("int + int * int")))
    {
        // print syntax tree
        lL1.Tree.Print();
    }
}
```
