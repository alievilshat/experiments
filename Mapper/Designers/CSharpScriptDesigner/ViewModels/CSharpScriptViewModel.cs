using System.Linq;
using System.Reflection;
using ScriptModule.Scripts.Generic;
using ScriptModule.Services;
using ScriptModule.ViewModels;

namespace ScriptModule.Designers.CSharpScriptDesigner.ViewModels
{
    public class CSharpScriptViewModel : ViewModelBase
    {
        private readonly CSharpScript _script;

        public override object Model
        {
            get { return _script; }
        }

        private string[] _classes;
        public string[] Classes
        {
            get { return _classes; }
            set { _classes = value; OnPropertyChanged("Classes"); }
        }


        private string[] _methods;
        public string[] Methods
        {
            get { return _methods; }
            set { _methods = value; OnPropertyChanged("Methods"); }
        }

        public string MainClass
        {
            get { return _script.InstanceClass; }
            set { _script.InstanceClass = value; OnPropertyChanged("MainClass"); }
        }

        public string MainMethod
        {
            get { return _script.MainMethod; }
            set { _script.MainMethod = value; OnPropertyChanged("MainMethod"); }
        }

        public string Dependencies
        {
            get { return _script.Dependencies; }
            set { _script.Dependencies = value; OnPropertyChanged("Dependencies"); }
        }

        public string Code
        {
            get { return _script.Code; }
            set { _script.Code = value; OnPropertyChanged("Code"); }
        }

        private bool _isMainClassDropDownOpen;
        public bool IsMainClassDropDownOpen
        {
            get { return _isMainClassDropDownOpen; }
            set { _isMainClassDropDownOpen = value; OnMainClassDropDownOpenChanged(); }
        }

        private void OnMainClassDropDownOpenChanged()
        {
            if (IsMainClassDropDownOpen)
            {
                try
                {
                    var assembly = Compiler.CompileAssembly(Code, Dependencies);
                    Classes = assembly.GetTypes().Select(i => i.Name).ToArray();
                }
                catch
                { }
            }
            OnPropertyChanged("IsMainClassDropDownOpen");
        }

        private bool _isMainMethodDropDownOpen;
        public bool IsMainMethodDropDownOpen
        {
            get { return _isMainMethodDropDownOpen; }
            set { _isMainMethodDropDownOpen = value; OnMainMethodDropDownOpenChanged(); }
        }

        private void OnMainMethodDropDownOpenChanged()
        {
            if (IsMainMethodDropDownOpen && !string.IsNullOrEmpty(MainClass))
            {
                try
                {
                    var assembly = Compiler.CompileAssembly(Code, Dependencies);
                    var mainClass = assembly.GetTypes().FirstOrDefault(i => i.Name == MainClass);
                    if (mainClass == null)
                        return;

                    Methods = mainClass.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(i => i.Name).ToArray();
                }
                catch
                { }
            }
            OnPropertyChanged("IsMainMethodDropDownOpen");
        }

        public CSharpScriptViewModel(CSharpScript script)
        {
            this._script = script;
        }
    }
}
