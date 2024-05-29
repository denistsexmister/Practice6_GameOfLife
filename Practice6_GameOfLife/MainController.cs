using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using System.Threading;
using Windows.UI.Core;

namespace Practice6_GameOfLife
{
    public class MainController
    {
        private readonly double _width;
        private readonly double _height;
        private readonly double _cell_size;

        private readonly GameOfLifeEngine _engine;
        private readonly CoreDispatcher _dispatcher;
        private Task _simulationTask;

        private int _timeBetweenSteps;
        private double _timeModifier;

        private readonly StackPanel _field;

        public StackPanel Field => _field;

        public double TimeModifier
        {
            get => _timeModifier;
            set => _timeModifier = value;
        }

        public MainController(double width, double height, double cell_size, CoreDispatcher dispatcher)
        {
            this._width = width;
            this._height = height;
            this._cell_size = cell_size;
            this._dispatcher = dispatcher;
            this._timeModifier = 1;
            this._timeBetweenSteps = 100;

            INeighborsCountingRules neighborsCountingRules;
            if (GameSettings.BorderRules == FieldBorderRules.BOUNDED)
            {
                neighborsCountingRules = new StandardNeighborsCountingRules();
            }
            else if (GameSettings.BorderRules == FieldBorderRules.UNBOUNDED)
            {
                neighborsCountingRules = new UnboundedNeighborsCountingRules();
            }   
            else
            {
                neighborsCountingRules = new StandardNeighborsCountingRules();
            }
            _engine = new GameOfLifeEngine((int)(height / cell_size),
                (int)(width / cell_size),
                new StandardLifeAndSurvivalRules(),
                neighborsCountingRules);

            _field = new StackPanel();


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

                    backgroundColor.Color = (_engine.Field[i][j]) ?
                        Windows.UI.Color.FromArgb(255, 0, 0, 0) :
                        Windows.UI.Color.FromArgb(255, 255, 255, 255);

                    Button fieldButton = new Button()
                    {
                        Name = j.ToString(),
                        Width = cell_size,
                        Height = cell_size,
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush() { Color = Windows.UI.Color.FromArgb(1, 0, 0, 0) },
                        Background = backgroundColor
                    };
                    fieldButton.Click += FieldButtonClick;

                    row.Children.Add(fieldButton);
                }

                _field.Children.Add(row);
            }
        }

        public async void PlayOrStopSimulation()
        {
            if (_engine.IsSimulationRun)
            {
                _engine.IsSimulationRun = false;
                if (_simulationTask != null)
                {
                    await _simulationTask;
                }
            }
            else
            {
                
                _engine.IsSimulationRun = true;
                _simulationTask = Task.Run(() => { RunSimulationForward(); });
                await _simulationTask;
            }
        }

        private async void RunSimulationForward()
        {
            while (_engine.IsSimulationRun)
            {
                _engine.MakeStepForward();

                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    MapFieldToScreenField();
                });
                
                Thread.Sleep((int) (_timeBetweenSteps * _timeModifier));
            }
        }

        public void MakeStepForward()
        {
            _engine.MakeStepForward();
        }

        public void MapFieldToScreenField()
        {
            for (int i = 0; i < _engine.Field.Length; i++)
            {
                StackPanel row = _field.Children.ElementAt(i) as StackPanel;
                for (int j = 0; j < _engine.Field[i].Length; j++)
                {
                    Button button = row.Children.ElementAt(j) as Button;

                    SolidColorBrush backgroundColor = new SolidColorBrush();

                    backgroundColor.Color = (_engine.Field[i][j]) ?
                        Windows.UI.Color.FromArgb(255, 0, 0, 0) :
                        Windows.UI.Color.FromArgb(255, 255, 255, 255);

                    button.Background = backgroundColor;
                }
            }
        }

        public void FieldButtonClick(object sender, RoutedEventArgs e)
        {
            Button fieldButton = sender as Button;

            int width_cell = Convert.ToInt32(fieldButton.Name);
            var row = (VisualTreeHelper.GetParent(fieldButton) as StackPanel);//as UIElement);
            int height_cell = Convert.ToInt32(row.Name);


            _engine.Field[height_cell][width_cell] = !_engine.Field[height_cell][width_cell];

            SolidColorBrush backgroundColor = new SolidColorBrush();

            backgroundColor.Color = (_engine.Field[height_cell][width_cell]) ?
                Windows.UI.Color.FromArgb(255, 0, 0, 0) :
                Windows.UI.Color.FromArgb(255, 255, 255, 255);

            fieldButton.Background = backgroundColor;
        }
    }
}
