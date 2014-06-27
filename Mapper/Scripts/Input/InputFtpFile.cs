
namespace ScriptModule.Scripts.Input
{
    class InputFtpFile : ScriptBase
    {
        public string Uri { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        protected override object ExecuteScript(object param = null)
        {
            return FtpUtils.RetrieveFile(Uri, Login, Password);
        }
    }
}
