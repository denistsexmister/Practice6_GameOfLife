using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ChooseGame : Page
    {
        public ChooseGame()
        {
            this.InitializeComponent();

            Button newFieldButton = new Button()
            {
                Content = "Create new field",
                Margin = new Thickness(0, 0, 0, 10)
            };
            newFieldButton.Click += NewFieldButtonClick;
            ComboBox chooseFieldFromFile = new ComboBox() { PlaceholderText = "Open existing field", Margin = new Thickness(0, 0, 0, 10) };
            ReadFieldNamesFromSavedFolder(chooseFieldFromFile);
            Button backButton = new Button()
            {
                Content = "Back",
                Margin = new Thickness(0, 0, 0, 10)
            };
            backButton.Click += BackButtonClick;

            screen.Children.Add(newFieldButton);
            screen.Children.Add(chooseFieldFromFile);
            screen.Children.Add(backButton);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void ReadFieldNamesFromSavedFolder(ComboBox comboBox)
        {
            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

            StorageFolder savesFolder = await installedLocation.GetFolderAsync("SavedFields");

            IReadOnlyList<StorageFile> files = await savesFolder.GetFilesAsync();

            foreach(StorageFile file in files)
            {
                Button fieldButton = new Button()
                {
                    Name = file.Name,
                    Content = file.Name
                };
                fieldButton.Click += FieldButtonClick;
                comboBox.Items.Add(fieldButton);
            }

        }

        private void FieldButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainGamePage), (sender as Button).Name);
        }

        private void NewFieldButtonClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainGamePage));
        }
    }
}
