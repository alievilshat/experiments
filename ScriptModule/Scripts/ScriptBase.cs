using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xaml;
using System.Xml;
using XamlWriter = System.Windows.Markup.XamlWriter;

namespace ScriptModule.Scripts
{
    public abstract class ScriptBase : IScript, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ProgressChangedEventHandler ProgressChanged;

        public object Execute(object param = null)
        {
            return ExecuteScript(param);
        }

        protected abstract object ExecuteScript(object param);

        protected void OnProgressChanged(object state, int percentage)
        {
            if (ProgressChanged != null)
                ProgressChanged(this, new ProgressChangedEventArgs(percentage, state));
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public static IScript GetScript(string scripttext)
        {
            var settings = new XamlXmlReaderSettings { XmlSpacePreserve = true };
            var reader = new XamlXmlReader(new StringReader(scripttext), settings);
            var result = (IScript)XamlServices.Load(reader);
            return result;
        }

        public static string GetScriptText(IScript script)
        {
            var builder = new StringBuilder();
            var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = false };
            var writer = XmlWriter.Create(new StringWriter(builder), settings);
            XamlWriter.Save(script, writer);
            return builder.ToString();
        }
    }
}
