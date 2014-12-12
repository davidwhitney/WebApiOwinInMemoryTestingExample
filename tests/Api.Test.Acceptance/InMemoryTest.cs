using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using NUnit.Framework;

namespace Api.Test.Acceptance
{
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