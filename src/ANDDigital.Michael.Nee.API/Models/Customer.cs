using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ANDDigital.Michael.Nee.API.Models
{
    public class Customer
    {
        private Customer(string name, string userId)
        {
            //check parameters for constructor aren't empty
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(userId, nameof(userId));

            this.Name = name;
            this.UserId = userId;

            Customers.Add(this);
        }

        public string Name { get; private set; }

        public string UserId { get; private set; }

        private List<PhoneNumber> PhoneNumbersList = new List<PhoneNumber>();

        public IEnumerable<PhoneNumber> CustomerPhoneNumbers()
        {
            return PhoneNumbersList;
        }

        internal bool AddTelephoneToCustomer(PhoneNumber number)
        {
            Guard.Against.Null(number, nameof(number));

            if (PhoneNumbersList.Contains(number))
                throw new Exception("Phone number already added to customer");

            PhoneNumbersList.Add(number);
            return true;
        }

        public static Customer CreateCustomer(string name, string userId)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(userId, nameof(userId));

            if (Customers.Any(c => c.Name == name && c.UserId == userId))
                return Customers.Single(c => c.Name == name && c.UserId == userId);

            return new Customer(name, userId);
        }

        public static Customer GetCustomer(string name, string userId)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(userId, nameof(userId));

            if (Customers.Any(c => c.Name == name && c.UserId == userId))
                return Customers.Single(c => c.Name == name && c.UserId == userId);

            return null;
        }

        private static List<Customer> Customers = new List<Customer>();

        public static IEnumerable<Customer> AllCustomers()
        {
            return Customers.ToList();
        }
    }
}
