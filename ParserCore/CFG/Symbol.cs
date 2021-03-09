using System.Collections.Generic;

namespace Parsers.Grammer
{
    public abstract class Symbol
    {
        public string SymbolName { get; init; }
        public Symbol(string symbolName)
        {
            SymbolName = symbolName;
        }
    }
    /// <summary>
    /// This is defintion for terminal symbol
    /// </summary>
    public class TerminalSymbol : Symbol
    {
        public TerminalSymbol(string symbolName) : base(symbolName) { }
        public static TerminalSymbol EPSILON => new("ε");
    }
    public class NonTerminalSymbol : Symbol
    {
        public List<Production> Production { get; init; }
        public NonTerminalSymbol(string symbolName) : base(symbolName) { }
    }
}