using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserLib.Grammar;

namespace ParserLib.Generator;

public class CSharpParserGnerator : IParserGenerator
{
    public Cfg Grammar { get; }
    public CSharpParserGnerator(Cfg grammar)
    {
        Grammar = grammar;
    }

    public string Generate()
    {
        throw new NotImplementedException();
    }
}