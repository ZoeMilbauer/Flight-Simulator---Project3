using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/// <summary>
/// View Model of the Joystick.
/// </summary>
namespace FlightSimulator.ViewModels
{
    class JoystickViewModel : BaseNotify
    {
        private ICommandModel model;

        private string throttlePath = "/controls/engines/current-engine/throttle";
        private string rudderPath = "/controls/flight/rudder";
        private string aileronPath = "/controls/flight/aileron";
        private string elevatorPath = "/controls/flight/elevator";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model"> Recieves a command model</param>
        public JoystickViewModel(ICommandModel model)
        {
            this.model = model;
            
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        /// <summary>
        /// Send a line of command to the model in order to update the rudder of the plane,
        /// by the given value.
        /// </summary>
        /// <param name="command"> the new value of the rudder </param>
        public void UpdateRudder(string command)
        {
            List<string> list = new List<string>();
            list.Add("set "+rudderPath+" "+command);
            model.SetValues(list, false);   
        }

        /// <summary>
        /// Send a line of command to the model in order to update the throttle of the plane,
        /// by the given value.
        /// </summary>
        /// <param name="command"> the new value of the throttle </param>
        public void UpdateThrottle(string command)
        {
            List<string> list = new List<string>();
            list.Add("set "+throttlePath+ " "+ command);
            model.SetValues(list, false);
        }

        /// <summary>
        /// Send two lines of commands to the model in order to update the aileron and the elevator of the plane,
        /// by the given values.
        /// </summary>
        /// <param name="commandAileron"> the new value of the aileron </param>
        /// <param name="commandElevator"> the new value of the elevator </param>
        public void updateAileronAndElevator(string commandAileron, string commandElevator)
        {
            List<string> list = new List<string>();
            list.Add("set " + aileronPath + " " + commandAileron);
            list.Add("set " + elevatorPath + " " + commandElevator);
            model.SetValues(list, false);
        }
    }
}
