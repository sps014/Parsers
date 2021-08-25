extern  crate ParserLib;

use ParserLib::grammar::production::Production;
use ParserLib::grammar::context_free_grammar::CFG;
use ParserLib::grammar::symbol::Symbol;
use ParserLib::grammar::symbol_type::SymbolType;

fn main() {
    let c=Symbol::terminal_with("v".to_string());
    let l=Symbol::non_terminal_with("E".to_string());
    let p=Production::new(l,vec![c,Symbol::new("g".to_string(),SymbolType::NonTerminal)]);
    p.print();
    let mut cfg=CFG::new("A".to_string());
    cfg.add_production(p);
    println!("{:?}",cfg);
    cfg.eliminate_left_factoring()

}
