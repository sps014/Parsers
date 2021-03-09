
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
        public ProductionType Type { get; init; } = ProductionType.Intermidiate;
        public string Left { get; init; }
        public List<Symbol> Right { get; set; }
        public Production(string leftSymbolname)
        {
            Left = leftSymbolname;
        }
        public override string ToString()
        {
            var symbols = Right.Select(x => x.SymbolName);
            return $"{Left}->{string.Join("", symbols)}";
        }
    }

    public enum ProductionType
    {
        Start,
        Intermidiate
    }
}
