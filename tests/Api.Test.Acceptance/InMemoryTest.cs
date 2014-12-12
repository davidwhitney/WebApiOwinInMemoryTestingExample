using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using Microsoft.Owin.Hosting;
using Moq;
using NUnit.Framework;

namespace Api.Test.Acceptance
{
    [TestFixture]
    public abstract class InMemoryTest
    {
        private IDisposable _app;
        protected HttpClient HttpClient;
        private IDictionary<Type, Mock> _mocks;
        
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            var port = FreeTcpPort();
            _mocks = new ConcurrentDictionary<Type, Mock>();

            HttpClient = new HttpClient { BaseAddress = new Uri("http://localhost:" + port) };
            _app = WebApp.Start<Startup>("http://localhost:" + port);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            _app.Dispose();
        }
        
        public Mock<T> MockOut<T>() where T : class
        {
            if (_mocks.ContainsKey(typeof(T)))
            {
                RebindToConfiguredMocks();
                return (Mock<T>)_mocks[typeof(T)];
            }

            _mocks.Add(typeof(T), new Mock<T>());
            RebindToConfiguredMocks();

            return (Mock<T>)_mocks[typeof(T)];
        }

        private void RebindToConfiguredMocks()
        {
            foreach (var item in _mocks)
            {
                Startup.Container.Unbind(item.Key);
                var item1 = item;
                Startup.Container.Bind(item.Key).ToMethod(_ => _mocks[item1.Key].Object);
            }
        }

        private static int FreeTcpPort()
        {
            var l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            var port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
    }
}