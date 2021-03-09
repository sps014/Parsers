
namespace Parsers.Grammer
{
    public class Production
    {
        public ProductionType Type { get; set; }
        public string LHS { get; set; }
        public string RHS { get; set; }
    }
    public enum ProductionType
    {
        Start,
        Default
    }
}
