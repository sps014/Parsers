using System.Collections.Generic;

namespace Parsers.Grammar
{
    public class Symbol
    {
        public string SymbolName { get; init; }
        public SymbolType Type { get; init; }
        public Symbol(string symbolName, SymbolType symbolType = SymbolType.Terminal)
        {
            SymbolName = symbolName;
            Type = symbolType;
        }
    }
    public enum SymbolType
    {
        Default,
        Terminal,
        NonTerminal,
        Start,
    }
}