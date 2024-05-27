using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Practice6_GameOfLife
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainGamePage : Page
    {
        private readonly double width;
        private readonly double height;
        private readonly double cell_width;
        private readonly double cell_height;//hello

        private Engine engine;

        public MainGamePage()
        {
            this.InitializeComponent();

            width = 800;
            height = 320;
            cell_width = 16;
            cell_height = 16;

            engine = new Engine(1, (int)(height / cell_height),
                (int)(width / cell_width),
                new StandardLifeAndSurvivalRules(),
                new StandardNeighborsCountingRules());


            List<Button> buttons = new List<Button>();

            
            for (int i = 0; i < (height / cell_height); i++)
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Name = i.ToString(),
                    Orientation = Orientation.Horizontal
                };

                for (int j = 0; j < (width / cell_width); j++)
                {
                    SolidColorBrush backgroundColor = new SolidColorBrush();
                    
                    backgroundColor.Color = (engine.Field[i][j]) ?
                        Windows.UI.Color.FromArgb(255, 0, 0, 0) :
                        Windows.UI.Color.FromArgb(255, 255, 255, 255);

                    stackPanel.Children.Add(new Button()
                    {
                        Name = j.ToString(),
                        Width = cell_width,
                        Height = cell_height,
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush() { Color = Windows.UI.Color.FromArgb(1, 0, 0, 0) },
                        Background = backgroundColor
                    });
                }

                game_field.Children.Add(stackPanel);
            }

            
        }

        private void make_step_button_Click(object sender, RoutedEventArgs e)
        {
            engine.MakeStepForward();

            MapFieldToScreenField();
        }

        private void MapFieldToScreenField()
        {
            UIElementCollection rows = game_field.Children;
            for (int i = 0; i < engine.Field.Length; i++)
            {
                StackPanel row = rows.ElementAt(i) as StackPanel;
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
