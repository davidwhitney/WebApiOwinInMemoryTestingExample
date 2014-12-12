using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using NUnit.Framework;

namespace Api.Test.Acceptance.ValueApi
{
    [TestFixture]
    public class WhenCallingValueApi : InMemoryTest
    {
        [Test]
        public void Test()
        {
            var response = HttpClient.GetAsync("/values/5").Result;
            var body = response.Content.ReadAsStringAsync().Result;

            

            //Execute necessary tests
            //Assert.Equal<string>("Hello world using OWIN TestServer", await response.Content.ReadAsStringAsync());
        }
    }


    public abstract class InMemoryTest
    {
        private IDisposable _app;
        protected HttpClient HttpClient;
        protected string TestServerUri = "http://localhost:12345";

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _app = WebApp.Start<Startup>(TestServerUri);
            HttpClient = new HttpClient {BaseAddress = new Uri(TestServerUri)};
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            _app.Dispose();
        }
    }
}
