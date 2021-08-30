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
        var left = ParseRule();
    }
    private Symbol ParseLeft()
    {
        var l = GetCurrent();
        if (l == null)
            throw new Exception("Reached end at without finding a valid Left hand side of Grammar");
        if((l.StartsWith('\'') && l.EndsWith('\'')) || !char.IsLetter(l[0]))
            throw new Exception($"Expected a non terminal (starting with Alphabet) but found {l}");

        return Symbol.NonTerminal(l);
    }
    private string GetCurrent()
    {
        if(pos+1<Grammar.Length)
        {
            return Grammar[pos++];
        }
        return null;
    }
}

