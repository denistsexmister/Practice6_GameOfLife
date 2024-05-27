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
        private readonly MainController mainController = new MainController(800, 320, 16);

        private readonly StackPanel gameField = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(0, 10, 0, 10)
        };
        private readonly StackPanel buttonsField = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(0, 10, 0, 10)
        };

        public MainGamePage()
        {
            this.InitializeComponent();


            gameField.Children.Add(mainController.Field);

            Button makeStepButton = new Button()
            {
                Name = "makeStepButton",
                Content = "Make Step"
            };
            makeStepButton.Click += MakeStepButtonClick;
            Button playSimulationButton = new Button()
            {
                Name = "playSimulationButton",
                Content = "Play"
            };
            playSimulationButton.Click += PlaySimulationButtonClick;

            buttonsField.Children.Add(makeStepButton);
            buttonsField.Children.Add(playSimulationButton);


            screen.Children.Add(gameField);
            screen.Children.Add(buttonsField);
        }

        private void PlaySimulationButtonClick(object sender, RoutedEventArgs e)
        {
            mainController.PlayOrStopSimulation();
        }

        private void MakeStepButtonClick(object sender, RoutedEventArgs e)
        {
            mainController.MakeStepForward();

            mainController.MapFieldToScreenField();
        }
    }
}
