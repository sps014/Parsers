use crate::grammar::symbol::Symbol;
use std::collections::{HashMap, HashSet};
use std::iter::FromIterator;
use crate::grammar::production::Production;

#[derive(Debug)]
pub struct CFG
{
    start:Symbol,
    nt_map:HashMap<Symbol,HashSet<Production>> //non terminal to productions map
}
impl CFG
{
    pub fn new(start:String)->Self
    {
        let mut map:HashMap<Symbol,HashSet<Production>>=HashMap::new();
        CFG{start:Symbol::non_terminal_with(start),nt_map:map}
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
    pub fn add_production(&mut self,production:Production)
    {
        let prod=production.clone();
        if !self.nt_map.contains_key(&production.left)
        {
            self.nt_map.insert(production.left,HashSet::new());
        }
        self.nt_map.get_mut(&prod.left).unwrap().insert(prod);

    }
}