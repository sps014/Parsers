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
        public enum CellValue
        {
            Undefined,
            Larger,
            Smaller,
            Accepted
        }
    }
}
