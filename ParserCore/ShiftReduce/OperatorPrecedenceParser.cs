using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using Parsers.Grammer;

namespace Parsers.ShiftReduce
{
    public class OperatorPrecedenceParser
    {
        private readonly ProductionTable table;
        public CellValue[,] OperatorTable { get; }
        public OperatorPrecedenceParser([NotNull] ProductionTable table)
        {
            this.table = table;
            int tc = table.Terminals.Count;// includes $
            OperatorTable = new CellValue[tc, tc];
        }


        public void FillPrecedenceTable()
        {
            var terminals = table.Terminals.ToList();
            int tc = OperatorTable.GetLength(0);

            var prec = new Dictionary<string, uint>
            {
                ["*"] = 2,
                ["/"] = 2,
                ["+"] = 3,
                ["-"] = 3,
                ["%"] = 4,
                ["$"] = 5
            };

            for (int i = 0; i < tc; i++)
            {
                for (int j = 0; j < tc; j++)
                {
                    if (i == 0 && j == 0)
                        OperatorTable[i, j] = CellValue.Accepted;
                    else
                    {
                        if (prec.ContainsKey(terminals[i]) && prec.ContainsKey(terminals[j]))
                        {
                            if (prec[terminals[i]] > prec[terminals[j]])
                                OperatorTable[i, j] = CellValue.Smaller;
                            else
                                OperatorTable[i, j] = CellValue.Larger;
                        }

                        else if (prec.ContainsKey(terminals[i]) && !prec.ContainsKey(terminals[j]))
                            OperatorTable[i, j] = CellValue.Smaller;

                        else if (!prec.ContainsKey(terminals[i]) && prec.ContainsKey(terminals[j]))
                            OperatorTable[i, j] = CellValue.Larger;

                    }
                }
            }
        }
        public void PrintTable()
        {
            var terminals = table.Terminals.ToList();
            int tc = OperatorTable.GetLength(0);

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("".PadRight(20));
            for (int i = 0; i < tc; i++)
            {
                Console.Write(terminals[i].ToString().PadRight(20));
            }
            Console.WriteLine();

            for (int i = 0; i < tc; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(terminals[i].ToString().PadRight(20));

                Console.ResetColor();

                for (int j = 0; j < tc; j++)
                {
                    Console.Write(OperatorTable[i, j].ToString().PadRight(20));
                }
                Console.WriteLine();
            }
        }
        public void StackConstruction(string input)
        {
            input += "$";
            var stack = "$";
            int matchIndex = 0;
            var terminals = table.Terminals.ToList();

            Console.WriteLine("\n");
            Console.WriteLine("stack".PadRight(30) + "input".PadRight(30) + "\t");

            while (input.Length > 0 && stack.Length > 0)
            {
                matchIndex = stack.Length - 1;

                while (char.IsUpper(stack[matchIndex]))
                {
                    matchIndex--;
                }

                if (input[0] == '$' && stack[matchIndex] == '$')
                {
                    Console.WriteLine("Accepted");
                    break;
                }

                Console.Write(stack.PadRight(30) + input.PadRight(30) + "\t");

                int indI = terminals.IndexOf(input[0].ToString());
                int indS = terminals.IndexOf(stack[matchIndex].ToString());

                if (OperatorTable[indI, indS] == CellValue.Undefined)
                {
                    Console.WriteLine("error");
                    break;
                }
                else if (OperatorTable[indI, indS] == CellValue.Larger)
                {
                    Console.WriteLine($"Shifted {input[0]} to stack.");
                    stack += input[0];
                    input = input[1..];
                }
                else
                {
                    bool reduced = false;
                    for (int i = 1; i < stack.Length; i++)
                    {
                        var str = string.Join("", stack[i..].Reverse());
                        if (table.ContainsProduction(str))
                        {
                            var p = table.InverseProduction(str);
                            p = string.Join("", p.Reverse());
                            stack = stack.Replace(stack[i..], p);
                            Console.WriteLine($"reduced: {str} to {p}");
                            reduced = true;
                            break;
                        }
                    }
                    if (!reduced)
                    {
                        Console.WriteLine("Error in reducing not valid grammer");
                        break;
                    }
                }
            }
        }
        public enum CellValue
        {
            Undefined,
            Larger,
            Smaller,
            Accepted
        }

    }
}
