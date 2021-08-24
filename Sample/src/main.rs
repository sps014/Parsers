extern  crate ParserLib;

use ParserLib::grammar::production::Production;
use ParserLib::grammar::context_free_grammar::CFG;

fn main() {
    let c=ParserLib::grammar::symbol::Symbol::terminal_with("v".to_string());
    let l=ParserLib::grammar::symbol::Symbol::non_terminal_with("E".to_string());
    let p=Production::new(l,vec![c]);
    p.print();
    let mut cfg=CFG::new("A".to_string());
    cfg.add_production(p);
    print!("{:?}",cfg);
}
