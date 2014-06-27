
namespace ScriptModule.Scripts.Output
{
    public class OutputFtpFile : ScriptBase
    {
        public string Uri { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        protected override object ExecuteScript(object param = null)
        {
            FtpUtils.UploadFile(Uri, param as byte[], Login, Password);
            return param;
        }
    }
}