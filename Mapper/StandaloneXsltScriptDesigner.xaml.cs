using Microsoft.Win32;
using ScriptModule.Designers.XsltScriptDesigner.ViewModels;
using ScriptModule.Properties;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml;
using System.Xml.Schema;

namespace ScriptModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StandaloneXsltScriptDesigner : Window
    {
        private const string SOURCE_SCHEMA = "source";
        private const string TARGET_SCHEMA = "target";

        public MapperViewModel Model { get; set; }

        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(StandaloneXsltScriptDesigner), new PropertyMetadata(string.Empty));

        public string CurrentFilePath
        {
            get { return (string)GetValue(CurrentFilePathProperty); }
            set { SetValue(CurrentFilePathProperty, value); }
        }
        public static readonly DependencyProperty CurrentFilePathProperty =
            DependencyProperty.Register("CurrentFilePath", typeof(string), typeof(StandaloneXsltScriptDesigner), new PropertyMetadata(null, CurrentFilePathChanged));

        private static void CurrentFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var s = (StandaloneXsltScriptDesigner)d;
            s.WindowTitle = string.IsNullOrEmpty(s.CurrentFilePath)
                ? "Mapper"
                : "Mapper [" + Path.GetFileName(s.CurrentFilePath) + "]";
        }


        public StandaloneXsltScriptDesigner()
        {
            Model = new MapperViewModel();
            InitializeComponent();
            if (UserSettings.Default.LastOpenFile != null && File.Exists(UserSettings.Default.LastOpenFile))
            {
                loadFiles(UserSettings.Default.LastOpenFile);
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
            Model.Initialize(null, null, null);
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

            Model.Initialize(loadSchema(CurrentFilePath, SOURCE_SCHEMA),
                loadSchema(CurrentFilePath, TARGET_SCHEMA),
                loadTransformation(CurrentFilePath));
        }

        private string getSchemaPath(string currentFilePath, string suffix)
        {
            return Path.Combine(
                Path.GetDirectoryName(currentFilePath), 
                Path.GetFileNameWithoutExtension(currentFilePath) + "_" + suffix + ".xsd"
            );
        }

        private XmlDocument loadSchema(string filename, string suffix)
        {
            try
            {
                var path = getSchemaPath(filename, suffix);

                if (!File.Exists(path))
                    throw new Exception("File " + path + " does not exist.");

                var doc = new XmlDocument();
                doc.Load(path);
                return doc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private XmlDocument loadTransformation(string filename)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(filename);
                return doc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
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
            {
                return File.Open(path, FileMode.Truncate);
            }
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
            UserSettings.Default.LastOpenFile = CurrentFilePath;
            UserSettings.Default.Save();

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
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            //var executor = new StandaloneScriptsExecutor(Model.SourceSchema, Model.TargetSchema, Model.Transformation.Document);
            //executor.ProgressUpdated += (o, a) => Dispatcher.InvokeAsync(() => Model.AddMessage(a.Total > 0 ? "{0} ({1}/{2})" : "{0}", a.State, a.Current, a.Total));

            //new Thread(() => {
            //    try
            //    {
            //        executor.Execute();
            //    }
            //    catch (Exception ex)
            //    {
            //        Dispatcher.InvokeAsync(() => Model.AddMessage("{0}\n{1}", ex.Message, ex.ToString()));
            //    }
            //}).Start();
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
