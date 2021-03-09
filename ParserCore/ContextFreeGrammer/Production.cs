
using System.Collections.Generic;
using System.Linq;

namespace Parsers.Grammer
{
    /// <summary>
    ///  Indicates a production in BNF eg. A->id*A .
    /// alternate production should be created as separate instance aka A-> eps | aAa |bBA
    ///should be A-> epsilon   , A-> aAa , A-> bBA
    /// </summary>
    public class Production
    {
        ///Indicate Type of production ie. is it start or any other
        public  ProductionType Type { get; set; } = ProductionType.Intermidiate;
        
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
