using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                    if (i == tc - 1 && j == tc - 1)
                        OperatorTable[i, j] = CellValue.Accepted;
                    else
                    {
                        if (prec.ContainsKey(terminals[i]) && prec.ContainsKey(terminals[j]))
                        {
                            if (prec[terminals[i]] >= prec[terminals[j]])
                                OperatorTable[i, j] = CellValue.Larger;
                            else
                                OperatorTable[i, j] = CellValue.Smaller;
                        }

                        else if (prec.ContainsKey(terminals[i]) && !prec.ContainsKey(terminals[j]))
                            OperatorTable[i, j] = CellValue.Smaller;

                        else if (!prec.ContainsKey(terminals[i]) && prec.ContainsKey(terminals[j]))
                            OperatorTable[i, j] = CellValue.Larger;

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
