using System;
using System.Windows.Markup;
using ScriptModule.Scripts;

namespace ScriptModule.DAL
{
    public class ScriptRow
    {
        public int ScriptId { get; set; }
        public string ScriptName { get; set; }
        public string ScriptText { get; set; }

        public IScript GetScript()
        {
            return (IScript)XamlReader.Parse(ScriptText);
        }

        public void UpdateScript(IScript script)
        {
            throw new NotImplementedException();
        }
    }
}
