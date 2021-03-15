using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Parsers.Grammar
{
    /// <summary>
    /// Represent a symbol in BNF
    /// </summary>
    public readonly struct Symbol : IEqualityComparer<Symbol>, IEquatable<Symbol>
    {
        /// <summary>
        /// Symbol Value ie a , A ,Expr
        /// </summary>
        public readonly string Value { get; init; }
        /// <summary>
        /// Symbol Type ie Terminal or Non terminal or start
        /// </summary>
        public readonly SymbolType Type { get; init; }

        /// <summary>
        /// Represent a basic Symbol in BNF
        /// </summary>
        /// <param name="symbolName"></param>
        /// <param name="symbolType"></param>
        public Symbol([DisallowNull] string symbolName, SymbolType symbolType = SymbolType.Terminal)
        {
            Value = symbolName;
            Type = symbolType;
        }
        public override string ToString()
        {
            return Value;
        }

        public bool Equals(Symbol x, Symbol y)
        {
            return x.Value == y.Value && y.Type == x.Type;
        }

        public int GetHashCode([DisallowNull] Symbol obj)
        {
            return HashCode.Combine(obj.Value.GetHashCode(), obj.Type.GetHashCode());
        }
        public static bool operator !=(Symbol obj1, Symbol obj2)
        {
            return !obj1.Equals(obj2);
        }
        public static bool operator ==(Symbol obj1, Symbol obj2)
        {
            return obj1.Equals(obj2);
        }

        public bool Equals(Symbol other)
        {
            return Equals(this, other);
        }
    }

    /// <summary>
    /// Collection of Built in Symbols for BNF
    /// </summary>
    public static class Symbols
    {
        /// <summary>
        /// Represents an ε Symbol
        /// </summary>
        public static Symbol EPSILON { get; } = new("ε");
        public static Symbol DOLLAR { get; } = new("$");

    }
    [Flags]
    public enum SymbolType
    {
        Default,
        Terminal,
        NonTerminal,
        Start,
    }
}