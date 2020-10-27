using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulator.Model
{
    /// <summary>
    /// CommandsModel : model of Auto Pilot, and Joystick. 
    /// Connected to the flight simulator and send it commands. 
    /// </summary>
    public class CommandsModel : ICommandModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ApplicationSettingsModel applicationSettingsModel;
        private List<string> commands = new List<string>();
        private bool stop = false;
        private bool wait2sec = false;

        /// <summary>
        /// Constructor : gets an ApplicationSettingsModel for connecting information. 
        /// </summary>
        /// <param name="applicationSettingsModel"></param>
        public CommandsModel(ApplicationSettingsModel applicationSettingsModel)
        {
            this.applicationSettingsModel = applicationSettingsModel;
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
        /// OpenSocket : opens the client socket and connects to the simulator.
        /// </summary>
        public void OpenSocket()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(
                applicationSettingsModel.FlightServerIP), 
                applicationSettingsModel.FlightCommandPort);
            TcpClient client = new TcpClient(); 
            bool connected = false;
            // try to connect to simulator
            while (!connected)
            {
                try
                {
                    client.Connect(ep);
                    connected = true; // connection succeed 
                }
                catch
                { }
            }
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // while not stop connection, send given commands to the simulator 
                while (!stop)
                {
                   // if there are commands in list commands
                    if (this.commands.Count() > 0)
                    {
                        int counter = 0;
                        // send every command to the simulator
                        foreach (string command in commands)
                        {
                            counter++;
                            Byte[] data = Encoding.ASCII.GetBytes(command + "\r\n");
                            stream.Write(data, 0, data.Length); // write to simulator
                            // if commands was sent from auto pilot, 
                            // and command is not last, wait 2 sec
                            if (wait2sec && counter != commands.Count())
                            {
                                Thread.Sleep(2000);
                            }
                            
                        }
                        commands.Clear(); // clear commands list
                        // notify that commands were sent
                        NotifyPropertyChanged("command");
                    }                   
                }               
            }
            client.Close(); 
        }

        /// <summary>
        /// Connect : opens a task that connects and sends given commands to the simulator
        /// </summary>
        public void Connect()
        {        
            Task t = new Task(() => { OpenSocket(); });
            t.Start();            
        }

        /// <summary>
        /// SetValues : gets a list of commands from view model and add it to list commands.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="wait2sec"></param>
        public void SetValues(List<string> commands, bool wait2sec)
        {
            this.wait2sec = wait2sec;
            this.commands.AddRange(commands);           
        }

        /// <summary>
        /// Disconnect : disconnect the simulator
        /// </summary>
        public void Disconnect()
        {
            stop = true;
        }

    }
}
