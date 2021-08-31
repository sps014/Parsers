
using System;
using System.Collections.Generic;
using ParserLib;
using ParserLib.Grammar;
using ParserLib.Grammar.Util;

var grammar = 
    @"A -> 'b' '+' C | 'c'
 B -> 'a' '+' M
";


var cfg =CfgBuilder.Build(grammar);