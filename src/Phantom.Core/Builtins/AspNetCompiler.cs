using System;
using System.Linq;

namespace Phantom.Core.Builtins
{
    using System.IO;

    public class aspnetcompiler : ExecutableTool<aspnetcompiler>
    {
        public string virtualpath { get; set; }
        public string physicalpath { get; set; }
        public string targetpath { get; set; }

        public aspnetcompiler()
        {
            toolPath = GetToolPath();
        }

        protected override void Execute()
        {
            toolPath = toolPath ?? GetToolPath();

            if (string.IsNullOrEmpty(toolPath))
                throw new InvalidOperationException("please specify the full path to aspnet_compiler.exe");

            if (string.IsNullOrEmpty(virtualpath))
                throw new InvalidOperationException("please specify virtualpath");

            if (string.IsNullOrEmpty(physicalpath))
                throw new InvalidOperationException("please specify physicalpath");

            if (string.IsNullOrEmpty(targetpath))
                throw new InvalidOperationException("please specify targetpath");

            var args = string.Format("-p \"{0}\" -v \"{1}\" {2} ", physicalpath, virtualpath, targetpath);

            Execute(args);
        }

        private static string GetToolPath()
        {
            if (!Directory.Exists("C:/Windows/Microsoft.NET/Framework"))
                return string.Empty;

            return Directory.GetDirectories("C:/Windows/Microsoft.NET/Framework")
                .Where(dir => File.Exists(Path.Combine(dir, "aspnet_compiler.exe")))
                .Select(dir => new FileInfo(Path.Combine(dir, "aspnet_compiler.exe")).FullName)
                .FirstOrDefault();
        }
    }
}
