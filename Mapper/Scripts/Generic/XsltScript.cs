using System;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using ScriptModule.Designers;
using ScriptModule.Designers.XsltScriptDesigner;

namespace ScriptModule.Scripts.Generic
{
    [ScriptDesigner(typeof(XsltScriptDesignerControl))]
    public class XsltScript : ScriptBase
    {
        public XmlDocument TransformationScript { get; set; }
        public XmlDocument SourceSchema { get; set; }
        public XmlDocument TargetSchema { get; set; }

        protected override object ExecuteScript(object param)
        {
            OnProgressChanged("Transformation", 0);
            return Transform((XmlDocument)param, TransformationScript);
        }

        protected virtual String Transform(XmlDocument source, XmlDocument transformationScript)
        {
            var result = new StringBuilder();

            using (var xsltReader = new XmlNodeReader(transformationScript))
            using (var inputReader = new XmlNodeReader(source))
            using (var resultWriter = XmlWriter.Create(result))
            {
                var processor = new XslCompiledTransform();
                processor.Load(xsltReader, new XsltSettings(true, true), new XmlUrlResolver());
                processor.Transform(inputReader, GetTransformationArguments(), resultWriter);
            }
            return result.ToString();
        }

        protected virtual XsltArgumentList GetTransformationArguments()
        {
            return new XsltArgumentList();
        }
    }
}
