using ANDDigital.Michael.Nee.API.Models;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ANDDigital.Michael.Nee.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Can_Create_Customer_Static_Factory_Method()
        {
            var customer = Customer.CreateCustomer("Michael Nee", "mn1987");

            Assert.IsType<Customer>(customer);
            Assert.NotNull(customer);
            Assert.Equal("Michael Nee", customer.Name);
            Assert.Equal("mn1987", customer.UserId);
        }

        [Fact]
        public void Customer_Factory_Returns_Same_Customer_After_Adding_Same_Parameters()
        {
            var customer = Customer.CreateCustomer("Michael Nee", "mn19872");
            var customer2 = Customer.CreateCustomer("Michael Nee", "mn19872");

            Assert.Same(customer, customer2);
        }

        [Fact]
        public void Customes_Returns_Different_Customers()
        {
            var customer = Customer.CreateCustomer("Michael Smith1", "ms101990");
            var customer2 = Customer.CreateCustomer("Michael Nee1", "mn19871");

            Assert.NotSame(customer, customer2);
        }

        [Fact]
        public void New_Customer_Has_Zero_PhoneNumbers()
        {
            var customer = Customer.CreateCustomer("Michael Smith", "ms1990");

            Assert.IsType<Customer>(customer);
            Assert.NotNull(customer);
            Assert.Empty(customer.CustomerPhoneNumbers());
        }


        [Fact]
        public void Add_Telephone_Number_To_Customer()
        {
            var customer = Customer.CreateCustomer("Michael Smith", "ms11990");

            var telephone = PhoneNumber.CreatePhoneNumber("048939834384");

            Assert.IsType<Customer>(customer);
            Assert.NotNull(customer);
            Assert.Empty(customer.CustomerPhoneNumbers());

            Assert.IsType<PhoneNumber>(telephone);
            Assert.NotNull(telephone);
            Assert.Null(telephone.Customer);

            telephone.AddCustomerToPhoneNumber(customer);

            Assert.NotEmpty(customer.CustomerPhoneNumbers());
            Assert.NotEmpty(PhoneNumber.AllPhoneNumbers());
        }

        [Fact]
        public void Retrieve_Existing_Customer_Returns_Customer()
        {
            var customer = Customer.CreateCustomer("Craig Smith", "CS1990");

            var customer2 = Customer.GetCustomer("Craig Smith", "CS1990");

            Assert.Equal(customer, customer2);
        }

        [Fact]
        public void Retrieve_Existing_Customer_Returns_Null()
        {
            var customer = Customer.GetCustomer("Craig Smith1", "CS1990");

            Assert.Null(customer);
        }

        [Fact]
        public void Retrieve_Customers_ContainsCustomer()
        {
            var customer = Customer.CreateCustomer("Andew Smith", "ANDS9487334");
            var customers = Customer.AllCustomers();

            Assert.NotEmpty(customers);
        }

        [Fact]
        public void Rerieve_Customers_List_Correct_Type()
        {
            var customers = Customer.AllCustomers();
            Assert.IsAssignableFrom<IEnumerable<Customer>>(customers);
        }
    }
}
