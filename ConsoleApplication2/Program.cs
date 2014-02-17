using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = foo<string>();
            res[0] = new Program().GetresponseEnvelope("");

            Console.WriteLine(res[0]);

        }

        static T[] foo<T>()
        {
            return new T[10];
        }

        String GetresponseEnvelope(String envlopstring)
        {

            var httpRequest = (HttpWebRequest)WebRequest.Create("http://localhost:50358/PaymentGateway.svc");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "text/xml; charset=utf-8";
            httpRequest.Headers.Add("SOAPAction", "http://tempuri.org/PaymentGateway/CheckOrderStatus".Quote());
            httpRequest.ProtocolVersion = HttpVersion.Version11;
            httpRequest.Credentials = CredentialCache.DefaultCredentials;

            using (Stream requestStream = httpRequest.GetRequestStream())
            using (StreamWriter streamWriter = new StreamWriter(requestStream, Encoding.UTF8))
            {
                streamWriter.Write("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\"><s:Body>");
                streamWriter.Write("<CheckOrderStatus xmlns=\"http://tempuri.org/\"><orderid>10</orderid></CheckOrderStatus>");
                streamWriter.Write("</s:Body></s:Envelope>");
            }

            //Get the Response    
            using (HttpWebResponse wr = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (StreamReader srd = new StreamReader(wr.GetResponseStream()))
                {
                    string resulXmlFromWebService = srd.ReadToEnd();
                    Console.Out.WriteLine(resulXmlFromWebService);
                    return resulXmlFromWebService;
                }
            }



        }
    }
}
