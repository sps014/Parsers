using ParserLib.Grammar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib.Generator;

public abstract class ParserGenerator
{
    public Cfg Grammar { get; }
    public virtual string FunctionName(Symbol nonTerminal)
    {
        return $"Parse{nonTerminal.Value}";
    }
    public virtual string Indentation(int level)
    {
        StringBuilder stringBuilder= new StringBuilder("");
        for (int i = 0; i < level; i++)
        {
            stringBuilder.Append('\t');
        }
        return stringBuilder.ToString();
    }
    public ParserGenerator(Cfg grammar)
    {
        Grammar = grammar;
        if (Grammar.Start.Value == null)
            throw new Exception("Grammar doesnot contain a start production");
    }
    public abstract string Generate();
}
