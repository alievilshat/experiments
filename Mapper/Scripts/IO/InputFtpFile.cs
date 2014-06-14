
namespace ScriptModule.Scripts.IO
{
    class InputFtpFile : ScriptBase
    {
        public string Login { get; set; }
        public string Password { get; set; }

        protected override object ExecuteScript(object param = null)
        {
            return FtpUtils.RetrieveFile(param as string, Login, Password);
        }
    }
}
