using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phantom.Tests.creamdog
{
    using Core.Builtins;
    using NUnit.Framework;

    [TestFixture]
    public class MSDepoyTester
    {
        [Test]
        public void kjjjdj() {

            var ms = new msdeploy
            {
                verb = "sync",
                source = new Dictionary<string, object>() { { "iisApp", "Default Website" } },
                dest = new Dictionary<string, object>() { { "iisApp", "Default Website" }, { "wmsvc", "127.0.0.1" }, { "authType", "basic" } },
                flags = "-JKkjhfdsjfg"
            };
            Console.WriteLine(ms.GetArgs());
        }
    }
}
