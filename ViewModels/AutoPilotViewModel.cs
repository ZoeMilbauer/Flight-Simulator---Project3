using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using FlightSimulator.Model.Interface;
using System.Windows.Media;
using System.Windows.Input;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// AutoPilotViewModel : The Auto Pilot view model.
    /// </summary>
    public class AutoPilotViewModel: BaseNotify
    {
        private string text;
        private ICommandModel model;
        private ICommand _command;
        private ICommand _clearCommand;

        /// <summary>
        /// Constructor : gets the command model as a model.
        /// </summary>
        /// <param name="model"></param>
        public AutoPilotViewModel(ICommandModel model)
        {
            this.model = model;          
        }

        /// <summary>
        /// UserText : binding with the text that user write on text box.
        /// </summary>
        public string UserText
        {
            // change background of text box to pink when there is a text.
            set
            {
                text = value;
                if (text != "")
                {
                    BackgroundColor = Brushes.Pink;
                    NotifyPropertyChanged("BackgroundColor");
                }
            }
            get
            {
                return text;
            }
        }

        /// <summary>
        /// BackgroundColor : binding with the background of text box.
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            set;
            get;
        }


        /// <summary>
        /// OkCommand : command that occures when ok button is clicked.
        /// commands are sent to command model, and background turnes to white.
        /// </summary>        
        public ICommand OkCommand
        {
            get
            {
                return _command ?? (_command = new CommandHandler(() => OkButton()));
            }
        }

        /// <summary>
        /// OkButton : send commands to command model, and turnes background to white,
        /// when ok button is clicked.
        /// </summary>
        private void OkButton()
        {
            BackgroundColor = Brushes.White;
            NotifyPropertyChanged("BackgroundColor");
            List<string> commands;
            string str = UserText; // get the user text
            if (str != "")
            {
                // split commands to list and send to model
                commands = str.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                SendCommands(commands);
            }
        }

        /// <summary>
        /// ClearCommand : command that occures when clear button is clicked.
        /// clear user text and turnes background to white.
        /// </summary>
        public ICommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => ClearButton()));

            }
        }

        /// <summary>
        /// ClearButton : clear user text and turnes background to white.
        /// </summary>
        private void ClearButton()
        {
            BackgroundColor = Brushes.White;
            NotifyPropertyChanged("BackgroundColor");
            UserText = "";
            NotifyPropertyChanged("UserText");
        }

        /// <summary>
        /// SendCommands : gets list of commands from the Auto Pilot and send them to model.
        /// </summary>
        /// <param name="commands"></param>
        public void SendCommands(List<string> commands)
        {
            model.SetValues(commands,true);
        }
    }
}
