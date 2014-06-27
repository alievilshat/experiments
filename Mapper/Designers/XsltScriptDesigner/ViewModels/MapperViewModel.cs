using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Schema;
using ScriptModule.Designers.XsltScriptDesigner.Logic;
using ScriptModule.Scripts.Generic;
using ScriptModule.Designers.XsltScriptDesigner.ViewModels.Xslt;

namespace ScriptModule.Designers.XsltScriptDesigner.ViewModels
{
    public class MapperViewModel : DesignerViewModelBase
    {
        public event EventHandler Initialized;
        public event EventHandler SizeChanged;
        public XsltScript Script { get; set; }

        public override object Model
        {
            get { return Script; }
        }

        public MapperViewModel()
        {
            Messages = new ObservableCollection<string>();
            Messages.CollectionChanged += (o, e) => HasMessages = Messages.Count > 0;
        }

        public MapperViewModel(XsltScript script)
        {
            this.Script = script;
            Initialize(script);
        }

        public IMapperHost Host { get; set; }

        public ObservableCollection<string> Messages { get; set; }

        private bool _hasMessages;
        public bool HasMessages 
        {
            get { return _hasMessages; }
            set { _hasMessages = value; OnPropertyChanged("HasMessages"); }
        }

        public void AddMessage(string text)
        {
            if (string.IsNullOrEmpty(text) || Messages.LastOrDefault() == text)
                return;

            Messages.Add(text);
            if (Messages.Count > 100)
                Messages.RemoveAt(0);
        }

        public void AddMessage(string format, params object[] args)
        {
            AddMessage(string.Format(format, args));
        }

        private XmlSchema _sourceSchema;
        public XmlSchema SourceSchema
        {
            get { return _sourceSchema; }
            set { _sourceSchema = value; OnPropertyChanged("SourceSchema"); }
        }

        private XmlSchema _targetSchema;
        public XmlSchema TargetSchema
        {
            get { return _targetSchema; }
            set { _targetSchema = value; OnPropertyChanged("TargetSchema"); }
        }

        private XmlDataProvider _transformation;
        public XmlDataProvider Transformation
        {
            get { return _transformation; }
            set
            {
                _transformation = value;
                TransformationViewModel = new XsltStylesheetViewModel() { 
                    MapperViewModel = this,
                    Node = Transformation != null ? Transformation.Document : null
                };
                OnPropertyChanged("Transformation"); 
            }
        }

        private XsltStylesheetViewModel _transformationViewModel;
        public XsltStylesheetViewModel TransformationViewModel
        {
            get { return _transformationViewModel; }
            set { _transformationViewModel = value; OnPropertyChanged("TransformationViewModel"); }
        }

        private RichTextBox _sourceTextBox;
        public RichTextBox SourceTextBox
        {
            get { return _sourceTextBox; }
            set { _sourceTextBox = value; OnPropertyChanged("SourceTextBox"); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; OnSizeChanged("Width"); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; OnSizeChanged("Height"); }
        }

        private void OnSizeChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
            if (SizeChanged != null)
                SizeChanged(this, EventArgs.Empty);
        }

        public void OnInitialized()
        {
            if (Initialized != null)
                Initialized(this, EventArgs.Empty);
        }

        public void UpdateSourceTextBox()
        {
            var builder = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(builder, new XmlWriterSettings { Indent = true });
            Transformation.Document.Save(writer);

            if (SourceTextBox == null)
                SourceTextBox = CreateRichTextBox();

            var rtb = SourceTextBox;
            SourceTextBox = null;

            rtb.Document.Blocks.Clear();
            rtb.Document.Blocks.Add(new Paragraph(new Run(builder.ToString())));

            SourceTextBox = rtb;
        }

        public void UpdateModel()
        {
            if (SourceTextBox == null)
                return;

            var t = Transformation;
            Transformation = null;

            string text = new TextRange(SourceTextBox.Document.ContentStart, SourceTextBox.Document.ContentEnd).Text;
            t.Document.LoadXml(text);

            Transformation = t;
            InvokeInitialized();
        }

        public void AddTransformation(XmlSchemaElement source, XmlSchemaElement target)
        {
            var builder = new TransformationBuilder(Transformation.Document);
            var node = builder.BuildTransform(source, target);
            node.AsObservable().Update("ChildNodes");
        }

        private void Initialize(XsltScript script)
        {
            Initialize(script.SourceSchema, script.TargetSchema, script.TransformationScript);
        }

        public void Initialize(XmlDocument sourceSchema, XmlDocument targetSchema, XmlDocument transformaitonScript)
        {
            ValidationEventHandler validationEventHandler = (sender, args) => Debug.WriteLine(args.Message);

            this.SourceSchema = sourceSchema != null ? XmlSchema.Read(new XmlNodeReader(sourceSchema), validationEventHandler) : getDefaultSchema("source");
            this.TargetSchema = targetSchema != null ? XmlSchema.Read(new XmlNodeReader(targetSchema), validationEventHandler) : getDefaultSchema("target");

            var transformation = getDefaultTransformation();
            if (transformaitonScript != null) transformation.Document.Load(new XmlNodeReader(transformaitonScript));
            this.Transformation = transformation;

            InvokeInitialized();
        }

        private XmlSchema getDefaultSchema(string root)
        {
            var schema = new XmlSchema();
            schema.Items.Add(new XmlSchemaElement() { Name = root });
            return schema;
        }

        private static XmlDataProvider getDefaultTransformation()
        {
            var transformation = new XmlDataProvider { Document = new XmlDocument() };
            transformation.XmlNamespaceManager = new XmlNamespaceManager(transformation.Document.NameTable);
            transformation.XmlNamespaceManager.AddNamespace(XSL, XSL_NAMESPACE);
            transformation.Document.AppendChild(transformation.Document.CreateElement(XSL, "stylesheet", XSL_NAMESPACE));
            return transformation;
        }
        

        private void InvokeInitialized()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)OnInitialized, DispatcherPriority.Render, new object[0]);
        }
    }
}
