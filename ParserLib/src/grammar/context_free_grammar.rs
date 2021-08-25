use crate::grammar::symbol::Symbol;
use std::collections::{HashMap, HashSet};
use std::iter::FromIterator;
use crate::grammar::production::Production;
use crate::grammar::utils::prefix_trie::PrefixTrie;

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
    pub fn eliminate_left_factoring(&mut self)
    {
        for (k,v) in self.nt_map.clone()
        {
            self.find_common(k.clone());
        }
    }
    fn find_common(&mut self,symbol:Symbol)->Vec<Symbol>
    {
        //let mut common=Vec::new();
        let mut pt=PrefixTrie::new();
        for i in self.nt_map.get(&symbol).unwrap().iter()
        {
            pt.add_production(i.clone());
        }
        print!("{:?}",pt);
        vec![]
    }
}