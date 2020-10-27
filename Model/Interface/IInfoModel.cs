using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface IInfoModel : INotifyPropertyChanged
    {
        // opens a server
        void OpenServer();
        // close the server
        void CloseServer();
    }
}
