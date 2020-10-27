using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FlightSimulator.Model;
using FlightSimulator.Views;
using FlightSimulator.ViewModels.Windows;

/// <summary>
/// The Main of the program.
/// </summary>
namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationSettingsModel applicationSettingsModel;
        private CommandsModel commandsModel;
        private ICommand _connectCommand;
        private InfoModel infoModel;
        private bool isConnected= false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            applicationSettingsModel = new ApplicationSettingsModel(); // create the ApplicationSettingsModel once
            commandsModel = new CommandsModel(applicationSettingsModel); // create the command model for the simulator
            infoModel = new InfoModel(applicationSettingsModel); // create the info model for the simulator
            // add the models to the views for creating the view models
            auto.Model = commandsModel;
            joystick.Model = commandsModel;
            flightBoard.Model = infoModel;
        }
        
        /// <summary>
        /// Property command for the Connect button
        /// </summary>
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => ConnectButton()));
            }
        }

        /// <summary>
        /// Connect method for the command
        /// </summary>
        private void ConnectButton()
        {
            if (isConnected) // if it is not the first connection
            {
                // close the current server
                infoModel.CloseServer();
                commandsModel.Disconnect();  
            }
            else
            {
                isConnected = true;
            }
            // open the server for the simulator and connect to it
            infoModel.OpenServer();
            commandsModel.Connect();
            connectButton.Content = "Connected!";         
            connectButton.Foreground = Brushes.MediumSeaGreen;
        }

        /// <summary>
        /// Create the setting window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsWindow(object sender, RoutedEventArgs e)
        {
            SettingsWindow s = new SettingsWindow(new SettingsWindowViewModel(applicationSettingsModel));
            s.Show();
        }
        
        /// <summary>
        /// This method is called when the user closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // close server and disconnect
            infoModel.CloseServer();
            commandsModel.Disconnect();
        }
    }
}
