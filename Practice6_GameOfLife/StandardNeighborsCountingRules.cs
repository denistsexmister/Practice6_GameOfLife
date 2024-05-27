using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice6_GameOfLife
{
    public class StandardNeighborsCountingRules : INeighborsCountingRules
    {
        public int CountLiveNeighborsOfCell(bool[][] field, int i, int j)
        {
            int liveCells = 0;
            int field_height = field.Length;
            int field_width = field[i].Length;


            for (int k = -1; k <= 1; k++)
            {
                for (int l = -1; l <= 1; l++)
                {
                    if (k == 0 && l == 0) continue;
                    if ((i + k) < 0 || (i + k) >= field_height
                        || (j + l) < 0 || (j + l) >= field_width) continue;

                    liveCells += field[i + k][j + l] ? 1 : 0;
                }
            }

            return liveCells;
        }
    }
}
