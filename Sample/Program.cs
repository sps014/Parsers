// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using ParserLib;
using ParserLib.Grammar;

var c=new Symbol("a");
var nt = Symbol.NonTerminal("K");
var p = new Production(nt, new List<Symbol>() {c,nt});
Console.WriteLine(p.ToString());
var cfg=new Cfg();
cfg.AddProduction(p);
cfg.EliminateLeftFactoring();