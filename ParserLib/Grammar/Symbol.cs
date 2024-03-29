﻿using System;
using ParserLib.Grammar;

namespace ParserLib.Grammar;

public readonly struct Symbol:IEquatable<Symbol>
{
    public string Value { get; init; }
    public SymbolType Kind { get;init; }

    public Symbol(string value,SymbolType type=SymbolType.Terminal)
    {
        Value = value;
        Kind = type;
    }

    public bool Equals(Symbol other)
    {
        return Value == other.Value && Kind == other.Kind;
    }

    public override bool Equals(object? obj)
    {
        return obj is Symbol other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, (int)Kind);
    }

    public override string ToString()
    {
        return Value;
    }

    public void Print()
    {
        Console.WriteLine(ToString());
    }
    public static bool operator !=(Symbol lhs, Symbol rhs)
    {
        return !Equals(lhs, rhs);
    }
    public static bool operator ==(Symbol lhs, Symbol rhs)
    {
        return Equals(lhs, rhs);
    }

    public static Symbol NonTerminal(string value)
    {
        return new(value, SymbolType.NonTerminal);
    }
    public static Symbol Terminal(string value)
    {
        return new(value, SymbolType.Terminal);
    }
}
public static class Symbols
{
    public static readonly Symbol EPSILON = new Symbol("ε");
}