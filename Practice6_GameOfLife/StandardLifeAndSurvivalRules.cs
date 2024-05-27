using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice6_GameOfLife
{
    public class StandardLifeAndSurvivalRules : ILifeAndSurvivalRules
    {
        public bool WillCellBeAlive(bool cellStatusBefore, int liveNeighbors)
        {
            if (cellStatusBefore && (liveNeighbors < 2 || liveNeighbors > 3))
            {
                return false;
            }
            else if (!cellStatusBefore && liveNeighbors == 3)
            {
                return true;
            }

            return cellStatusBefore;
        }
    }
}
