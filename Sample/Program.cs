using static System.Console;
using Parsers.ShiftReduce;
using Parsers.Grammer;
using System.Collections.Generic;

OperatorPrecedenceParser precedence = new();


Production production = new("A");
production.Right = new()
{
    new TerminalSymbol("id^"),
    new NonTerminalSymbol("B"),
    new TerminalSymbol("+"),
    TerminalSymbol.EPSILON
};

WriteLine(production.ToString());

production = new("A");
production.Right = new()
{
    new TerminalSymbol("m"),
    new NonTerminalSymbol("X"),
    new TerminalSymbol("+"),
    new TerminalSymbol("c")
};
WriteLine(production.ToString());
