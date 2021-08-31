using ParserLib.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib.Generator;

public abstract class ParserGenerator
{
    public Cfg Grammar { get; }
    public ParserGenerator(Cfg grammar)
    {
        Grammar = grammar;
        if (Grammar.Start.Value == null)
            throw new Exception("Grammar doesnot contain a start production");
    }
    public abstract string Generate();
}
