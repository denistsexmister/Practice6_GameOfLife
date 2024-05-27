using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using System.Threading;

namespace Practice6_GameOfLife
{
    public class MainController
    {
        private readonly double width;
        private readonly double height;
        private readonly double cell_size;

        private readonly GameOfLifeEngine engine;
        private Thread simulationThread; 

        private readonly StackPanel field;

        public StackPanel Field
        {
            get
            {
                return field;
            }
        }

        public MainController(double width, double height, double cell_size)
        {
            this.width = width;
            this.height = height;
            this.cell_size = cell_size;

            engine = new GameOfLifeEngine((int)(height / cell_size),
                (int)(width / cell_size),
                1000,
                new StandardLifeAndSurvivalRules(),
                new StandardNeighborsCountingRules());


            field = new StackPanel();


            for (int i = 0; i < (height / cell_size); i++)
            {
                StackPanel row = new StackPanel()
                {
                    Name = i.ToString(),
                    Orientation = Orientation.Horizontal
                };

                for (int j = 0; j < (width / cell_size); j++)
                {
                    SolidColorBrush backgroundColor = new SolidColorBrush();

                    backgroundColor.Color = (engine.Field[i][j]) ?
                        Windows.UI.Color.FromArgb(255, 0, 0, 0) :
                        Windows.UI.Color.FromArgb(255, 255, 255, 255);

                    row.Children.Add(new Button()
                    {
                        Name = j.ToString(),
                        Width = cell_size,
                        Height = cell_size,
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush() { Color = Windows.UI.Color.FromArgb(1, 0, 0, 0) },
                        Background = backgroundColor
                    });
                }

                field.Children.Add(row);
            }
        }

        public void PlayOrStopSimulation()
        {
            if (engine.IsSimulationRun)
            {
                engine.IsSimulationRun = false;
            }
            else
            {
                simulationThread = new Thread(RunSimulationForward);
            }
        }

        private void RunSimulationForward()
        {
            engine.IsSimulationRun = true;
            while (engine.IsSimulationRun)
            {
                engine.MakeStepForward();
                MapFieldToScreenField();
                Thread.Sleep(engine.TimeBetweenSteps);
            }
        }

        public void MakeStepForward()
        {
            engine.MakeStepForward();
        }

        public void MapFieldToScreenField()
        {
            for (int i = 0; i < engine.Field.Length; i++)
            {
                StackPanel row = field.Children.ElementAt(i) as StackPanel;
                for (int j = 0; j < engine.Field[i].Length; j++)
                {
                    Button button = row.Children.ElementAt(j) as Button;

                    SolidColorBrush backgroundColor = new SolidColorBrush();

                    backgroundColor.Color = (engine.Field[i][j]) ?
                        Windows.UI.Color.FromArgb(255, 0, 0, 0) :
                        Windows.UI.Color.FromArgb(255, 255, 255, 255);

                    button.Background = backgroundColor;
                }
            }
        }
    }
}
