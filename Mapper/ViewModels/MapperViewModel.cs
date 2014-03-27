using System;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Schema;

namespace Mapper
{
    public class MapperViewModel : ViewModelBase
    {
        public event EventHandler LayoutUpdated;

        public MapperViewModel()
        { }

        public IMapperHost Host { get; set; }

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

        public void OnLayoutUpdated()
        {
            if (LayoutUpdated != null)
                LayoutUpdated(this, EventArgs.Empty);
        }

        public RichTextBox _sourceTextBox;
        public RichTextBox SourceTextBox
        {
            get { return _sourceTextBox; }
            set { _sourceTextBox = value; OnPropertyChanged("SourceTextBox"); }
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
        }
    }
}
