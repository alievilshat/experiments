using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ScriptModule
{
    public class FtpUtils
    {
        public static string RetrieveFile(string path, string login, string password)
        {
            var request = createFtpRequest(path, WebRequestMethods.Ftp.DownloadFile, login, password);

            using (var response = (FtpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string[] RetrieveListOfFiles(string location, string login, string password)
        {
            var request = createFtpRequest(location, WebRequestMethods.Ftp.ListDirectory, login, password);

            using (var response = (FtpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                List<string> files = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    files.Add(line);
                }
                return files.ToArray();
            }
        }

        public static string UploadFile(string location, byte[] data, string login, string password)
        {
            var request = createFtpRequest(location, WebRequestMethods.Ftp.UploadFile, login, password);

            request.ContentLength = data.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
            }
        }

        private static FtpWebRequest createFtpRequest(string path, string method, string login, string password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Credentials = new NetworkCredential(login, password);
            request.Method = method;
            return request;
        }
    }
}