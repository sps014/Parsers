#[derive(Clone,Debug,Eq,PartialEq)]
pub enum SymbolType
{
    Terminal,
    NonTerminal,
    Start
}