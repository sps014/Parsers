use std::collections::HashMap;
use crate::grammar::symbol::Symbol;
use crate::grammar::symbol_type::SymbolType;
use crate::grammar::production::Production;

#[derive(Debug,Clone)]
pub struct PrefixTrie
{
    pub active:bool,
    pub children:HashMap<Symbol,Box<PrefixTrie>>,
}

impl PrefixTrie {
    pub fn new()->Self
    {
        PrefixTrie{active:true,children:HashMap::new()}
    }
    pub fn add(&mut self, child:Symbol) -> &mut PrefixTrie
    {
        if !self.children.contains_key(&child)
        {
            let mut c=PrefixTrie::new();
            self.children.insert(child.clone(),Box::new(c.clone()));
        }
        self.children.get_mut(&child).unwrap().as_mut()
    }
    pub fn add_production(&mut self,production:Production)
    {
        let mut current=self;
        for i in production.right.iter()
        {
            current=current.add(i.clone());
        }
    }

}