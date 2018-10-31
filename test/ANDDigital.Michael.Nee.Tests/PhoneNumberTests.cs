using ANDDigital.Michael.Nee.API.Models;
using ANDDigital.Michael.Nee.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ANDDigital.Michael.Nee.Tests
{
    public class PhoneNumberTests
    {
        [Fact]
        public void PhoneNumber_Create_Successful()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("89389348394");

            Assert.IsType<PhoneNumber>(phoneNumber);
            Assert.NotNull(phoneNumber);
            Assert.Equal("89389348394", phoneNumber.Number);
            Assert.Null(phoneNumber.Customer);
            Assert.False(phoneNumber.Activated);


        }

        [Fact]
        public void PhoneNumber_NotDuplicates_Allowed_Throws_Exception()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("8938934343443");
            var exception = Assert.Throws<Exception>(   () => PhoneNumber.CreatePhoneNumber("8938934343443"));

            Assert.Equal("Number already exists", exception.Message);
        }

        [Fact]
        public void Get_All_PhoneNumbers_Returns_Multiple_Numbers()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("89389399443");
            var phoneNumber2 = PhoneNumber.CreatePhoneNumber("8938934433443");

            Assert.NotEmpty(PhoneNumber.AllPhoneNumbers);
        }

        [Fact]
        public void ActivatePhoneNumber_Returns_True()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("443344334431");

            var customer = Customer.CreateCustomer("Matthew Smith", "MS9861");

            phoneNumber.AddCustomerToPhoneNumber(customer);
            var activatedResponse = phoneNumber.ActivateNumber();

            Assert.True(activatedResponse);
        }

        [Fact]
        public void ActivateNumber_With_No_Customer_Returns_Exception()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("4434334431");

            var customer = Customer.CreateCustomer("Matts Smith", "MS9861");

            Assert.Throws<Exception>(() => phoneNumber.ActivateNumber());
        }

        [Fact]
        public void Does_PhoneNumber_Already_Exist_Return_True()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("4434334439");

            Assert.True(PhoneNumber.DoesPhoneNumberAlreadyExist("4434334439"));
        }

        [Fact]
        public void Does_PhoneNumber_Already_Exist_Return_False()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("00000000001");

            Assert.False(PhoneNumber.DoesPhoneNumberAlreadyExist("00000000002"));
        }

        [Fact]
        public void Create_PhoneNumber_Retriev_SameObject()
        {
            var phoneNumber = PhoneNumber.CreatePhoneNumber("10000000001");
            var phoneNumber2 = PhoneNumber.GetPhoneNumber("10000000001");
            Assert.Equal(phoneNumber, phoneNumber2);
        }

        [Fact]
        public void Get_PhoneNumber_Returns_Null()
        {
            var phoneNumber = PhoneNumber.GetPhoneNumber("99900000001");
            Assert.Null(phoneNumber);
        }
    }
}
