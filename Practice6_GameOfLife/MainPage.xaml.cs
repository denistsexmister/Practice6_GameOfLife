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

            Size windowSize = new Size(800, 600);
            ApplicationView.PreferredLaunchViewSize = windowSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().TryResizeView(windowSize);
            Window.Current.CoreWindow.SizeChanged += (s, e) =>
            {
                ApplicationView.GetForCurrentView().TryResizeView(windowSize);
            };
        }

        private void play_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChooseGame));
        }

        private void settings_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
