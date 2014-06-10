using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptModule.Scripts
{
    public abstract class ScriptBase : IScript
    {
        public abstract void Execute();
    }
}
