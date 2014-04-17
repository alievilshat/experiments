using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using DatabaseImporterWebService;
using DatabaseImporterWebService.ProcessManagement;
using Mapper.Properties;
using Microsoft.Win32;

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

        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public string CurrentFilePath
        {
            get { return (string)GetValue(CurrentFilePathProperty); }
            set { SetValue(CurrentFilePathProperty, value); }
        }
        public static readonly DependencyProperty CurrentFilePathProperty =
            DependencyProperty.Register("CurrentFilePath", typeof(string), typeof(MainWindow), new PropertyMetadata(null, CurrentFilePathChanged));

        private static void CurrentFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = (MainWindow)d;
            s.WindowTitle = string.IsNullOrEmpty(s.CurrentFilePath)
                ? "Mapper"
                : "Mapper [" + Path.GetFileName(s.CurrentFilePath) + "]";
        }


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
            CurrentFilePath = null;

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
            CurrentFilePath = filepath;

            Model.SourceSchema = loadSchema(CurrentFilePath, SOURCE_SCHEMA);
            Model.TargetSchema = loadSchema(CurrentFilePath, TARGET_SCHEMA);
            Model.Transformation = loadTransformation(CurrentFilePath);
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
            CurrentFilePath = Save(null);
        }

        public void Save()
        {
            CurrentFilePath = Save(CurrentFilePath);
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
            Settings.Default.LastOpenFile = CurrentFilePath;
            Settings.Default.Save();

            if (!ensureSaveChanges())
                e.Cancel = true;
        }

        private bool ensureSaveChanges()
        {
            var res = MessageBox.Show("Do you want to save the changes?", "Save", MessageBoxButton.YesNoCancel);
            switch (res)
            {
                case MessageBoxResult.No:
                    return true;

                case MessageBoxResult.Yes:
                    Save();
                    return true;

                default:
                case MessageBoxResult.None:
                case MessageBoxResult.Cancel:
                    return false;
            }
        }
        #endregion

        #region Run Handler
        int nextjobid = 0;
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            var id = nextjobid++;

            var doc = XDocument.Parse(Model.Transformation.Document.InnerXml);
            var executor = new StandaloneScriptsExecutor(id, doc);

            new Thread(() =>
            {
                try
                {
                    AsyncProcessInfo info;
                    while ((info = AsyncProcessManager.GetProcessInfo(id)) == null || !info.Completed)
                    {
                        if (info != null)
                            Dispatcher.InvokeAsync(() => 
                                Model.AddMessage(info.Total > 0 ? "{0} ({1}/{2})" : "{0}", info.StatusText, info.Processed, info.Total));

                        AsyncProcessManager.Wait(id, 1000);
                    }
                    Dispatcher.InvokeAsync(() => Model.AddMessage(info.StatusText));
                }
                catch (Exception ex)
                {
                    Dispatcher.InvokeAsync(() => Model.AddMessage("ERROR: {0}\n{1}", ex.Message, ex.ToString()));
                }

            }).Start();

            AsyncProcessManager.StartAsyncProcess(executor);
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

        #region Message Panel Handlers
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
             var val = messagePanel.Height - e.VerticalChange;
             if (val >= 0)
                 messagePanel.Height = val;
        }

        double messagePanelHeight = 100;
        private void Thumb_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (messagePanel.Height > 0)
            {
                messagePanelHeight = messagePanel.Height;
                messagePanel.Height = 0;
            }
            else
                messagePanel.Height = messagePanelHeight;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Model.Messages.Clear();
        }
        #endregion

    }
}
