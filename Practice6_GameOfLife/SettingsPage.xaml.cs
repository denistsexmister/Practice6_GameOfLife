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
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            select.SelectedIndex = (GameSettings.BorderRules == FieldBorderRules.UNBOUNDED) ? 1 : 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string boundSettings = ((sender as ComboBox).SelectedItem as ComboBoxItem).Name;
            GameSettings.BorderRules = boundSettings.Equals("bounded") ? FieldBorderRules.BOUNDED : (boundSettings.Equals("unbounded")) ? FieldBorderRules.UNBOUNDED : FieldBorderRules.BOUNDED;
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
