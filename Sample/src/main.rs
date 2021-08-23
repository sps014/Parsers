extern  crate ParserLib;

fn main() {
    let c=ParserLib::grammar::symbol::Symbol::terminal_with("ok".to_string());
    println!("{:?}",c);
}
