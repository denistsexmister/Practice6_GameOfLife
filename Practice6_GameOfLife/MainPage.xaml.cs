using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Practice6_GameOfLife
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView appView = ApplicationView.GetForCurrentView();

            appView.SetPreferredMinSize(new Size(800, 600));
            appView.TryResizeView(new Size(800, 600));
        }

        private void play_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainGamePage));
        }

        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
