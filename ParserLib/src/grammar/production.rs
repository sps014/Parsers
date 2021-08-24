use crate::grammar::symbol::Symbol;

#[derive(Debug,Clone,Eq, PartialEq)]
pub struct Production
{
    pub left:Symbol,
    pub right:Vec<Symbol>
}
impl Production
{
    pub fn new(left:Symbol,right:Vec<Symbol>)->Self
    {
        Production{left,right}
    }
    pub fn from_str(left:String,right:Vec<Symbol>)->Self
    {
        Production{left:Symbol::non_terminal_with(left),right}
    }
    pub fn print(&self)
    {
        print!("{} ->",self.left.value);
        for i in self.right.iter()
        {
            print!(" {}", i.value);
        }
        print!("\n");
    }
}