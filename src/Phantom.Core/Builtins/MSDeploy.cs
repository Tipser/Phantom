using System;
using System.Linq;

namespace Phantom.Core.Builtins
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public class msdeploy : ExecutableTool<msdeploy>
    {

        public msdeploy()
        {
            toolPath = GetMSDeployExePath();
            skipfiles = new string[0];
            dest = new Dictionary<string, object>();
            source = new Dictionary<string, object>();
        }

        public string verb { get; set; }
        public IDictionary dest { get; set; }
        public IDictionary source { get; set; }
        public string flags { get; set; }
        public string[] skipfiles { get; set; }

        protected override void Execute()
        {
            toolPath = toolPath ?? GetMSDeployExePath();

            var args = GetArgs();

            Execute(args);
        }

        public string GetArgs() {

            if (string.IsNullOrEmpty(toolPath))
                throw new InvalidOperationException("please specify the full path to msdeploy.exe");

            if (string.IsNullOrEmpty(verb))
                throw new InvalidOperationException("please specify verb");

            if (source.Keys.Count == 0)
                throw new InvalidOperationException("please specify source");

            if (dest.Keys.Count == 0)
                throw new InvalidOperationException("please specify dest");

            var sourceStr = string.Join(",", source.Keys.Cast<string>().Select(key => ToParameter(key, source[key])));
            var destStr = string.Join(",", dest.Keys.Cast<string>().Select(key => ToParameter(key, dest[key])));

            var args = string.Format("-verb:{0} {3} -source:{1}, -dest:{2}", verb, sourceStr, destStr, SkipFiles);

            if (flags != null)
                args = string.Format("{0} {1}", args, string.Join(",", flags));

            return args;
        }

        private static string ToParameter(string key, object value) {
            return string.Format("{0}={2}{1}{2}", key, value, value is string ? "\"" : string.Empty);
        }

        private string SkipFiles 
        {
            get 
            {
                return string.Join(" ", skipfiles.Select(expr => string.Format("-skip:objectName=filePath,absolutePath=\"{0}\"", expr)).ToArray());
            }
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
