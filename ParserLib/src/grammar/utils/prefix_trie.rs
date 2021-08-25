use std::collections::HashMap;
use crate::grammar::symbol::Symbol;
use crate::grammar::symbol_type::SymbolType;

#[derive(Debug,Clone)]
pub struct PrefixTrie
{
    pub active:bool,
    pub children:HashMap<Symbol,Box<PrefixTrie>>
}

impl PrefixTrie {
    pub fn new()->Self
    {
        PrefixTrie{active:true,children:HashMap::new()}
    }
    pub fn add(&mut self, symbol:Symbol, child:Symbol) -> &PrefixTrie
    {
        if !self.children.contains_key(&symbol)
        {
            let mut c=PrefixTrie::new();
            self.children.insert(child.clone(),Box::new(c.clone()));
        }

        return  self.children.get(&symbol).unwrap().as_ref();
    }

}