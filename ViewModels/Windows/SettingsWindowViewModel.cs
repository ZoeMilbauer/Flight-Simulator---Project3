using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    /// <summary>
    /// SettingsWindowViewModel : The setting window view model. 
    /// </summary>
    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;
        private ICommand _clickCommand;
        private ICommand _cancelCommand;

        /// <summary>
        /// Constructor : gets a model. 
        /// </summary>
        /// <param name="model"></param>
        public SettingsWindowViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// FlightServerIP : gets and sets ip
        /// </summary>
        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                // update ip on model and notify it changed
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        /// <summary>
        /// FlightCommandPort : gets and sets command port 
        /// </summary>
        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                // update port on model and notify it changed
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        /// <summary>
        /// FlightInfoPort : gets and sets info port 
        /// </summary>
        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                // update port on model and notify it changed
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }

        /// <summary>
        /// SaveSettings : save settings in model
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }

        /// <summary>
        /// ReloadSettings : reload settings
        /// </summary>
        public void ReloadSettings()
        {
            model.ReloadSettings();
        }

        /// <summary>
        /// ClickCommand : command that occures when ok button is clicked.
        /// Saves the given settings.
        /// </summary>
        #region Commands
        #region ClickCommand
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }

        /// <summary>
        /// OnClick : save given settings when ok button is clicked.
        /// </summary>
        private void OnClick()
        {
            model.SaveSettings();
        }
        #endregion

        /// <summary>
        /// CancelCommand : command that occures when cancel button is clicked.
        /// Reload settings.
        /// </summary>
        #region CancelCommand
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }

        /// <summary>
        /// OnCancel : reload settings when cancel button is clicked.
        /// </summary>
        private void OnCancel()
        {
            model.ReloadSettings();
        }
        #endregion
        #endregion
    }
}

