using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ANDDigital.Michael.Nee.API.Models;
using Ardalis.GuardClauses;

namespace ANDDigital.Michael.Nee.API.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        public bool ActivatePhoneNumber(string phoneNumber)
        {
            Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));

            var numberExist = DoesPhoneNumberExist(phoneNumber);

            if (!numberExist) return false;

            var number = PhoneNumber.AllPhoneNumbers().Single(c => c.Number == phoneNumber);

            return number.ActivateNumber();
        }

        public bool DoesPhoneNumberExist(string phoneNumber)
        {
            Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));

            var numberExist = PhoneNumber.DoesPhoneNumberAlreadyExist(phoneNumber);

            if (!numberExist) return false;

            return true;
        }

        public IEnumerable<PhoneNumber> GetAllPhoneNumbers()
        {
            return PhoneNumber.AllPhoneNumbers().OrderBy(c=> c.CreatedAt);
        }

        public IEnumerable<PhoneNumber> PhoneNumbersForCustomer(string userId)
        {
            Guard.Against.NullOrEmpty(userId, nameof(userId));

            if (PhoneNumber.AllPhoneNumbers().Any(c => c.Customer != null && c.Customer.UserId == userId))
            return PhoneNumber.AllPhoneNumbers().Where(c => c.Customer != null && c.Customer.UserId == userId).OrderBy(c => c.CreatedAt);

            return null;
        }
    }
}
