using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Win32;
using Mapper.Properties;

namespace Mapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SOURCE_SCHEMA = "source";
        private const string TARGET_SCHEMA = "target";
        private const string XSL = "xsl";
        private const string XSL_NAMESPACE = "http://www.w3.org/1999/XSL/Transform";

        public MapperViewModel Model { get; set; }

        private string _currentFilePath;

        public MainWindow()
        {
            Model = new MapperViewModel();
            InitializeComponent();
            if (Settings.Default.LastOpenFile != null && File.Exists(Settings.Default.LastOpenFile))
            {
                loadFiles(Settings.Default.LastOpenFile);
            }
            else
                loadDefaultTemplates();
        }

        #region New Handler
        private void New_Click(object sender, RoutedEventArgs e)
        {
            loadDefaultTemplates();
        }

        private void loadDefaultTemplates()
        {
            _currentFilePath = null;

            Model.SourceSchema = getDefaultSchema(SOURCE_SCHEMA);
            Model.TargetSchema = getDefaultSchema(TARGET_SCHEMA);
            Model.Transformation = getDefaultTransformation();
        }
        #endregion

        #region Open File Handler
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            LoadModel();
        }

        public void LoadModel()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open Transformation Script",
                Filter = "*.xsl|*.xsl"
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                loadFiles(dialog.FileName);
            }
        }

        private void loadFiles(string filepath)
        {
            _currentFilePath = filepath;

            Model.SourceSchema = loadSchema(_currentFilePath, SOURCE_SCHEMA);
            Model.TargetSchema = loadSchema(_currentFilePath, TARGET_SCHEMA);
            Model.Transformation = loadTransformation(_currentFilePath);
        }

        private string getSchemaPath(string _currentFilePath, string suffix)
        {
            return Path.Combine(
                Path.GetDirectoryName(_currentFilePath), 
                Path.GetFileNameWithoutExtension(_currentFilePath) + "_" + suffix + ".xsd"
            );
        }

        private XmlSchema loadSchema(string filename, string suffix)
        {
            try
            {
                var path = getSchemaPath(filename, suffix);

                if (!File.Exists(path))
                    throw new Exception("File " + path + " does not exist.");

                using (var stream = File.OpenText(path))
                {
                    return XmlSchema.Read(stream, (o, e) => Console.WriteLine(e.Message));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return getDefaultSchema(suffix);
            }
        }

        private XmlDataProvider loadTransformation(string filename)
        {
            var transformation = getDefaultTransformation();
            try
            {
                transformation.Document.LoadXml(File.ReadAllText(filename));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return transformation;
        }
        #endregion

        #region Save File Handler
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            _currentFilePath = Save(null);
        }

        public void Save()
        {
            _currentFilePath = Save(_currentFilePath);
        }

        public string Save(string path)
        {
            if (path == null)
            {
                var dialog = new SaveFileDialog
                {
                    Title = "Save Transformation Script",
                    Filter = "*.xsl|*.xsl"
                };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    path = dialog.FileName;
                }
                else
                    return path;
            }
            try
            {
                saveSchema(Model.SourceSchema, path, SOURCE_SCHEMA);
                saveSchema(Model.TargetSchema, path, TARGET_SCHEMA);
                saveTransformation(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                path = Save(null);
            }
            return path;
        }

        private void saveTransformation(string path)
        {
            using (var stream = createNewFile(path))
            {
                XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true });
                Model.Transformation.Document.Save(writer);
            }
        }

        private void saveSchema(XmlSchema schema, string transformationPath, string suffix)
        {
            var schemaPath = getSchemaPath(transformationPath, suffix);
            using (var w = createNewFile(schemaPath))
            {
                schema.Write(w);
            }
        }

        private static FileStream createNewFile(string path)
        {
            if (File.Exists(path))
                return File.Open(path, FileMode.Truncate);
            return File.Open(path, FileMode.Create);
        }
        #endregion

        #region Close Handler
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.LastOpenFile = _currentFilePath;
            Settings.Default.Save();

            var res = MessageBox.Show("Do you want to save the changes?", "Save", MessageBoxButton.YesNoCancel);
            switch (res)
            {
                case MessageBoxResult.No:
                    return;

                case MessageBoxResult.Yes:
                    Save();
                    break;

                default:
                case MessageBoxResult.None:
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
        #endregion
        
        #region Default Templates
        public XmlSchema getDefaultSchema(string root)
        {
            var schema = new XmlSchema();
            schema.Items.Add(new XmlSchemaElement() { Name = root });
            return schema;
        }

        private static XmlDataProvider getDefaultTransformation()
        {
            var transformation = new XmlDataProvider();
            transformation.Document = new XmlDocument();
            transformation.XmlNamespaceManager = new XmlNamespaceManager(transformation.Document.NameTable);
            transformation.XmlNamespaceManager.AddNamespace(XSL, XSL_NAMESPACE);
            transformation.Document.AppendChild(transformation.Document.CreateElement(XSL, "stylesheet", XSL_NAMESPACE));
            return transformation;
        }
        #endregion
    }
}
