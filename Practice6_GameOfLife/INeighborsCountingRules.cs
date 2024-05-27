using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice6_GameOfLife
{
    public interface INeighborsCountingRules
    {
        int CountLiveNeighborsOfCell(bool[][] field, int i, int j);
    }
}
