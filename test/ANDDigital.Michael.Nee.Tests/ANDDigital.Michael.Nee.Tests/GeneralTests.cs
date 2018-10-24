using ANDDigital.Michael.Nee.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ANDDigital.Michael.Nee.Tests
{
    public class GeneralTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;
        public GeneralTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task API_Controller_Returns_Correct_Response_Code()
        {
            var response = await _fixture.Client.GetAsync("api/Values");
            response.EnsureSuccessStatusCode();
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task API_Controller_Returns_Correct_Not_Found_Code()
        {
            var response = await _fixture.Client.GetAsync("api/Values1");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        } 
    }
}
