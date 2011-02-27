using System;

namespace Phantom.Core.Builtins
{
    using System.IO;

    public class msdeploy : ExecutableTool<msdeploy>
    {

        public msdeploy()
        {
            toolPath = GetMSDeployExePath();
        }

        public string verb { get; set; }
        public string dest { get; set; }
        public string source { get; set; }
        public string flags { get; set; }

        protected override void Execute()
        {
            toolPath = toolPath ?? GetMSDeployExePath();

            if (string.IsNullOrEmpty(toolPath))
                throw new InvalidOperationException("please specify the full path to msdeploy.exe");
            
            if (string.IsNullOrEmpty(verb))
                throw new InvalidOperationException("please specify verb");
            
            if (string.IsNullOrEmpty(source))
                throw new InvalidOperationException("please specify source");
            
            if (string.IsNullOrEmpty(dest))
                throw new InvalidOperationException("please specify dest");
            
            var args = string.Format("-verb:{0} -source:{1}, -dest:{2}", verb, source, dest);

            if (flags != null)
                args = string.Format("{0} {1}", args, string.Join(",", flags));
            
            Execute(args);
        }

        private static string GetMSDeployExePath()
        {
            if (File.Exists("C:/Program Files/IIS/Microsoft Web Deploy/msdeploy.exe"))
                return new FileInfo("C:/Program Files/IIS/Microsoft Web Deploy/msdeploy.exe").FullName;
            
            return File.Exists("C:/Program Files (x86)/IIS/Microsoft Web Deploy/msdeploy.exe") 
            ? new FileInfo("C:/Program Files (x86)/IIS/Microsoft Web Deploy/msdeploy.exe").FullName 
            : string.Empty;
        }

    }
}
