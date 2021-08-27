
using System;
using System.Collections.Generic;
using ParserLib;
using ParserLib.Grammar;

var nt = Symbol.NonTerminal("K");
var p = new Production(nt, 
    new()
    {
        new Symbol("a"),nt,new Symbol("b")
    }
);
var p1 = new Production(nt, 
    new()
    {
        new Symbol("a"),nt,new Symbol("c")
    }
);
var p2 = new Production(nt, 
    new()
    {
        new Symbol("b"),nt,new Symbol("c")
    }
);
var p3 = new Production(nt, 
    new()
    {
        new Symbol("c"),nt,new Symbol("d")
    }
);
var p4 = new Production(nt, 
    new()
    {
        new Symbol("c"),nt,new Symbol("e")
    }
);
var cfg=new Cfg();

cfg.AddProduction(p);
cfg.AddProduction(p1);
cfg.AddProduction(p2);
cfg.AddProduction(p3);
cfg.AddProduction(p4);

cfg.EliminateLeftFactoring();
cfg.Print();