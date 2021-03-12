using System.Collections.Generic;

namespace Parsers.Grammar
{
    /// <summary>
    /// Represent a symbol in BNF
    /// </summary>
    public readonly struct Symbol
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
        public Symbol(string symbolName, SymbolType symbolType = SymbolType.Terminal)
        {
            Value = symbolName;
            Type = symbolType;
        }
        public override string ToString()
        {
            return Value;
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
    }
    public enum SymbolType
    {
        Default,
        Terminal,
        NonTerminal,
        Start,
    }
}