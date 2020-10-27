using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{

    public partial class FlightBoard : UserControl
    {

        ObservableDataSource<Point> planeLocations = null;
        private FlightBoardViewModel vm;

        /// <summary>
        /// Constructor
        /// </summary>
        public FlightBoard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Property for setting the view model
        /// </summary>
        public InfoModel Model
        {
            set
            {
                vm = new FlightBoardViewModel(value);
                DataContext = vm;
                // add a delegate to the view model for drawing the Lat and Lon
                vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) { Dispatcher.InvokeAsync(() => { Vm_PropertyChanged(sender, e); });  };
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        /// <summary>
        /// When the view model sends a notification, this function is called and the point will be drawn on the board.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
                Point p1 = new Point(vm.Lat,vm.Lon);            
                planeLocations.AppendAsync(Dispatcher, p1);
        }
    }
}

