using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Interface Client
namespace myFirstProject
{

    public interface InterfaceClient
    {
        void connect(string ip, int port);
        void write(string command);
        string read(); // blocking call
        void disconnect();
    }
}
