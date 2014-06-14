using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchemaEditor
{
    public interface IConnectionSettings
    {
        string Server { get; }
        int Port { get; }
        string Login { get; }
        string LoginPassword { get; }
        string Database { get; }
        int Timeout { get; }
        bool Encription { get; }
        string GetConnectionString();
    }
}
