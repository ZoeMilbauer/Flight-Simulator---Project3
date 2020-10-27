using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// View Model Of the Flight Board
/// </summary>
namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private InfoModel model;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"> Recieve aa model of type InfoModel </param>
        public FlightBoardViewModel(InfoModel model)
        {
            this.model = model;
            // change the values of the longtitude and latitude when the model sends a notification
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                ChangeLatLon();
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        /// <summary>
        /// Property for the Longtitude
        /// </summary>
        public double Lon
        {
            set;
            get;
        }

        /// <summary>
        /// property for the Latitude
        /// </summary>
        public double Lat
        {
            set;
            get;
        }

        /// <summary>
        /// Update the Longtitude and the Latitude by the values in the model
        /// </summary>
        public void ChangeLatLon()
        {
            Lon = model.Longitude;
            Lat = model.Latitude;
        }
    }

}
