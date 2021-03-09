using static System.Console;
using Parsers.ShiftReduce;
using Parsers.Grammer;
using System.Collections.Generic;

OperatorPrecedenceParser precedence = new();

ProductionTable table = new();

Production production = new("A");
production.Right = new()
{
    new TerminalSymbol("id^"),
    new NonTerminalSymbol("B"),
    new TerminalSymbol("+"),
    TerminalSymbol.EPSILON
};

table.Add(production);

Production p2 = new("A");

p2.Right = new()
{
    new TerminalSymbol("m"),
    new NonTerminalSymbol("X"),
    new TerminalSymbol("+"),
    new TerminalSymbol("c")
};

table.Add(p2);

foreach (var v in table)
{
    foreach (var g in v)
    {
        WriteLine(g.ToString());
    }
}

