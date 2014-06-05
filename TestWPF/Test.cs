using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using NaitonControls;
using NaitonStore.App_Code.Security;
using WSALibrary;
using NaitonStore.databaseImporterWebService;
using NaitonStore;
using Microsoft.Win32;
using System.Xml.XPath;
using System.Linq;
using System.Collections.Generic;

public class Handler : ScriptHandler
{
    private const string EXPORT_PATH = "script.delivery_export_path";

    public string Path
    {
        get { return ApplicationSettings.Instance.GetApplicationSetting(EXPORT_PATH); }
        set { ApplicationSettings.Instance.SetApplicationSetting(EXPORT_PATH, value); OnPropertyChanged("Path"); }
    }

    private string _result;

    public void ButtonClick(object sender, RoutedEventArgs args)
    {
        try
        {
            var btn = (dynamic)sender;
            var window = btn.FindResource("Form");

            var info = IWSAInfo.WSAInfo;
            var importer = new 
            _results = importer.Execute(info.Login, info.Password, 10, "", "", "<parameters><employeeid>" + info.EmployeeID + "</employeeid></parameters>");

            window.DataContext = this;
            window.Show();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    public void BrowseClick(object sender, RoutedEventArgs args)
    {
        var dialog = new SaveFileDialog();
        if (dialog.ShowDialog().GetValueOrDefault())
        {
            Path = dialog.FileName;
        }
    }

    public void OkClick(object sender, RoutedEventArgs args)
    {
        try
        {
            File.WriteAllText(Path, _result);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    
}