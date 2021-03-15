// using System;
// using System.Collections.Generic;
// using System.ComponentModel.Design.Serialization;
// using System.Diagnostics.CodeAnalysis;
// using System.Linq;
// using Parsers.Grammar;

// namespace Parsers.TopDown
// {
//     public class RecursiveDescentParser
//     {
//         public RecursiveDescentParser([NotNull] ProductionTable table)
//         {
//             Table = table;
//         }

//         public ProductionTable Table { get; }

//         public SyntaxTree Parse([NotNull] List<Symbol> input)
//         {
//             var tree = new SyntaxTree();
//             tree.Build(input, Table);
//             tree.Print(null);
//             return tree;
//         }

//     }
//     public class SyntaxTree
//     {
//         public SyntaxNode Root { get; private set; }
//         private List<Symbol> input;
//         private ProductionTable table;
//         public void Build(List<Symbol> input, ProductionTable table)
//         {
//             this.input = input;
//             this.table = table;
//             int pos = 0;
//             Match(table.StartSymbol, new SyntaxNode() { Value = table.StartSymbol }, ref pos);

//         }
//         public void Print(SyntaxNode node, int sp = 4)
//         {
//             node ??= Root;
//             Console.WriteLine("".PadRight(sp, '-') + node.Value.Value);
//             foreach (var v in node.Children)
//             {
//                 Print(v, sp + 4);
//             }

//         }
//         private bool Match(Symbol current, SyntaxNode parent, ref int pos, int oldPos = 0)
//         {
//             if (Root == null)
//                 Root = parent;

//             if (pos >= input.Count)
//             {
//                 return true;
//             }

//             foreach (var o in table[current.Value])
//             {
//                 Console.WriteLine($"Exploring Production {o}");

//                 foreach (var (sym, i) in o.Right.Select((i, n) => (i, n)))
//                 {
//                     var nn = new SyntaxNode() { Value = sym };
//                     parent.Children.Add(nn);
//                     if (sym.Type == SymbolType.NonTerminal)
//                     {
//                         Console.WriteLine($"Exploring Symbol {sym.Value}");

//                         if (!Match(sym, nn, ref pos, oldPos))
//                         {
//                             parent.Children.RemoveAt(parent.Children.Count - 1);
//                             pos = oldPos;
//                             break;
//                         }
//                     }
//                     else
//                     {
//                         if (pos < input.Count && input[pos].Value == sym.Value)
//                         {
//                             pos++;
//                             Console.WriteLine($"Matched Terminal {sym.Value}  result={string.Join("", input.Select(x => x.Value).Take(pos))}");
//                         }
//                         else if (pos < input.Count && input[pos].Value != sym.Value)
//                         {
//                             pos = oldPos;
//                             return false;
//                         }
//                     }
//                 }
//             }
//             return true;
//         }
//     }
// }