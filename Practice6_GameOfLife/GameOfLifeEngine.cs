using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practice6_GameOfLife
{
    public class GameOfLifeEngine
    {
        private readonly int FIELD_WIDTH;
        private readonly int FIELD_HEIGHT;

        private int timeBetweenSteps;
        private bool isSimulationRun;

        private readonly ILifeAndSurvivalRules lifeAndSurvivalRules;
        private readonly INeighborsCountingRules neighborsCountingRules;

        private bool[][] currentStep;
        private bool[][] nextStep;


        public GameOfLifeEngine(int FIELD_HEIGHT, int FIELD_WIDTH, int timeBetweenSteps,
            ILifeAndSurvivalRules lifeAndSurvivalRules,
            INeighborsCountingRules neighborsCountingRules)
        {
            this.FIELD_HEIGHT = FIELD_HEIGHT;
            this.FIELD_WIDTH = FIELD_WIDTH;

            this.timeBetweenSteps = timeBetweenSteps;


            this.lifeAndSurvivalRules = lifeAndSurvivalRules;
            this.neighborsCountingRules = neighborsCountingRules;

            currentStep = new bool[FIELD_HEIGHT][];
            nextStep = new bool[FIELD_HEIGHT][];
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                currentStep[i] = new bool[FIELD_WIDTH];
                nextStep[i] = new bool[FIELD_WIDTH];
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    currentStep[i][j] = false;
                    nextStep[i][j] = false;
                }
            }
            //temp
            currentStep[0][1] = true;
            currentStep[1][2] = true;
            currentStep[2][0] = true;
            currentStep[2][1] = true;
            currentStep[2][2] = true;
            //temp
        }

        public bool[][] Field
        {
            get
            {
                return currentStep;
            }
        }

        public bool IsSimulationRun
        {
            get
            {
                return isSimulationRun;
            }
            set
            {
                isSimulationRun = value;
            }
        }

        public int TimeBetweenSteps
        {
            get
            {
                return timeBetweenSteps;
            }
        }        

        public void MakeStepForward()
        {
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    int liveNeighbors = CountLiveNeighborsOfCell(i, j);

                    nextStep[i][j] = WillCellBeAlive(currentStep[i][j], liveNeighbors);
                }
            }

            CopyFieldToField(nextStep, currentStep);
        }
        private void CopyFieldToField(bool[][] firstField, bool[][] secondField)
        {
            for (int i = 0; i < firstField.Length; i++)
            {
                for (int j = 0; j < firstField[i].Length; j++)
                {
                    secondField[i][j] = firstField[i][j];
                }
            }
        }

        private bool WillCellBeAlive(bool cellStatusBefore, int liveNeighbors)
        {
            return lifeAndSurvivalRules.WillCellBeAlive(cellStatusBefore, liveNeighbors);
        }

        private int CountLiveNeighborsOfCell(int i, int j)
        {
            return neighborsCountingRules.CountLiveNeighborsOfCell(currentStep, i, j);
        }
    }
}
