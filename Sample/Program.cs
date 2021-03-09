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

Production production2 = new("A");

production2.Right = new()
{
    new TerminalSymbol("m"),
    new NonTerminalSymbol("X"),
    new TerminalSymbol("+"),
    new TerminalSymbol("c")
};

table.Add(production2);

foreach (var values in table)
{
    foreach (var prod in values)
    {
        WriteLine(prod.ToString());
    }
}

