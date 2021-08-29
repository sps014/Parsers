using System;
using System.Collections.Generic;
using System.Linq;
using ParserLib.Grammar;

namespace ParserLib.Grammar;

public readonly struct Production:IEquatable<Production>
{
    public Symbol Left { get; init; }
    public List<Symbol> Right { get; init; }

    public Production(string left, List<Symbol> right)
    {
        Left = Symbol.NonTerminal(left);
        Right= right;
    }
    public Production(Symbol left, List<Symbol> right)
    {
        Left = left;
        Right = right;
    }

    public void Print()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        return $"{Left.Value} -> {string.Join(" ", Right.Select((x => x.Value)))}";
    }

    public bool Equals(Production other)
    {
        return Left.Equals(other.Left) && Right.Equals(other.Right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Production other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Left, Right);
    }
}