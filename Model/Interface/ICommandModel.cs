using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightSimulator.Model.Interface
{
    public interface ICommandModel : INotifyPropertyChanged 
    {
        // connect to simulator
        void Connect();
        // disconnect to simulator
        void Disconnect();
        // set commands to commands list
        void SetValues(List<string> commands, bool wait2sec);
    }
}
