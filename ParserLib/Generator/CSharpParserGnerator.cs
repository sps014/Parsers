using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserLib.Grammar;

namespace ParserLib.Generator;

public class CSharpParserGnerator : ParserGenerator
{
    public CSharpParserGnerator(Cfg grammar) : base(grammar)
    {
    }

    public override string Generate()
    {
        StringBuilder sb = new StringBuilder("");
        foreach(var k in Grammar.NonTerminals)
        {
            int c = 0;
            var epslion = new Production(k, new List<Symbol> { Symbols.EPSILON });

            foreach (var p in Grammar[k])
            {
                if (p == epslion)
                    continue;
                ProcessProduction(p, sb,c==0);
                c++;
            }
            if(Grammar[k].Contains(epslion))
            {
                //generate else part if only one production generate empty function
            }
        }
        return sb.ToString();
    }
    private void ProcessProduction(Production p,StringBuilder sb,bool first=false)
    {
        GenerateNestedProduction(p.Right,0,sb,0,first);
    }
    private void GenerateNestedProduction(List<Symbol> right, int pos,StringBuilder sb,int indent=0,bool first=false)
    {
        if (pos >= right.Count)
            return;

        var cur=right[pos];
        if (cur == Symbols.EPSILON)
        {
            GenerateNestedProduction(right, pos + 1, sb);
            return;
        }
        if (cur.Kind == SymbolType.Terminal)
        {
            sb.Append($"{Indentation(indent)}");
            sb.Append(pos == 0 && !first ? "else if" : "if");
            sb.Append($"(Current()==\"{cur.Value}\")\r\n");
            sb.Append($"{Indentation(indent)}{{\r\n");
            sb.Append($"{Indentation(indent + 1)}Next();\r\n");
            GenerateNestedProduction(right, pos + 1, sb,indent+1);
            sb.Append($"{Indentation(indent)}}}\r\n");
        }
        else
        {
            sb.Append($"{Indentation(indent)}{FunctionName(cur)}();\r\n");
            GenerateNestedProduction(right, pos + 1, sb,indent);
        }
    }
}