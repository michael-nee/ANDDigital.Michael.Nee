﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ANDDigital.Michael.Nee.API.ViewModels
{
    public class CustomerPhoneNumbersViewModel
    {
        public string Number { get; set; }
        public bool Activated { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset ActivatedAt { get; set; }

        public DateTimeOffset CustomerAssignedAt { get; set; }
    }
}
