# Parsers
 
A parse tree generator.\
CFG should be Left Recusion free and no left factoring should be there at all.

```cs

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
```

#### Output
![output](https://user-images.githubusercontent.com/45932883/117781074-db8bb180-b25d-11eb-8c6b-b47e1d9726a5.png)
