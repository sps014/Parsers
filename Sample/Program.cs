// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using ParserLib;
using ParserLib.Grammar;

var nt = Symbol.NonTerminal("K");
var p = new Production(nt, 
    new List<Symbol>()
    {
        new Symbol("a"),nt,new Symbol("*")
    }
);
var p1 = new Production(nt, 
    new List<Symbol>()
    {
        new Symbol("a"),nt,new Symbol("@")
    }
);
var p2 = new Production(nt, 
    new List<Symbol>()
    {
        new Symbol("G"),nt,new Symbol("@")
    }
);
var cfg=new Cfg();
cfg.AddProduction(p);
cfg.AddProduction(p1);
cfg.AddProduction(p2);
cfg.EliminateLeftFactoring();