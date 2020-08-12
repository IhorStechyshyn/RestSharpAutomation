using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;

namespace RestSharpAutomation
{
    [TestFixture]
    public class TestBase
    {
        protected Settings Settings;
        protected Uri BaseUrl;

        [OneTimeSetUp]
        public void SetSettings()
        {
            var json = File.ReadAllText($@"{GetPathToProject()}\config.json");
            Settings = JsonConvert.DeserializeObject<Settings>(json);
            BaseUrl = new Uri($"{Settings.Protocol}://{Settings.Url}");
        }

        private string GetPathToProject()
        {
            var path = TestContext.CurrentContext.TestDirectory;
            path = path.Replace(@"\bin\Debug", "");

            return path;
        }
    }
}
