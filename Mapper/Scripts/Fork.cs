using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptModule.Scripts
{
    public class Fork : ScriptBase
    {
        protected override object ExecuteScript(object param)
        {
            if (!(param is IEnumerable<object>))
                throw new ApplicationException("Input is not IEnumeratble");
            

        }
    }
}
