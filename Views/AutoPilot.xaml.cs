using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.ViewModels;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for AutoPilot.xaml
    /// </summary>
    public partial class AutoPilot : UserControl
    {
        private AutoPilotViewModel vm;
        
        /// <summary>
        ///  Constructor
        /// </summary>
        public AutoPilot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Property for setting the view model
        /// </summary>
        public CommandsModel Model {
            set {
                vm = new AutoPilotViewModel(value);
                DataContext = vm;
                }
        }
    }
}
