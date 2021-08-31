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
        throw new NotImplementedException();
    }
}