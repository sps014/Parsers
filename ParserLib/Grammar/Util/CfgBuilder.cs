using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib.Grammar.Util;
public class CfgBuilder
{
    private string[] Grammar;
    private int pos = 0;
    public Cfg Build(string grammar)
    {
        pos = 0;
        Grammar= grammar.Split(" ").Where(x=>!string.IsNullOrWhiteSpace(x)).ToArray();

        Cfg lang=new Cfg();
        while (pos<=Grammar.Length)
        {
            foreach (var p in ParseRule())
            {
                lang.AddProduction(p);
            }
        }
        return lang;
    }
    private List<Production> ParseRule()
    {
        var left = ParseLeft();
        List<Production> result = new List<Production>();
        while(GetCurrent()!=null)
        {
            result.Add(new Production(left,ParseRight().Right));
        }
        return result;
    }
    private Symbol ParseLeft()
    {
        var l = GetNext();
        if (l == null)
            throw new Exception("Reached end without finding a valid Left hand side symbol of Grammar");
        if((l.StartsWith('\'') && l.EndsWith('\'')) || !char.IsLetter(l[0]))
            throw new Exception($"Expected a non terminal (starting with Alphabet) but found {l}");

        //consume -> symbol 
        var arrow = GetNext();
        if(arrow==null || arrow!="->")
            throw new Exception("Reached end without finding a valid arrow in grammar '->'");
        
        return Symbol.NonTerminal(l);
    }
    private Production ParseRight()
    {
        List<Symbol> result = new List<Symbol>();
        while (GetCurrent()!=null)
        {
            if (GetCurrent() == "\r\n" || GetCurrent() == "|")
            {
                GetNext();
                break;
            }
            var l=GetNext();
            if ((l.StartsWith('\'') && l.EndsWith('\'')))
            {
                result.Add(Symbol.Terminal(l.Remove(l.Length - 1).Remove(0,1)));
            }
            else
                result.Add(Symbol.NonTerminal(l));

        }
        if(result.Count == 0)
            throw new Exception($"Expected terminals and non terminal found nothing but found");

        return new Production(string.Empty,result);
    }
    private string GetCurrent()
    {
        if (pos  < Grammar.Length)
        {
            return Grammar[pos];
        }
        return null;
    }
    private string GetNext()
    {
        if(pos+1<Grammar.Length)
        {
            return Grammar[pos++];
        }
        return null;
    }
}

