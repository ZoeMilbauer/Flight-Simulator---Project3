using System;
using System.Windows;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsWindowViewModel vm;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vm"> the view model for the setting window </param>
        public SettingsWindow(SettingsWindowViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            DataContext = this.vm;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// The OK botton of the settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ok_Click(object sender, RoutedEventArgs e)
        {
            // if all the fields are not empty
            if (ServerIP.Text != "" && ServerPort.Text != "" && CommandPort.Text != "")
            {
                // update thee view model properties
                vm.FlightServerIP = ServerIP.Text;
                vm.FlightInfoPort = Int32.Parse(ServerPort.Text);
                vm.FlightCommandPort = Int32.Parse(CommandPort.Text);
                vm.SaveSettings();
                vm.ReloadSettings();
            }
            Close(); // close the window
        }

        /// <summary>
        /// The Cancel botton of the settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close(); // close the window
        }
    }
}
