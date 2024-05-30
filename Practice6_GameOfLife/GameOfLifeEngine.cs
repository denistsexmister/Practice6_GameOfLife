using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace Practice6_GameOfLife
{
    public class GameOfLifeEngine
    {
        private readonly int FIELD_WIDTH;
        private readonly int FIELD_HEIGHT;

        private bool isSimulationRun;

        private readonly ILifeAndSurvivalRules lifeAndSurvivalRules;
        private readonly INeighborsCountingRules neighborsCountingRules;

        private bool[][] currentStep;


        public GameOfLifeEngine(int FIELD_HEIGHT, int FIELD_WIDTH,
            ILifeAndSurvivalRules lifeAndSurvivalRules,
            INeighborsCountingRules neighborsCountingRules)
        {
            this.FIELD_HEIGHT = FIELD_HEIGHT;
            this.FIELD_WIDTH = FIELD_WIDTH;

            this.lifeAndSurvivalRules = lifeAndSurvivalRules;
            this.neighborsCountingRules = neighborsCountingRules;

            currentStep = new bool[FIELD_HEIGHT][];
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                currentStep[i] = new bool[FIELD_WIDTH];
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    currentStep[i][j] = false;
                }
            }
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
   

        public void MakeStepForward()
        {
            bool[][] nextStep = new bool[FIELD_HEIGHT][];
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                nextStep[i] = new bool[FIELD_WIDTH];
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    int liveNeighbors = CountLiveNeighborsOfCell(i, j);

                    nextStep[i][j] = WillCellBeAlive(currentStep[i][j], liveNeighbors);
                }
            }

            CopyFieldToField(nextStep, currentStep);
        }

        public async Task GetFieldFromFile(string filename)
        {
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

            StorageFolder savesFolder = await installedLocation.GetFolderAsync("SavedFields");

            StorageFile file = await savesFolder.GetFileAsync(filename);

            string content = await FileIO.ReadTextAsync(file);
            string[] rows = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                string[] cols = rows[i].Split(',');
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    currentStep[i][j] = bool.Parse(cols[j]);
                }
            }
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
