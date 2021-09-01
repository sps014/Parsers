
using System;
using System.Collections.Generic;
using ParserLib;
using ParserLib.Grammar;
using ParserLib.Grammar.Util;
using ParserLib.Generator;

var grammar = 
    @"A -> 'b' '+' C '-' Mon | 'c'
 C -> 'a' '+' | eps
";


var cfg =CfgBuilder.Build(grammar);
cfg.Print();
cfg.Start.Print();
var gen = new CSharpParserGnerator(cfg);
gen.Generate();