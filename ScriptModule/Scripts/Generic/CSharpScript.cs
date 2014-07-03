using ScriptModule.Designers;
using ScriptModule.Designers.CSharpScriptDesigner;
using ScriptModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;

namespace ScriptModule.Scripts.Generic
{
    [ScriptDesigner(typeof(CSharpScriptDesignerControl))]
    [ContentProperty("Code")]
    public class CSharpScript : ScriptBase
    {
        public string Code { get; set; }
        public string InstanceClass { get; set; }
        public string MainMethod { get; set; }
        public string Dependencies { get; set; }

        public CSharpScript()
        {
        }

        protected override object ExecuteScript(object param)
        {
            OnProgressChanged("Initialization", 0);
            var method = GetMainMethod();
            var instance = GetInstance();

            OnProgressChanged("Execution", 20);

            int paramCount = method.GetParameters().Length;
            switch (paramCount)
            {
                case 0:
                    return method.Invoke(instance, new object[0]);

                case 1:
                    return method.Invoke(instance, new object[] { param });

                default:
                    throw new ApplicationException("Main method of CSharpScript should have 0 or 1 parameter.");
            }
        }

        private Assembly _compiledAssembly;
        public Assembly GetAssembly()
        {
            if (_compiledAssembly == null)
            {
                _compiledAssembly = Compiler.CompileAssembly(Code, getDependencies());
            }
            return _compiledAssembly;
        }

        private string[] getDependencies()
        {
            if (Dependencies == null)
                return new string[0];
            return Dependencies.Split(new [] {';'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        private object _instance;
        public object GetInstance()
        {
            if (_instance == null)
            {
                _instance = createInstance();
            }
            return _instance;
        }
        
        public MethodInfo GetMainMethod()
        {
            return GetMethod(getMainMethodName());
        }

        private readonly Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();
        public MethodInfo GetMethod(string methodName)
        {
            MethodInfo res;
            if (_methods.TryGetValue(methodName, out res))
                return res;

            _methods[methodName] = res = createMethodInfo(methodName);
            return res;
        }

        private Type getInstanceType()
        {
            var assembly = GetAssembly();
            return InstanceClass != null ? assembly.GetType(InstanceClass) : assembly.GetTypes().First();
        }

        private object createInstance()
        {
            var assembly = GetAssembly();
            var type = getInstanceType();
            return Activator.CreateInstance(type);
        }

        private string getMainMethodName()
        {
            if (MainMethod == null)
            {
                var method = getInstanceType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).SingleOrDefault();
                if (method == null)
                    throw new Exception("Main method not specified!");
                return method.Name;
            }
            return MainMethod;
        }

        private MethodInfo createMethodInfo(string methodName)
        {
            var type = getInstanceType();
            return type.GetMethod(methodName);
        }
    }
}
