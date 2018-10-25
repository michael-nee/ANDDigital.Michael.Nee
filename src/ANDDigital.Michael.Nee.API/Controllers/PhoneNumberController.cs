using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ANDDigital.Michael.Nee.API.Models;
using ANDDigital.Michael.Nee.API.Services;
using ANDDigital.Michael.Nee.API.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ANDDigital.Michael.Nee.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PhoneNumberController : ControllerBase
    {
        private readonly IPhoneNumberService _phonService;
        private readonly IMapper _mapper;
        public PhoneNumberController(IPhoneNumberService phonService, IMapper mapper)
        {
            _phonService = phonService;
            _mapper = mapper;
        }
        [HttpGet("AllPhoneNumbers")]
        public ActionResult<IEnumerable<AllPhoneNumbersViewModel>> GetAllPhoneNumbers()
        {
            var viewModel = _mapper.Map<IEnumerable<AllPhoneNumbersViewModel>>(_phonService.GetAllPhoneNumbers());
            return Ok(viewModel);
        }

        [HttpPost("ActivatePhoneNumber")]
        public ActionResult<bool> ActivateNumber([Required]string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return BadRequest("phoneNumber cannot be empty");

            var doesNumberExist = _phonService.DoesPhoneNumberExist(phoneNumber);
            if(!doesNumberExist) return NotFound("phoneNumber does not exist");

            return Ok(_phonService.ActivatePhoneNumber(phoneNumber));
        }

        [HttpGet("CustomerPhoneNumbers")]
        public ActionResult<IEnumerable<CustomerPhoneNumbersViewModel>> CustomerPhoneNumbers([Required] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("userId cannot be empty");

            var phoneNumbers = _phonService.PhoneNumbersForCustomer(userId);

            if (phoneNumbers == null || phoneNumbers.Count() == 0) return NotFound();

            var viewModel = _mapper.Map<IEnumerable<CustomerPhoneNumbersViewModel>>(phoneNumbers);
            return Ok(viewModel);
        }
    }
}