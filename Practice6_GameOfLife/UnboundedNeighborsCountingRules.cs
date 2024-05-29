using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice6_GameOfLife
{
    public class UnboundedNeighborsCountingRules : INeighborsCountingRules
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
                    int height_cell = (i + k < 0) ? field_height - 1 : (i + k >= field_height) ? 0 : i + k;
                    int width_cell = (j + l < 0) ? field_width - 1 : (j + l >= field_width) ? 0 : j + l;

                    liveCells += field[height_cell][width_cell] ? 1 : 0;
                }
            }

            return liveCells;
        }
    }
}
