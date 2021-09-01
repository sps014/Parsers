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
        if (!Left.Equals(other.Left)||Right.Count!=other.Right.Count)
            return false;

        for (int i = 0; i < Right.Count; i++)
        {
            if(Right[i]!=other.Right[i])
                return false;
        }
        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is Production other && Equals(other);
    }
    public static bool operator !=(Production lhs, Production rhs)
    {
        return !Equals(lhs, rhs);
    }
    public static bool operator ==(Production lhs, Production rhs)
    {
        return Equals(lhs, rhs);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Left, Right);
    }
}