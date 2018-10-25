using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ANDDigital.Michael.Nee.API.Models
{
    public class PhoneNumber
    {
        public string Number { get; private set; }

        public bool Activated { get; private set; } = false;

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset ActivatedAt { get; private set; }

        public DateTimeOffset CustomerAssignedAt { get; private set; }

        private PhoneNumber(string number)
        {
            Guard.Against.NullOrEmpty(number, nameof(number));
            Number = number;
            CreatedAt = DateTimeOffset.UtcNow;

            PhoneNumbers.Add(this);
        }

        public Customer Customer { get; private set; }

        public bool ActivateNumber()
        {
            if(Customer == null)
                throw new Exception("Phone Number requires customer to be activated.");

            if (Activated) throw new Exception("Phone Number already activated");

            Activated = true;
            ActivatedAt = DateTimeOffset.UtcNow;
            return Activated;
        }


        public static bool DoesPhoneNumberAlreadyExist(string number)
        {
            if (PhoneNumbers.Any(c => c.Number == number))
                return true;

            return false;
        }

        public static IEnumerable<PhoneNumber> AllPhoneNumbers()
        {
            return PhoneNumbers;
        }

        private static List<PhoneNumber> PhoneNumbers = new List<PhoneNumber>();

        public static PhoneNumber CreatePhoneNumber(string number)
        {
            Guard.Against.NullOrEmpty(number, nameof(number));

            if (DoesPhoneNumberAlreadyExist(number))
                throw new Exception("Number already exists");

            return new PhoneNumber(number);
        }

        public bool AddCustomerToPhoneNumber(Customer customer)
        {
            Guard.Against.Null(customer, nameof(customer));

            if (Customer != null) throw new Exception("Already a customer attached to phone number");

            this.Customer = customer;
            CustomerAssignedAt = DateTimeOffset.UtcNow;
            customer.AddTelephoneToCustomer(this);

            return true;
        }

        public static PhoneNumber GetPhoneNumber(string number)
        {
            Guard.Against.NullOrEmpty(number, nameof(number));

            if (DoesPhoneNumberAlreadyExist(number))
                return PhoneNumbers.Single(c => c.Number == number);

            return null;
        }
    }
}
