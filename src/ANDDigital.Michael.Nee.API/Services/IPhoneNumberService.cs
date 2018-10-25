using ANDDigital.Michael.Nee.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ANDDigital.Michael.Nee.API.Services
{
    public interface IPhoneNumberService
    {
        IEnumerable<PhoneNumber> GetAllPhoneNumbers();

        bool ActivatePhoneNumber(string phoneNumber);

        bool DoesPhoneNumberExist(string phoneNumber);

        IEnumerable<PhoneNumber> PhoneNumbersForCustomer(string userId);
    }
}
