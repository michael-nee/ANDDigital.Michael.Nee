using ANDDigital.Michael.Nee.API.Models;
using ANDDigital.Michael.Nee.API.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ANDDigital.Michael.Nee.API
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<PhoneNumber, AllPhoneNumbersViewModel>()
                .ForMember(dest => dest.CustomerName, source => source.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.UserId, source => source.MapFrom(src => src.Customer.UserId));


            CreateMap<PhoneNumber, CustomerPhoneNumbersViewModel>();
        }
    }
}
