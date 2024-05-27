using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice6_GameOfLife
{
    public interface ILifeAndSurvivalRules
    {
        bool WillCellBeAlive(bool cellStatusBefore, int liveNeighbors);
    }
}
