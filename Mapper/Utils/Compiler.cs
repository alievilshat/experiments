using System;
using System.Linq;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;

namespace ScriptModule.Utils
{
    class Compiler
    {
        public static Assembly CompileAssembly(string code, string[] dependencies)
        {
            var codeProvider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
            var parameters = new CompilerParameters { GenerateInMemory = true };

            parameters.ReferencedAssemblies.AddRange(new[] { 
                    "mscorlib.dll", 
                    "System.dll", 
                    "System.Core.dll", 
                    "System.Data.dll",
                    "System.Windows.Forms.dll",
                    "Microsoft.CSharp.dll",
                    "System.Xaml.dll",
                    Assembly.Load("WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35").Location,
                    Assembly.Load("PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35").Location,
                    Assembly.Load("PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35").Location,
                    Assembly.Load("ScriptModule").Location
                });
            foreach (var d in dependencies)
            {
                parameters.ReferencedAssemblies.Add(Assembly.Load(d).Location);
            }

            var res = codeProvider.CompileAssemblyFromSource(parameters, code);

            if (res.Errors.Count > 0)
            {
                throw new ApplicationException(string.Join("\r\n", res.Errors.Cast<object>()));
            }

            return res.CompiledAssembly;
        }
    }
}
