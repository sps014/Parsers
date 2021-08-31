using ParserLib.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserLib.Generator;

public interface IParserGenerator
{
    Cfg Grammar { get; }
    string Generate();
}
