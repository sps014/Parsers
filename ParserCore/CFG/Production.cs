
using System.Collections.Generic;
using System.Linq;

namespace Parsers.Grammer
{
    /// <summary>
    ///  A->idI+s
    ///  B->id*7A
    /// </summary>
    public class Production
    {
        public ProductionType Type { get; set; }
        public NonTerminalSymbol Left { get; set; }
        public List<Symbol> Right { get; set; }

        public override string ToString()
        {
            var symbols = Right.Select(x => x.SymbolName);
            return $"{Left.SymbolName}->{string.Join("", symbols)}";
        }
    }

    public enum ProductionType
    {
        Start,
        Intermidiate
    }
}
