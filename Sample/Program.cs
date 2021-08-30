
using System;
using System.Collections.Generic;
using ParserLib;
using ParserLib.Grammar;
using ParserLib.Grammar.Util;

var grammar = 
    @"A -> 'b' '+' C 
";


var cfg =new CfgBuilder().Build(grammar);