use crate::grammar::symbol::Symbol;
use std::collections::{HashMap, HashSet};
use crate::grammar::production::Production;

pub struct CFG
{
    start:Symbol,
    nt_map:HashMap<Symbol,HashSet<Production>> //non terminal to productions map
}
impl CFG
{
    pub fn new(start:String)->Self
    {
        CFG{start:Symbol::non_terminal_with(start),nt_map:HashMap::new()}
    }
    pub fn from_symbol( start:Symbol,nt_map:HashMap<Symbol,HashSet<Production>>)->Self
    {
        CFG{start,nt_map}
    }
    pub fn from( start:String,nt_map:HashMap<Symbol,HashSet<Production>>)->Self
    {
        CFG{start:Symbol::non_terminal_with(start),nt_map}
    }
    pub fn set_start(&mut self,start:String)
    {
        self.start=Symbol::non_terminal_with(start);
    }
}