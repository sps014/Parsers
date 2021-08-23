use super::symbol_type::*;

#[derive( Clone, PartialEq,Debug)]
pub struct Symbol
{
    pub value:String,
    pub kind:SymbolType
}
impl Symbol
{
    pub fn new(value:String,kind:SymbolType)->Self
    {
        Symbol{value,kind}
    }
    pub fn terminal_with(value:String)->Self
    {
        Symbol{value,kind:SymbolType::Terminal }
    }
    pub fn non_terminal_with(value:String)->Self
    {
        Symbol{value,kind:SymbolType::NonTerminal }
    }
}