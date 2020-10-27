using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FlightSimulator.Model
{
    /// <summary>
    /// InfoModel : model of Flight Board.
    /// Opens a data server that gets location from simulator
    /// </summary>
    public class InfoModel : IInfoModel
    {
        private bool stop = false;
        private ApplicationSettingsModel applicationSettingsModel;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor :  gets an ApplicationSettingsModel for server information. 
        /// </summary>
        /// <param name="applicationSettingsModel"></param>
        public InfoModel(ApplicationSettingsModel applicationSettingsModel)
        {
            this.applicationSettingsModel = applicationSettingsModel;
        }

        /// <summary>
        /// Latitude of plane 
        /// </summary>
        public double Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// Longitude of plane
        /// </summary>
        public double Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// Server : opens a server that reads location from simulator.
        /// </summary>
        public void Server()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(applicationSettingsModel.FlightServerIP), 
                applicationSettingsModel.FlightInfoPort);
            TcpListener listener = new TcpListener(ep);
            listener.Start();
            TcpClient client = listener.AcceptTcpClient(); // accept a client

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                double prevLat = -1;
                double prevLon = -1;
                StreamReader sr = new StreamReader(stream);
                // while not stop, read location from simulator
                while (!stop)
                {
                    // gets info from client
                    string input = sr.ReadLine();
                    string[] values = input.Split(',');
                    Longitude = Convert.ToDouble(values[0]); 
                    Latitude = Convert.ToDouble(values[1]);
                    input = "";
                    // if location is different from prvious location, 
                    // update location and notify it changed
                    if (prevLat != Latitude || prevLon != Longitude)
                    {
                        prevLon = Longitude;
                        prevLat = Latitude;
                        NotifyPropertyChanged("info");
                    }

                }           
            }
            client.Close();
            listener.Stop();
        }

        /// <summary>
        /// OpenServer : opens a task that opens a server and gets information from simulator
        /// </summary>
        public void OpenServer()
        {
            Task t = new Task(() => { Server(); });
            t.Start();
        }

        /// <summary>
        /// NotifyPropertyChanged : notifyes when a property is changes.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// CloseServer : close the server.
        /// </summary>
        public void CloseServer()
        {
            stop = true;
        }
    }
}
