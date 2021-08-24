#[derive(Clone,Debug,Eq,PartialEq,Hash)]
pub enum SymbolType
{
    Terminal,
    NonTerminal,
    Start
}