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
            GenrateFunction(k,sb);
        }
        Console.WriteLine(sb.ToString());
        return sb.ToString();
    }
    
    private void GenrateFunction(Symbol s,StringBuilder sb)
    {
        sb.Append($"private void {FunctionName(s)}()\r\n");
        sb.Append("{\r\n");


        int c = 0;
        var epslion = new Production(s, new List<Symbol> { Symbols.EPSILON });
        var has_eps = Grammar[s].Count(x=>x.Right.Count==1&&x.Right[0]==Symbols.EPSILON)>0;

        foreach (var p in Grammar[s])
        {
            if (p == epslion)
                continue;
            ProcessProduction(p, sb, c == 0);
            c++;
        }
        HandleEpsilon(has_eps, sb);

        sb.Append("}\r\n");

    }
    private void ProcessProduction(Production p, StringBuilder sb, bool first = false)
    {
        GenerateNestedProduction(p.Right, 0, sb, 1, first);
    }
    private void HandleEpsilon(bool has_eps,StringBuilder sb)
    {
        if(has_eps)
        {
            return;
        }
        int indent = 1;
        sb.Append($"{Indentation(indent)}else\r\n");
        sb.Append($"{Indentation(indent)}{{\r\n");
        sb.Append($"{Indentation(indent + 1)}Unmatched();\r\n");
        sb.Append($"{Indentation(indent)}}}\r\n");
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
            sb.Append($"\r\n{Indentation(indent)}");
            sb.Append(pos == 0 && !first ? "else if" : "if");
            sb.Append($"(Current()==\"{cur.Value}\")\r\n");
            sb.Append($"{Indentation(indent)}{{\r\n");
            sb.Append($"{Indentation(indent + 1)}Next();\r\n");
            GenerateNestedProduction(right, pos + 1, sb,indent+1);
            sb.Append($"{Indentation(indent)}}}\r\n");
            if (pos == 0)
                return;
            sb.Append($"{Indentation(indent)}else\r\n");
            sb.Append($"{Indentation(indent)}{{\r\n");
            sb.Append($"{Indentation(indent+1)}Unmatched();\r\n");
            sb.Append($"{Indentation(indent)}}}\r\n");



        }
        else
        {
            sb.Append($"{Indentation(indent)}{FunctionName(cur)}();\r\n");
            GenerateNestedProduction(right, pos + 1, sb,indent);
        }
    }
}