using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib.Grammar.Util;
public static class CfgBuilder
{
    public static Cfg Build(string grammar)
    {
        var rules= grammar.Split("\r\n").Where(x=>!string.IsNullOrWhiteSpace(x)).ToArray();

        Cfg lang=new Cfg();
        bool gotFirst = false;
        foreach(var l in rules)
        {
            foreach (var p in ParseRule(l))
            {
                if (!gotFirst)
                {
                    lang.Start = p.Left;
                    gotFirst = true;
                }

                lang.AddProduction(p);
            }
        }
        return lang;
    }
    private static List<Production> ParseRule(string line)
    {
        var pts = line.Split(" " ).Where(p=>!string.IsNullOrWhiteSpace(p)).ToArray();
        var result=new List<Production>();
        if (pts.Length == 0)
            throw new Exception($"expected one left non terminal in line: {line}");

        var left = pts[0];

        if (left.StartsWith('\'') && left.EndsWith('\''))
            throw new Exception($"expected one left non terminal got terminal '{left}' in line {line}: ");

        if (pts.Length<2 || pts[1]!="->")
            throw new Exception($"expected -> in line : {line}");

        List<Symbol> right = new();

        for (int i = 2; i < pts.Length; i++)
        {
            var p = pts[i];
            
            if (p.StartsWith('\'') && p.EndsWith('\''))
            {
                right.Add(Symbol.Terminal(p.Remove(p.Length - 1).Remove(0, 1)));
            }
            else if(p=="eps")
            {
                right.Add(Symbols.EPSILON);
            }
            else if(p!="|")
            {
                right.Add(Symbol.NonTerminal(p));
            }

            if (p == "|" || i == pts.Length - 1)
            {
                if (right.Count == 0)
                    continue;
                result.Add(new Production(left, new List<Symbol>(right)));
                right.Clear();
            }
            

        }

        return result;
    }
    
}

