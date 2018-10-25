using ANDDigital.Michael.Nee.API.Models;
using ANDDigital.Michael.Nee.API.ViewModels;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace ANDDigital.Michael.Nee.Tests.API
{
    public class PhoneNumberAPITests : IClassFixture<TestServerFixture>
    {
        private const string _allPhoneNumbersUri = "api/PhoneNumber/AllPhoneNumbers";
        private const string _activePhoneNumberUri = "api/PhoneNumber/ActivatePhoneNumber";
        private const string _customerPhoneNumbersUri = "api/PhoneNumber/CustomerPhoneNumbers";

        private readonly TestServerFixture _fixture;
        public PhoneNumberAPITests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PhoneNumber_API_Controller_Returns_Correct_Response_Code()
        {
            var response = await _fixture.Client.GetAsync(_allPhoneNumbersUri);
            response.EnsureSuccessStatusCode();
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task PhoneNumber_API_Controller_Returns_NotEmptyStringList()
        {
            var response = await _fixture.Client.GetAsync(_allPhoneNumbersUri);
            response.EnsureSuccessStatusCode();

            var responseStrong = await response.Content.ReadAsStringAsync();

            responseStrong.Should().NotBe("[]");

            var model = JsonConvert.DeserializeObject<List<AllPhoneNumbersViewModel>>(responseStrong);

            Assert.IsType<List<AllPhoneNumbersViewModel>>(model);
        }

        [Fact]
        public async Task Activate_Valid_PhoneNumber()
        {
            var url = string.Format(_activePhoneNumberUri + "?phoneNumber={0}", "07700900043");

            var response = await _fixture.Client.PostAsync(url, null);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal("true", result);
        }

        [Fact]
        public async Task Activate_Invalid_PhoneNumber()
        {
            var url = string.Format(_activePhoneNumberUri + "?phoneNumber={0}", "0770090043");

            var response = await _fixture.Client.PostAsync(url, null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal("\"phoneNumber does not exist\"", result);
        }


        [Fact]
        public async Task Get_Phone_Numbers_Valid_User()
        {
            var url = string.Format(_customerPhoneNumbersUri + "?userId={0}", "MS1998");

            var response = await _fixture.Client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string result = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<CustomerPhoneNumbersViewModel>>(result);

            Assert.IsType<List<CustomerPhoneNumbersViewModel>>(model);

            //Assert.Equal("\"phoneNumber does not exist\"", result);
        }

        [Fact]
        public async Task Get_Phone_Numbers_Invalid_User_Returns_NotFound()
        {
            var url = string.Format(_customerPhoneNumbersUri + "?userId={0}", "MS19981");

            var response = await _fixture.Client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            string result = await response.Content.ReadAsStringAsync();

            Assert.Equal("", result);
        }
    }
}
