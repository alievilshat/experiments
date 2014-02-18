using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    interface IConnectionSettings
    {
        string Server { get; }
        int Port { get; }
        string Login { get; }
        string Password { get; }
        string Database { get; }
        int Timeout { get; }
        bool Encription { get; }
    }
}
