using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
        private readonly MainController mainController;

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
            mainController = new MainController(800, 320, 16, this.Dispatcher);

            gameField.Children.Add(mainController.Field);

            Grid buttonsGrid = new Grid();
            ColumnDefinition firstColumn = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
            ColumnDefinition secondColumn = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
            buttonsGrid.ColumnDefinitions.Add(firstColumn);
            buttonsGrid.ColumnDefinitions.Add(secondColumn);

            StackPanel leftStackPanel = new StackPanel();
            StackPanel rightStackPanel = new StackPanel();
            Grid.SetColumn(leftStackPanel, 0);
            Grid.SetColumn(rightStackPanel, 1);
            buttonsGrid.Children.Add(leftStackPanel);
            buttonsGrid.Children.Add(rightStackPanel);

            Button makeStepButton = new Button()
            {
                Name = "makeStepButton",
                Content = "Make Step",
                Margin = new Thickness(0, 0, 0, 10)
            };
            makeStepButton.Click += MakeStepButtonClick;
            Button playSimulationButton = new Button()
            {
                Name = "playSimulationButton",
                Content = "Play",
                Margin = new Thickness(0, 0, 0, 10)
            };
            playSimulationButton.Click += PlaySimulationButtonClick;

            StackPanel speedStackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBlock speedText = new TextBlock()
            {
                Text = "Speed modifier: "
            };
            Slider speedSlider = new Slider()
            {
                Name = "speedSlider",
                Minimum = 0.5,
                Maximum = 2,
                Value = 1,
                StepFrequency = 0.5,
                Width = 100,
                TickFrequency = 0.5,
                TickPlacement = TickPlacement.Outside
            };
            speedSlider.ValueChanged += SpeedSliderValueChanged;

            speedStackPanel.Children.Add(speedText);
            speedStackPanel.Children.Add(speedSlider);

            StackPanel saveStackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 10) };
            TextBox filenameBox = new TextBox()
            {
                Name = "filenameBox",
                PlaceholderText = "Write filename to save...",
                Margin = new Thickness(0, 0, 10, 0)
            };
            Button saveButton = new Button()
            {
                Name = "saveButton",
                Content = "Save current field"
            };
            saveButton.Click += SaveButtonClick;

            saveStackPanel.Children.Add(filenameBox);
            saveStackPanel.Children.Add(saveButton);

            Button backButton = new Button()
            {
                Content = "Back"
            };
            backButton.Click += BackButtonClick;

            leftStackPanel.Children.Add(makeStepButton);
            leftStackPanel.Children.Add(playSimulationButton);
            leftStackPanel.Children.Add(speedStackPanel);
            leftStackPanel.Children.Add(backButton);

            rightStackPanel.Children.Add(saveStackPanel);

            buttonsField.Children.Add(buttonsGrid);


            screen.Children.Add(gameField);
            screen.Children.Add(buttonsField);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChooseGame));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is string filename)
            {
                await mainController.GetFieldFromFile(filename);
                mainController.MapFieldToScreenField();
            }
        }

        private async void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            TextBox filenameBox = (VisualTreeHelper.GetParent((sender as Button)) as StackPanel).Children.ElementAt(0) as TextBox;
            string filename = filenameBox.Text ?? "default";
            if (filename.Equals("")) filename = "default";
            filename += ".txt";

            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

            StorageFolder savesFolder = await installedLocation.GetFolderAsync("SavedFields");

            StorageFile saveFile = await savesFolder.CreateFileAsync(filename, CreationCollisionOption.GenerateUniqueName);

            string fieldString = mainController.GetFieldInStringFormat();

            await FileIO.WriteTextAsync(saveFile, fieldString);
        }

        private void SpeedSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider speedSlider = sender as Slider;
            mainController.TimeModifier = speedSlider.Value;
        }

        private void PlaySimulationButtonClick(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = (sender as Button).Content.Equals("Play") ? "Stop" : "Play";
            mainController.PlayOrStopSimulation();
        }

        private void MakeStepButtonClick(object sender, RoutedEventArgs e)
        {
            mainController.MakeStepForward();

            mainController.MapFieldToScreenField();
        }
    }
}
