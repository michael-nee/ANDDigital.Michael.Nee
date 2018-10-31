using ANDDigital.Michael.Nee.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace ANDDigital.Michael.Nee.Tests
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }
        public TestServerFixture()
        {
            string appRootPath = Path.GetFullPath(@"C:\\Users\\Michael\\source\\technical-interviews\\AND Digital\\code\\ANDDigital.Michael.Nee\\src\\ANDDigital.Michael.Nee.API");

            var builder = new WebHostBuilder()
                  .UseEnvironment("Development")
                  .UseStartup<Startup>();  // Uses Start up class from your API Host project to configure the test server

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }

    }
}
