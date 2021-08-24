extern  crate ParserLib;

use ParserLib::grammar::production::Production;

fn main() {
    let c=ParserLib::grammar::symbol::Symbol::terminal_with("v".to_string());
    let l=ParserLib::grammar::symbol::Symbol::non_terminal_with("E".to_string());
    let p=Production::new(l,vec![c]);
    p.print();
}
